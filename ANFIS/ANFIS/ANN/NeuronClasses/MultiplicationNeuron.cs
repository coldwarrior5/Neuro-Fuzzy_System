using System;
using System.Collections.Generic;
using ANFIS.ANN.Interfaces;

namespace ANFIS.ANN.NeuronClasses
{
	public class MultiplicationNeuron : INeuron
	{
		public int InputSize { get; }

		public MultiplicationNeuron(int inputSize)
		{
			InputSize = inputSize;
		}

		public double GetOutput(double[] input, int position, double[] variables)
		{
			if (input.Length != InputSize)
				throw new ArgumentException("This neuron accepts exactly " + InputSize + " numbers.");

			double product = 1;
			foreach (double variable in input)
				product *= variable;

			return product;
		}

		public void ResetWeights()
		{
		}

		public void UpdateParameters(double[] correction)
		{
		}

		public void UpdateParameters(List<double> correction)
		{
		}

		public double[] GetParameters()
		{
			return null;
		}

		public IActivationFunction GetFunction()
		{
			return null;
		}
	}
}