using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ANFIS.ANN.Interfaces;
using ANFIS.Structures;
using BackpropagationHandler = ANFIS.Structures.Backpropagation;
using ContentAlignment = System.Drawing.ContentAlignment;

namespace ANFIS.ANN
{
	public class NeuralNetwork
	{
		public int NumberOfLayers { get; private set; }
		private List<int> _architecture;
		private IActivationFunction _function;
		private IActivationFunction _outputLayerFunction;
		private List<NeuronLayer> _layers;
		private BackpropagationType _bacpropagationType;
		private double _eta;
		private double _desiredError;
		private double[] _perSymbolError;
		public double TotalError { get; private set; }
		private Instance _instance;
		private double[][] _yLayer;
		private double[][] _deltaLayer;


		public const double EtaMin = 0.0001;
		public const double EtaDefault = 0.1;
		public const double EtaMax = 1;

		public const double DesiredErrorMin = 0;
		public const double DesiredErrorDefault = 0.0001;
		public const double DesiredErrorMax = 5;

		public const int IterationLimit = 10000;

		public const int NeuronMin = 1;
		private readonly int _neuronDefault;
		private List<double> _changes;
		public const int NeuronMax = 200;

		public const int LayersMin = 3;
		public const int LayersMax = 10;

		public const double RefreshRate = 0.25; // Every quarter a second update the graph

		// TODO refactor neural network definition network should looks completely different
		public NeuralNetwork(Instance instance, IActivationFunction function = null, IActivationFunction outputLayerFunction = null)
		{
			_instance = instance;
			int inputSize = instance.NumVariables * 2;
			int outputSize = 1;
			_neuronDefault = outputSize;
			_desiredError = DesiredErrorDefault;
			_eta = EtaDefault;
			List<int> architecture = new List<int> {inputSize, _neuronDefault, outputSize};
			InitNeuralNetwork(architecture, function, outputLayerFunction);
		}

		private void InitNeuralNetwork(List<int> architecture, IActivationFunction function = null, IActivationFunction outputLayerFunction = null)
		{
			if (architecture.Count < LayersMin)
				throw new ArgumentException(@"Architecture must contain at least one hidden layer!", architecture.ToString());
			NumberOfLayers = architecture.Count;
			_architecture = architecture;
			_function = function;
			_outputLayerFunction = outputLayerFunction;
			_perSymbolError = new double[_instance.NumSamples];

			ResetTraining();
		}

		public void AddLayer()
		{
			NumberOfLayers++;
			_architecture.Insert(_architecture.Count - 1, _neuronDefault);
		}

		public void RemoveLayer()
		{
			NumberOfLayers--;
			_architecture.RemoveAt(_architecture.Count - 1);
		}

		public void UpdateLayer(int index, int value)
		{
			if (index == 0 || index == NumberOfLayers - 1)
				throw new Exception("Input and output layers cannot be changed!");
			_architecture[index] = value;
		}

		public void ChangeEta(double eta)
		{
			_eta = eta;
		}

		public double GetEta()
		{
			return _eta;
		}

		public void ChangeDesiredError(double desiredError)
		{
			_desiredError = desiredError;
		}

		public double GetDesiredError()
		{
			return _desiredError;
		}

		private void InitNetwork()
		{
			_layers = new List<NeuronLayer>();
			_yLayer = new double[NumberOfLayers][];
			_deltaLayer = new double[NumberOfLayers][];
			for (int i = 0; i < NumberOfLayers; i++)
			{
				int numberOfNeuronsInFormerLayer = i == 0 ? 0 : _architecture[i - 1];
				IActivationFunction currentFunction = i == NumberOfLayers - 1 ? _outputLayerFunction : _function;
				_layers.Add(new NeuronLayer(_architecture[i], numberOfNeuronsInFormerLayer, currentFunction));
				_yLayer[i] = new double[_architecture[i]];
				_deltaLayer[i] = new double[_architecture[i]];
			}
			Evaluate();
		}

		public void Train(Chart chart, Label totalError, Button testNetwork)
		{
			InitNetwork();
			int iter = 0;
			List<List<Sample>> batches = FormBatches();
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			while (TotalError > _desiredError && iter++ < IterationLimit)
			{
				Backpropagation(batches);
				stopwatch.Stop();
				if (stopwatch.Elapsed.TotalSeconds < RefreshRate)
				{
					stopwatch.Start();
					continue;
				}
				Evaluate();
				FillChart(chart, totalError, this);
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
						_instance.Samples.ToList()
					};
					break;
				case BackpropagationType.Online:
					batches = new List<List<Sample>>();
					foreach (Sample t in _instance.Samples)
						batches.Add(new List<Sample> {t});
					break;
			}
			return batches;
		}

