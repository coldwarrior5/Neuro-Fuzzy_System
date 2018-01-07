using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using ANFIS.ANN.Functions;
using ANFIS.ANN.Interfaces;
using ANFIS.ANN.Structures;
using ANFIS.Structures;
using ILNumerics;
using ILNumerics.Drawing;
using ILNumerics.Drawing.Plotting;
using BackpropagationHandler = ANFIS.Structures.Backpropagation;

namespace ANFIS.ANN
{
	public class NeuralNetwork
	{
		public int NumberOfLayers { get; }
		private INeuronLayer[] _layers;
		private LayerInfo[] _architecture;
		public int NumberOfRules { get; set; }
		private BackpropagationType _bacpropagationType;
		public double Eta { get; set; }
		public double DesiredError { get; set; }
		public double TotalError { get; private set; }
		public List<double> ErrorTimeline { get; private set; }
		public Instance Instance { get; }

		private double[] _averageProbabilityOutcome;
		private double[] _probabilityOutcome;
		private double[] _z;

		public const int RulesMin = 1;
		public const int RulesDefault = 10;
		public const int RulesMax = 50;

		public const double EtaMin = 0.0001;
		public const double EtaDefault = 0.01;
		public const double EtaMax = 1;

		public const double DesiredErrorMin = 0;
		public const double DesiredErrorDefault = 100;
		public const double DesiredErrorMax = 100;

		public const int IterationLimit = 100000;
		public const int TimeLimit = 120; // in seconds

		public const double RefreshRate = 0.25; // Every quarter a second update the graph

		public NeuralNetwork(Instance instance)
		{
			Instance = instance;
			NumberOfRules = RulesDefault;
			DesiredError = DesiredErrorDefault;
			Eta = EtaDefault;
			ErrorTimeline = new List<double>();
			NumberOfLayers = 5;
			_layers = new INeuronLayer[NumberOfLayers];
		}

		private void InitNetwork()
		{
			DefineArchitecture(out LayerInfo[] architecture);
			if (_architecture is null || _architecture.Equals(architecture))
			{
				_architecture = architecture;
				for (int i = 0; i < NumberOfLayers; i++)
					_layers[i] = new NeuronLayer(architecture[i], Instance.NumVariables);
			}
			
			Evaluate();
		}

		private void DefineArchitecture(out LayerInfo[] architecture)
		{
			architecture = new LayerInfo[NumberOfLayers];
			architecture[0] = new LayerInfo ( 1, NumberOfRules * Instance.NumVariables, NeuronType.Input );
			architecture[1] = new LayerInfo ( Instance.NumVariables, NumberOfRules, NeuronType.Multiplication );
			architecture[2] = new LayerInfo ( NumberOfRules, NumberOfRules, NeuronType.Normalization );
			architecture[3] = new LayerInfo ( 1, NumberOfRules, NeuronType.Function );
			architecture[4] = new LayerInfo ( NumberOfRules, 1, NeuronType.Output );
		}

		public void Train(ILPanel panel, Label totalError, Button testNetwork)
		{
			InitNetwork();
			int iter = 0;
			List<List<Sample>> batches = FormBatches();
			Stopwatch totalTime = new Stopwatch();
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			totalTime.Start();
			while (TotalError > DesiredError && iter++ < IterationLimit && totalTime.Elapsed.TotalSeconds < TimeLimit)
			{
				Backpropagation(batches);
				stopwatch.Stop();
				if (stopwatch.Elapsed.TotalSeconds < RefreshRate)
				{
					stopwatch.Start();
					continue;
				}
				Evaluate();
				UpdateInfo(panel, totalError, this);
				stopwatch.Restart();
			}
			testNetwork.Enabled = true;
		}

		private List<List<Sample>> FormBatches()
		{
			List<List<Sample>> batches = null;
			switch (_bacpropagationType)
			{
				case BackpropagationType.Batch:
					batches = new List<List<Sample>>
					{
						Instance.Samples.ToList()
					};
					break;
				case BackpropagationType.Online:
					batches = new List<List<Sample>>();
					foreach (Sample t in Instance.Samples)
						batches.Add(new List<Sample> {t});
					break;
			}
			return batches;
		}

