using System;
using ANFIS.ANN.Interfaces;

// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace ANFIS.ANN
{
	public class Sigmoid : IActivationFunction
	{
		public double Function(double x)
		{
			double k = Math.Exp(x);
			return k / (1.0f + k);
			
		}

		public double Derivation(double x)
		{
			double k = Math.Exp(x);
			return k / Math.Pow(1.0f + k, 2);
			
		}
	}

	public class Adaline : IActivationFunction
	{
		public double Function(double x)
		{
			return x;
		}

		public double Derivation(double x)
		{
			return 1;
		}
	}
}