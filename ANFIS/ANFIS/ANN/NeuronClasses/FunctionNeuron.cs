using System;
using System.Collections.Generic;
using ANFIS.ANN.Interfaces;
using ANFIS.Handlers.Mathematics;
using ANFIS.Handlers.Mathematics.Interface;

namespace ANFIS.ANN.NeuronClasses
{
	public class FunctionNeuron : INeuron
	{
		public int InputSize { get; }
		public int VariableSize { get; }
		private readonly IAdaptingFunction _function;

		public FunctionNeuron(int inputSize, int variableSize)
		{
			InputSize = inputSize;
			VariableSize = variableSize;
			_function = new SimpleFunction(variableSize + 1);
		}

		public FunctionNeuron(int inputSize, int variableSize, IAdaptingFunction function)
		{
			InputSize = inputSize;
			VariableSize = variableSize;
			_function = function;
		}
		
		public void UpdateParameters(double[] correction)
		{
			_function.UpdateParameters(correction);
		}

		public void UpdateParameters(List<double> correction)
		{
			_function.UpdateParameters(correction);
		}

		public double[] GetParameters()
		{
			return _function.GetParameters();
		}

		public IActivationFunction GetFunction()
		{
			return null;
		}

		public double GetOutput(double[] input, int position, double[] variables)
		{
			if(variables == null)
				throw new ArgumentException("This neuron requires variables for activation function.");

			if (variables.Length != VariableSize)
				throw new ArgumentException("This neuron accepts exactly " + VariableSize + " variables.");

			if (input.Length != InputSize)
				throw new ArgumentException("This neuron accepts exactly " + InputSize + " iputs.");

			return input[0] * _function.ValueAt(variables);
		}

		public void ResetWeights()
		{
			_function.ResetParameters();
		}
	}
}