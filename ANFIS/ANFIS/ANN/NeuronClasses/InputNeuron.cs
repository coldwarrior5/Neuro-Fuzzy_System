using System;
using System.Collections.Generic;
using ANFIS.ANN.Functions;
using ANFIS.ANN.Interfaces;

namespace ANFIS.ANN.NeuronClasses
{
	public class InputNeuron : INeuron
	{
		public int InputSize { get; }
		private readonly IActivationFunction _function;

		public InputNeuron()
		{
			InputSize = 1;
			_function = new Sigmoid();
		}

		public InputNeuron(IActivationFunction function)
		{
			_function = function;
		}

		public double GetOutput(double[] input, int position, double[] variables)
		{
			if (input.Length != InputSize)
				throw new ArgumentException("Input neuron can handle only one number.");
			return _function.ValueAt(input[0]);
		}

		public void ResetWeights()
		{
			_function.ResetWeights();
		}

		public void UpdateParameters(double[] correction)
		{
			UpdateA(correction[0]);
			UpdateB(correction[1]);
		}

		public void UpdateParameters(List<double> correction)
		{
			UpdateA(correction[0]);
			UpdateB(correction[1]);
		}

		public double[] GetParameters()
		{
			return _function.GetWeights();
		}

		public IActivationFunction GetFunction()
		{
			return _function;
		}

		private void UpdateA(double correction)
		{
			((Sigmoid)_function).UpdateA(correction);
		}

		private void UpdateB(double correction)
		{
			((Sigmoid)_function).UpdateB(correction);
		}
	}
}