using System;
using ANFIS.ANN.Interfaces;
using ANFIS.Handlers.Mathematics;

// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace ANFIS.ANN.Functions
{
	public class Sigmoid : IActivationFunction
	{
		private double _a;
		private double _b;

		public Sigmoid()
		{
			ResetWeights();
		}

		public Sigmoid(double a, double b)
		{
			_a = a;
			_b = b;
		}

		public double ValueAt(double x)
		{
			double k = Math.Exp(_b*(x - _a));
			return 1.0f / (1.0f + k);
		}
		
		public void UpdateA(double correction)
		{
			_a += correction;
		}

		public void UpdateB(double correction)
		{
			_b += correction;
		}

		public void ResetWeights()
		{
			_a = MathHandler.Rand.NextDouble() * 2 - 1;
			_b = MathHandler.Rand.NextDouble() * 2 - 1;
		}

		public double[] GetWeights()
		{
			return new[] {_a, _b};
		}
	}

	public class Adaline : IActivationFunction
	{
		private double _a;
		private double _b;

		public Adaline()
		{
			ResetWeights();
		}

		public Adaline(double a, double b)
		{
			_a = a;
			_b = b;
		}

		public double ValueAt(double x)
		{
			return x;
		}

		public void UpdateA(double correction)
		{
			_a += correction;
		}

		public void UpdateB(double correction)
		{
			_b += correction;
		}

		public void ResetWeights()
		{
			_a = MathHandler.Rand.NextDouble() * 2 - 1;
			_b = MathHandler.Rand.NextDouble() * 2 - 1;
		}

		public double[] GetWeights()
		{
			return new[] { _a, _b };
		}
	}
}