using System;
using System.Collections.Generic;
using ANFIS.ANN.Interfaces;

namespace ANFIS.ANN
{
	public class NeuronLayer
	{
		public int NumberOfNeurons { get; }
		public int InputSize { get; }
		private Neuron[] _neurons;

		public NeuronLayer(int numberOfNeurons, int numberOfNeuronsInPreviousLayer, IActivationFunction function = null)
		{
			NumberOfNeurons = numberOfNeurons;
			InputSize = numberOfNeuronsInPreviousLayer;
			InitNeurons(function);
		}

		private void InitNeurons(IActivationFunction function)
		{
			_neurons = new Neuron[NumberOfNeurons];
			DetermineRightFunction(ref function);
			for (int i = 0; i < NumberOfNeurons; i++)
				_neurons[i] = new Neuron(InputSize, function);
		}

		public void ResetLayer()
		{
			for (var i = 0; i < NumberOfNeurons; i++)
				_neurons[i].Reset();
		}

		private void DetermineRightFunction(ref IActivationFunction function)
		{
			if (function is null)
				function = new Sigmoid();
			if (InputSize == 0)
				function = new Adaline();
		}

		public double[] GetOutputs(double[] inputs)
		{
			if(InputSize == 0 && inputs.Length != NumberOfNeurons)
				throw new ArgumentException(@"Input needs to contain the amount of elements that corresponds to input layer size!", inputs.ToString());
			if(InputSize != 0 && inputs.Length != InputSize)
				throw new ArgumentException(@"Input needs to contain the amount of elements that corresponds to former layer size!", inputs.ToString());

			double[] outputs = new double[NumberOfNeurons];
			for (int i = 0; i < NumberOfNeurons; i++)
			{
				outputs[i] = InputSize == 0 ? _neurons[i].GetOutput(new [] {inputs[i]}) : _neurons[i].GetOutput(inputs);
			}
			return outputs;
		}

		public void ApplyChange(List<double> doubles)
		{
			for (int i = 0; i < NumberOfNeurons; i++)
			{
				_neurons[i].ApplyChange(doubles.GetRange(i * InputSize, InputSize));
			}
		}

		public Neuron[] GetNeurons()
		{
			return _neurons;
		}
		
		// This is for last layer
		internal void Backpropagation(double[] inputs, double[] outputs, ref double[] deltaLayer, double[] desired, ref List<double> changes)
		{
			for (int j = 0; j < NumberOfNeurons; j++)
			{
				deltaLayer[j] = _neurons[j].Backpropagation(inputs, outputs, desired, ref changes, j);
			}
		}

		// This is for hidden layer
		internal void Backpropagation(double[] inputs, double[] outputs, ref double[] deltaLayer, double[] deltaNext, Neuron[] neurons, ref List<double> changes)
		{
			for (int j = 0; j < NumberOfNeurons; j++)
			{
				deltaLayer[j] = _neurons[j].Backpropagation(inputs, outputs, deltaNext, neurons, ref changes, j);
			}
		}
	}
}