using System;
using System.Collections.Generic;
using ANFIS.ANN.Interfaces;

namespace ANFIS.ANN.NeuronClasses
{
	public class OutputNeuron : INeuron
	{
		public int InputSize { get; }

		public OutputNeuron(int inputSize)
		{
			InputSize = inputSize;
		}

		public double GetOutput(double[] input, int position, double[] variables)
		{
			if (input.Length != InputSize)
				throw new ArgumentException("This neuron accepts exactly " + InputSize + " numbers.");

			double sum = 0;
			foreach (double rule in input)
				sum += rule;

			return sum;
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
	}
}