		private void Backpropagation(List<List<Sample>> batches)
		{
			InitChanges(out List<List<double>> antecedentChanges, NumberOfRules * Instance.NumVariables, 2);
			InitChanges(out List<List<double>> consequenseChanges, NumberOfRules, Instance.NumVariables + 1);
			// Go through every batch
			for (int i = 0; i < batches.Count; i++)
			{
				// Go through every sample in the batch
				for (int j = 0; j < batches[i].Count; j++)
				{
					// Get all ai and bi
					List<double[]> parameters = new List<double[]>();
					for (int k = 0; k < NumberOfRules * Instance.NumVariables; k++)
						parameters.Add(_layers[0].GetParameters(k));

					// Determine layer outputs
					double[] givenOutput = GetOutput(batches[i][j].Variables);
					
					CalculateAntecedentChange(ref antecedentChanges, batches[i][j].Value, givenOutput[0], batches[i][j].Variables, parameters);
					CalculateConsequenceChange(ref consequenseChanges, batches[i][j].Value, givenOutput[0], batches[i][j].Variables);
				}
				// Need to average the change and assign learning rate
				AverageAndMultiplyWithLearningRate(ref antecedentChanges, batches[i].Count);
				AverageAndMultiplyWithLearningRate(ref consequenseChanges, batches[i].Count);
				// Apply change after single batch
				_layers[0].UpdateParameters(antecedentChanges);
				_layers[3].UpdateParameters(consequenseChanges);
				// Now set changes to zero
				ResetChanges(ref antecedentChanges);
				ResetChanges(ref consequenseChanges);
			}
		}

		private void CalculateAntecedentChange(ref List<List<double>> changes, double expectedOutput, double givenOutput, double[] variables, List<double[]> parameters)
		{
			double difference = expectedOutput - givenOutput;
			CalculateDividend(out List<double> dividend);
			CalculateDivisor(out double divisor);
			for (int i = 0; i < Instance.NumVariables; i++)
			{
				for (int j = 0; j < NumberOfRules; j++)
				{
					double otherProbabilities = 1;
					for (int k = 0; k < Instance.NumVariables; k++)
					{
						if(k == i) continue;
						otherProbabilities *= _probabilityOutcome[k * NumberOfRules + j];
					}
					double tempValue = difference * (dividend[j] / divisor) *_probabilityOutcome[i * NumberOfRules + j] * (1 - _probabilityOutcome[i * NumberOfRules + j]) * otherProbabilities;
					changes[i * NumberOfRules + j][0] += tempValue * parameters[i][1];
					changes[i * NumberOfRules + j][1] += tempValue * (parameters[i][0] - variables[i]);
				}
			}
		}

		private void CalculateDivisor(out double divisor)
		{
			divisor = 0;
			for (int i = 0; i < NumberOfRules; i++)
			{
				divisor += Math.Pow(_averageProbabilityOutcome[i], 2);
			}
		}

		private void CalculateDividend(out List<double> dividend)
		{
			dividend = new List<double>(new double[NumberOfRules]);

			for (int i = 0; i < NumberOfRules; i++)
			{
				for (int j = 0; j < NumberOfRules; j++)
				{
					dividend[i] += _averageProbabilityOutcome[j] * (_z[i] - _z[j]);
				}
			}
		}

		private void CalculateConsequenceChange(ref List<List<double>> changes, double expectedOutput, double givenOutput, double[] variables)
		{
			for (int i = 0; i < NumberOfRules; i++)
			{
				double tempValue = (expectedOutput - givenOutput) * _averageProbabilityOutcome[i];
				for (int j = 0; j < Instance.NumVariables; j++)
					changes[i][j] += tempValue * variables[j];
				changes[i][Instance.NumVariables] += tempValue;
			}
		}

		private void AverageAndMultiplyWithLearningRate(ref List<List<double>> changes, int batchSize)
		{
			for (int i = 0; i < changes.Count; i++)
			{
				for (int j = 0; j < changes[i].Count; j++)
					changes[i][j] = changes[i][j] / batchSize * Eta;
			}
		}

		private void InitChanges(out List<List<double>> changes, int size, int variableSize)
		{
			changes = new List<List<double>>();
			for (int i = 0; i < size; i++)
			{
				changes.Add(new List<double>());
				for (int j = 0; j < variableSize; j++)
					changes[i].Add(0);
			}
		}

		private void ResetChanges(ref List<List<double>> changes)
		{
			for (int i = 0; i < changes.Count; i++)
			{
				for (int j = 0; j < changes[i].Count; j++)
				{
					changes[i][j] = 0;
				}
			}
		}

		public void ResetTraining()
		{
			TotalError = 0;
			ErrorTimeline.Clear();
			if (_architecture is null)
				return;
			for (int i = 0; i < NumberOfLayers; i++)
			{
				_layers[i].ResetLayer();
			}
		}

		private double[] GetOutput(double[] inputs)
		{
			double[] tempInputs = inputs;
			for (var i = 0; i < NumberOfLayers; i++)
			{
				try
				{
					tempInputs = _layers[i].GetOutputs(tempInputs, inputs);
					switch (i)
					{
						case 0:
							_probabilityOutcome = tempInputs;
							break;
						case 2:
							_averageProbabilityOutcome = tempInputs;
							break;
						case 3:
							_z = tempInputs;
							break;
					}
				}
				catch(Exception)
				{
					return null;
				}
			}
			return tempInputs;
		}