		private void Backpropagation(List<List<Sample>> batches)
		{
			InitChanges(out List<List<double>> changes);
			/*
			// Go through every batch
			for (int i = 0; i < batches.Count; i++)
			{
				// Go through every simbol in the batch
				for (int j = 0; j < batches[i].Count; j++)
				{
					// Determine layer outputs
					GetOutputs(batches[i][j].Variables[0], batches[i][j].Variables[1]);
					// Go through each layer and evaluate delta
					for (int k = NumberOfLayers - 1; k > 0; k--)
					{
						bool outputLayer = k == NumberOfLayers - 1;
						double[] input = _yLayer[k - 1];
						double[] output = _yLayer[k];
						double[] deltaOrDesired = outputLayer ? Array.ConvertAll(batches[i][j].Class, element => (double)element) : _deltaLayer[k + 1];
						_changes = changes[k - 1];
						if (outputLayer)
						{
							_layers[k].Backpropagation(input, output, ref _deltaLayer[k], deltaOrDesired, ref _changes);
						}
						else
						{
							_layers[k].Backpropagation(input, output, ref _deltaLayer[k], deltaOrDesired, _layers[k + 1].GetNeurons() , ref _changes);
						}
					}
				}

				// Multiply each change with learning rate
				for (int j = 0; j < changes.Count; j++)
				{
					for (int k = 0; k < changes[j].Count; k++)
					{
						changes[j][k] *= _eta;
					}
				}

				// Apply change after single batch
				for (int j = 1; j < NumberOfLayers; j++)
				{
					_layers[j].ApplyChange(changes[j - 1]);
				}
				ResetChanges(ref changes);
			}
			*/
		}

