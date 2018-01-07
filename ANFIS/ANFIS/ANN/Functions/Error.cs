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
			return sum;
		}
	}

	public class ErrorFunction : IFunction
	{
		private readonly int _numVariables;
		private readonly Sample[] _samples;
		private readonly IFunction _estimator;

		public ErrorFunction(Sample[] samples, IFunction estimator)
		{
			_numVariables = samples.Length;
			_samples = samples;
			_estimator = estimator;
		}

		public double ValueAt(double[] variables)
		{
			var expectedOutput = FindSample(variables);
			return _estimator.ValueAt(variables) - expectedOutput;
		}

		public int GetNumVariables()
		{
			return _numVariables;
		}

		public string Name()
		{
			return "DeltaFunction";
		}

		private double FindSample(double[] variables)
		{
			for (int i = 0; i < _numVariables; i++)
			{
				if (_samples[i].Variables.SequenceEqual(variables))
					return _samples[i].Value;
			}
			throw new IndexOutOfRangeException("No such sample can be found");
		}
	}
}