		public void Evaluate()
		{
			TotalError = 0;
			double[] givenOutputs = new double[Instance.Samples.Length];
			double[] expectedOutputs = new double[Instance.Samples.Length];
			
			for (int i = 0; i < Instance.Samples.Length; i++)
			{
				double[] temp = GetOutput(Instance.Samples[i].Variables);
				givenOutputs[i] = temp[0];
				expectedOutputs[i] = Instance.Samples[i].Value;
			}

			TotalError = Error.Evaluate(givenOutputs, expectedOutputs);
		}

		public void OnValueChanged_Type(object sender, EventArgs e)
		{
			if (sender is ComboBox box)
			{
				_bacpropagationType = BackpropagationHandler.ToEnum((string)box.SelectedItem);
			}
		}

		private static TResult Convert<TResult>(Func<double[], TResult> f, params double[] args)
		{
			TResult result = f(args);
			return result;
		}

		public static void FillTrainChoices(ILPanel panel, Label totalError, TextBox rules, ComboBox backpropagationType, TextBox eta, TextBox desiredError, NeuralNetwork ann)
		{
			InitPanel(panel, ann);
			totalError.Text = ann.TotalError.ToString(CultureInfo.InvariantCulture);
			rules.Text = RulesDefault.ToString(CultureInfo.InvariantCulture);
			FillTypeChoices(backpropagationType);
			eta.Text = EtaDefault.ToString(CultureInfo.InvariantCulture);
			desiredError.Text = DesiredErrorDefault.ToString(CultureInfo.InvariantCulture);
		}


		private static void InitPanel(ILPanel panel, NeuralNetwork ann)
		{
			panel.Scene.Remove(panel.Scene.First<ILPlotCube>());
			int xmin = (int)ann.Instance.Samples[0].Variables[0];
			int xmax = (int)ann.Instance.Samples[ann.Instance.NumSamples - 1].Variables[0];
			int ymin = (int)ann.Instance.Samples[0].Variables[1];
			int ymax = (int)ann.Instance.Samples[ann.Instance.NumSamples - 1].Variables[0];
			
			ILArray<double> positions = ILMath.zeros<double>(3, ann.Instance.NumSamples);

			int index = 0;
			foreach (var sample in ann.Instance.Samples)
			{
				positions[0, index] = sample.Variables[0];
				positions[1, index] = sample.Variables[1];
				positions[2, index] = sample.Value;
				index++;
			}

			ILPlotCube cube = new ILPlotCube(twoDMode: false)
			{
				// rotate plot cube
				Rotation = Matrix4.Rotation(new Vector3(-1, 1, .1f), 0.4f),
				// perspective projection
				Projection = Projection.Perspective,
				Children =
				{
					new ILSurface((x, y) => 0,
						xmin, xmax, 50,
						ymin, ymax, 50,
						(x,y) => x * y,
						colormap: Colormaps.Autumn) {
						UseLighting = true
					},
					new ILSurface((x, y) => (float)Convert(ann.Instance.OriginalFunction.ValueAt, x, y),
						xmin, xmax, 50,
						ymin, ymax, 50,
						(x, y) => x * y,
						colormap: Colormaps.Hsv)
					{
						UseLighting = true
					},
					// add line plot, provide data as rows 
					new ILPoints {
						Positions = ILMath.tosingle(positions),
						Color = Color.Red
					}
				}
			};
			panel.Scene.Add(cube);
			panel.Scene.First<ILPlotCube>().Rotation = Matrix4.Rotation(Vector3.UnitX, .8f) * Matrix4.Rotation(Vector3.UnitZ, .6f);
			panel.Refresh();
		}
		
		public static void UpdateInfo(ILPanel panel, Label totalError, NeuralNetwork ann)
		{
			DrawFunctions(panel, ann);
			totalError.Text = ann.TotalError.ToString(CultureInfo.InvariantCulture);
			totalError.Update();
		}

		private static void DrawFunctions(ILPanel panel, NeuralNetwork ann)
		{
			int xmin = (int)ann.Instance.Samples[0].Variables[0];
			int xmax = (int)ann.Instance.Samples[ann.Instance.NumSamples - 1].Variables[0];
			int ymin = (int)ann.Instance.Samples[0].Variables[1];
			int ymax = (int)ann.Instance.Samples[ann.Instance.NumSamples - 1].Variables[0];
			
			var approximatedFunction = new ILSurface((x, y) => (float)Convert(ann.GetOutput, x, y)[0],
				xmin, xmax, 50,
				ymin, ymax, 50,
				(x,y) => x * y,
				colormap: Colormaps.Autumn) {
				UseLighting = true
			};
			panel.Scene.Remove(panel.Scene.First<ILSurface>());
			panel.Scene.First<ILPlotCubeDataGroup>().Insert(0, approximatedFunction);
			panel.Refresh();
		}

		public static void FillTypeChoices(ComboBox box)
		{
			box.Items.Clear();
			box.Items.Add(BackpropagationHandler.ToString(BackpropagationType.Batch));
			box.Items.Add(BackpropagationHandler.ToString(BackpropagationType.Online));
			box.SelectedItem = box.Items[0];
		}
	}
}