		//TODO see about init weight
		private void InitChanges(out List<List<double>> changes)
		{
			changes = new List<List<double>>();
			// No changes for input layer
			for (int i = 0; i < NumberOfLayers - 1; i++)
			{
				changes.Add(new List<double>());
				for (int j = 0; j < _architecture[i] * _architecture[i + 1]; j++)
				{
					changes[i].Add(0);
				}
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

		public void ResetNetwork()
		{
			for (var i = 0; i < NumberOfLayers; i++)
			{
				_layers[i].ResetLayer();
			}
		}

		public void ResetTraining()
		{
			TotalError = 0;
			for (int i = 0; i < _instance.NumSamples; i++)
				_perSymbolError[i] = 0;

			_layers = new List<NeuronLayer>();
			_yLayer = new double[NumberOfLayers][];
			_deltaLayer = new double[NumberOfLayers][];
		}

		public double[] GetOutputs(double[] xValues, double[] yValues)
		{
			double[] input = FormatInput(xValues, yValues);
			return GetOutputs(input);
		}

		private double[] FormatInput(double[] xValues, double[] yValues)
		{
			return xValues.Concat(yValues).ToArray();
		}

		private double[] GetOutputs(double[] inputs)
		{
			double[] tempInputs = inputs;
			for (var i = 0; i < NumberOfLayers; i++)
			{
				try
				{
					tempInputs = _yLayer[i] = _layers[i].GetOutputs(tempInputs);
				}
				catch(Exception)
				{
					return null;
				}
				
			}
			return tempInputs;
		}

		public List<int> GetArchitecture()
		{
			return _architecture;
		}

		public void Evaluate()
		{
			/*
			TotalError = 0;
			double[][] givenOutputs = new double[_instance.Samples.Length][];
			int[][] expectedOutputs = new int[_instance.Samples.Length][];
			
			for (int i = 0; i < _instance.Samples.Length; i++)
			{
				givenOutputs[i] = GetOutputs(_instance.Samples[i].XPositions, _instance.Samples[i].YPositions);
				expectedOutputs[i] = _instance.Samples[i].Class;
			}

			_perSymbolError = CriterionFunction.EvaluatePerSymbol(givenOutputs, expectedOutputs);
			for (int i = 0; i < _instance.NumSamples; i++)
			{
				TotalError += _perSymbolError[i];
			}
			*/
		}

		public void OnValueChanged_Type(object sender, EventArgs e)
		{
			if (sender is ComboBox box)
			{
				_bacpropagationType = BackpropagationHandler.ToEnum((string)box.SelectedItem);
			}
		}

		public bool Consistent()
		{
			if (_layers.Count != _architecture.Count)
				return false;
			for (int i = 0; i < _layers.Count; i++)
			{
				if (_layers[i].NumberOfNeurons != _architecture[i])
					return false;
			}
			return true;
		}

		public void FixArchitecture()
		{
			_architecture = new List<int>();
			foreach (NeuronLayer t in _layers)
			{
				_architecture.Add(t.NumberOfNeurons);
			}
			NumberOfLayers = _architecture.Count;
		}

		public static void FillTrainChoices(Chart graph, Label totalError, TableLayoutPanel layoutArchitecture, ComboBox backpropagationType, TextBox eta, TextBox desiredError, NeuralNetwork ann)
		{
			FillChart(graph, totalError, ann);
			FillPanel(layoutArchitecture, ann);
			FillTypeChoices(backpropagationType);
			eta.Text = EtaDefault.ToString(CultureInfo.InvariantCulture);
			desiredError.Text = DesiredErrorDefault.ToString(CultureInfo.InvariantCulture);
		}

		public static void FillChart(Chart graph, Label totalError, NeuralNetwork ann)
		{
			string name = "SymbolError";
			graph.Series[name].Points.Clear();
			graph.ChartAreas[0].AxisY.Maximum = Math.Abs(ann.TotalError) < double.Epsilon ? 1 : ann.TotalError;

			for (int i = 0; i < ann._instance.NumSamples; i++)
			{
				graph.Series[name].Points.AddXY(i + 1, ann._perSymbolError[i]);
			}
			graph.Update();
			totalError.Text = ann.TotalError.ToString(CultureInfo.InvariantCulture);
			totalError.Update();
		}

		public static void FillPanel(TableLayoutPanel panel, NeuralNetwork ann)
		{
			int rowCount = 1;
			int columnCount = ann.NumberOfLayers;
			panel.ColumnCount = columnCount;
			panel.RowCount = rowCount;

			panel.ColumnStyles.Clear();
			panel.RowStyles.Clear();
			panel.Controls.Clear();

			for (var i = 0; i < columnCount; i++)
			{
				panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / columnCount));
			}
			for (var i = 0; i < rowCount; i++)
			{
				panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rowCount));
			}

			Label input = new Label
			{
				Text = ann._architecture[0].ToString(),
				AutoSize = false,
				TextAlign = ContentAlignment.MiddleCenter,
				Dock = DockStyle.Fill
			};
			panel.Controls.Add(input);

			
			for (int i = 1; i < ann.NumberOfLayers - 1; i++)
			{
				TextBox box = new TextBox
				{
					Text = ann._architecture[i].ToString(),
					Name = $"t_{i}",
					Dock = DockStyle.Fill
				};
				panel.Controls.Add(box);

			}
			
			Label output = new Label
			{
				Text = ann._architecture[ann.NumberOfLayers - 1].ToString(),
				AutoSize = false,
				TextAlign = ContentAlignment.MiddleCenter,
				Dock = DockStyle.Fill
			};
			panel.Controls.Add(output);
		}

		public static void FillTypeChoices(ComboBox box)
		{
			box.Items.Clear();
			box.Items.Add(BackpropagationHandler.ToString(BackpropagationType.Batch));
			box.Items.Add(BackpropagationHandler.ToString(BackpropagationType.Online));
			box.SelectedItem = box.Items[0];
		}

		public static int WhichClass(double[] solution)
		{
			int whichClass = 0;
			double max = 0;
			for (int i = 0; i < solution.Length; i++)
			{
				if (!(solution[i] > max)) continue;
				max = solution[i];
				whichClass = i;
			}
			return whichClass;
		}

		public static void SetClasses(TableLayoutPanel panel, double[] results)
		{
			int columnCount = results.Length > 4 ? 5 : results.Length;
			int rowCount = (results.Length - 1) / 5 + 1;

			panel.ColumnCount = columnCount;
			panel.RowCount = rowCount;

			panel.ColumnStyles.Clear();
			panel.RowStyles.Clear();
			panel.Controls.Clear();

			for (var i = 0; i < columnCount; i++)
			{
				panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / columnCount));
			}
			for (var i = 0; i < rowCount; i++)
			{
				panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / rowCount));
			}

			for (var i = 0; i < results.Length; i++)
			{
				var label = new Label
				{
					Text = @"Class " + (i + 1) + @": " + results[i].ToString("0.0000"),
					Name = $"c_{i + 1}",
					AutoSize = false,
					TextAlign = ContentAlignment.MiddleCenter,
					Dock = DockStyle.Fill
				};
				
				panel.Controls.Add(label);
			}
		}

		public static int WhichClass(int[] classes)
		{
			int whichClass = -1;
			for (int i = 0; i < classes.Length; i++)
			{
				if (classes[i] != 1) continue;
				whichClass = i;
				break;
			}
			return whichClass;
		}
	}
}