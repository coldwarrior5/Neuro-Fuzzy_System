using System;
using System.Collections.Generic;
using ANFIS.ANN.Interfaces;
using ANFIS.ANN.NeuronClasses;
using ANFIS.ANN.Structures;

namespace ANFIS.ANN
{
	public class NeuronLayer : INeuronLayer
	{
		public LayerInfo Architecture { get; }
		private readonly int _numberOfVariables;
		private INeuron[] _neurons;

		public NeuronLayer(LayerInfo architecture, int numberOfVariables)
		{
			Architecture = architecture;
			_numberOfVariables = numberOfVariables;
			InitNeuron();
		}

		private void InitNeuron()
		{
			_neurons = new INeuron[Architecture.NumberOfNeurons];
			for (int i = 0; i < Architecture.NumberOfNeurons; i++)
			{
				switch (Architecture.Type)
				{
					case NeuronType.Input:
						// Has input size of 1
						_neurons[i] = new InputNeuron();
						break;
					case NeuronType.Multiplication:
						_neurons[i] = new MultiplicationNeuron(Architecture.InputSize);
						break;
					case NeuronType.Normalization:
						_neurons[i] = new NormalizationNeuron(Architecture.InputSize);
						break;
					case NeuronType.Function:
						// Has input size of 1
						_neurons[i] = new FunctionNeuron(Architecture.InputSize, _numberOfVariables);
						break;
					case NeuronType.Output:
						_neurons[i] = new OutputNeuron(Architecture.InputSize);
						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(Architecture.Type), Architecture.Type, null);
				}
			}
		}

		public double[] GetOutputs(double[] inputs, double[] variables)
		{
			if (_neurons is null)
				return null;
			double[] outputs = new double[Architecture.NumberOfNeurons];
			int rules = Architecture.NumberOfNeurons / variables.Length;
			for (int i = 0; i < Architecture.NumberOfNeurons; i++)
			{
				switch (Architecture.Type)
				{
					case NeuronType.Input:
						outputs[i] = _neurons[i].GetOutput( new[] { inputs[i / rules]}, i, variables);
						break;
					case NeuronType.Multiplication:
						double[] input = new double[variables.Length];
						for (int j = 0; j < variables.Length; j++)
							input[j] = inputs[j * rules + i];
						outputs[i] = _neurons[i].GetOutput(input, i, variables);
						break;
					case NeuronType.Function:
						outputs[i] = _neurons[i].GetOutput(new[] { inputs[i] }, i, variables);
						break;
					case NeuronType.Normalization:
					case NeuronType.Output:
						outputs[i] = _neurons[i].GetOutput(inputs, i, variables);
						break;
				}
			}
			return outputs;
		}

		public void ResetLayer()
		{
			if (_neurons is null)
				return;
			for (var i = 0; i < Architecture.NumberOfNeurons; i++)
				_neurons[i].ResetWeights();
		}

		public void UpdateParameters(List<List<double>> correction)
		{
			if (_neurons is null)
				return;

			if (Architecture.Type is NeuronType.Function || Architecture.Type is NeuronType.Input)
			{
				if (correction.Count != Architecture.NumberOfNeurons)
					return;
				for (int i = 0; i < Architecture.NumberOfNeurons; i++)
				{
					_neurons[i].UpdateParameters(correction[i]);
				}
			}
		}

		public double[] GetParameters(int index)
		{
			return _neurons[index].GetParameters();
		}

		public T GetFunction<T>(int index)
		{
			return _neurons[index].GetFunction<T>();
		}
	}
}