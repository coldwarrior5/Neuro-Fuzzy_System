using System;
using ANFIS.ANN.Interfaces;
using ANFIS.Handlers.Mathematics;

// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace ANFIS.ANN.Functions
{
	public class Sigmoid : IActivationFunction
	{
		public double A { get; private set; }
		public double B { get; private set; }

		public Sigmoid()
		{
			ResetWeights();
		}

		public Sigmoid(double a, double b)
		{
			A = a;
			B = b;
		}

		public double ValueAt(double x)
		{
			double k = Math.Exp(B*(x - A));
			return k / (1.0f + k);
		}
		
		public void UpdateA(double correction)
		{
			A += correction;
		}

		public void UpdateB(double correction)
		{
			B += correction;
		}

		public void ResetWeights()
		{
			A = MathHandler.Rand.NextDouble();
			B = MathHandler.Rand.NextDouble();
		}

		public double[] GetWeights()
		{
			return new[] {A, B};
		}
	}

	public class Adaline : IActivationFunction
	{
		public double A { get; private set; }
		public double B { get; private set; }

		public Adaline()
		{
			ResetWeights();
		}

		public Adaline(double a, double b)
		{
			A = a;
			B = b;
		}

		public double ValueAt(double x)
		{
			return x;
		}

		public void UpdateA(double correction)
		{
			A += correction;
		}

		public void UpdateB(double correction)
		{
			B += correction;
		}

		public void ResetWeights()
		{
			A = MathHandler.Rand.NextDouble() * 2 - 1;
			B = MathHandler.Rand.NextDouble() * 2 - 1;
		}

		public double[] GetWeights()
		{
			return new[] { A, B };
		}
	}
}