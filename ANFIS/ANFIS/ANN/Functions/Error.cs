using System;
using System.Linq;
using ANFIS.Handlers.Mathematics.Interface;
using ANFIS.Structures;

namespace ANFIS.ANN.Functions
{
	public static class Error
	{
		public static double Evaluate(double[] givenOutputs, double[] expectedOutputs)
		{
			if(givenOutputs.Length != expectedOutputs.Length)
				throw new Exception("Arrays must be of same size!");

			double sum = 0;
			for (int i = 0; i < givenOutputs.Length; i++)
			{
				sum += Math.Pow(expectedOutputs[i] - givenOutputs[i], 2);
			}
			return sum / givenOutputs.Length;
		}
	}

	public class ErrorFunction : IFunction
	{
		private readonly int _numVariables;
		private readonly Sample[] _samples;
		private readonly NeuralNetwork _ann;

		public ErrorFunction(Sample[] samples, NeuralNetwork ann)
		{
			_numVariables = samples.Length;
			_samples = samples;
			_ann = ann;
		}

		public double ValueAt(double[] variables)
		{
			bool found = FindSample(variables, out double expectedOutput);
			return found ? _ann.GetOutput(variables)[0] - expectedOutput : 0;
		}

		public int GetNumVariables()
		{
			return _numVariables;
		}

		public string Name()
		{
			return "DeltaFunction";
		}

		private bool FindSample(double[] variables, out double result)
		{
			for (int i = 0; i < _numVariables; i++)
			{
				if (_samples[i].Variables.SequenceEqual(variables))
				{
					result = _samples[i].Value;
					return true;
				}
			}
			result = 0;
			return false;
		}
	}
}