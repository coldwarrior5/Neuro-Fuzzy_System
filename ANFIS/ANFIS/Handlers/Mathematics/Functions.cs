using System;
using System.Collections.Generic;
using ANFIS.Handlers.Mathematics.Abstract;
using ANFIS.Handlers.Mathematics.Interface;

namespace ANFIS.Handlers.Mathematics
{
	public static class Functions
	{
		public static List<IFunction> GetFunctions()
		{
			return new List<IFunction> { new FuntionAlpha() };
		}

		public static IFunction GetFunction(String name)
		{
			List<IFunction> functions = GetFunctions();
			foreach (IFunction function in functions)
			{
				if (function.ToString().Equals(name))
					return function;
			}
			return null;
		}
	}

	public class FuntionAlpha : AFunction
	{
		private int _numVariables = 2;
		public override double ValueAt(double[] variables)
		{
			if(variables.Length != _numVariables)
				throw new ArgumentException("This function accepts only two parameters");
			double x = variables[0];
			double y = variables[1];
			return (Math.Pow(x - 1, 2) + Math.Pow(y + 2, 2) - 5 * x * y + 3) * Math.Pow(Math.Cos(x / 5), 2);
		}

		public override int GetNumVariables()
		{
			return _numVariables;
		}

		public override string ToString()
		{
			return "((x-1)^2+(y+2)^2-5xy+3)*cos(x/5)^2";
		}

		public override string Name()
		{
			return "FunctionAlpha";
		}
	}

	public class SimpleFunction : IAdaptingFunction
	{
		private readonly int _numParameters;
		private readonly double[] _parameters;

		public SimpleFunction(int numParameters)
		{
			_numParameters = numParameters;
			_parameters = new double[numParameters];
			ResetParameters();
		}

		public double ValueAt(double[] variables)
		{
			if (variables.Length != _numParameters - 1)
				throw new ArgumentException("This function requires exactly " + (_numParameters - 1) + " input values.");

			double result = 0;
			for (int i = 0; i < variables.Length; i++)
				result += _parameters[i] * variables[i];
			result += _parameters[_numParameters - 1];
			return result;
		}

		public double[] GetParameters()
		{
			return (double[])_parameters.Clone();
		}

		public void UpdateParameters(double[] correction)
		{
			if (correction.Length != _numParameters)
				throw new ArgumentException("This function has exactly " + _numParameters + " parameters.");
			for (int i = 0; i < _numParameters; i++)
				_parameters[i] += correction[i];
		}

		public void UpdateParameters(List<double> correction)
		{
			if (correction.Count != _numParameters)
				throw new ArgumentException("This function has exactly " + _numParameters + " parameters.");
			for (int i = 0; i < _numParameters; i++)
				_parameters[i] += correction[i];
		}

		public void ResetParameters()
		{
			for (int i = 0; i < _numParameters; i++)
				_parameters[i] = MathHandler.Rand.NextDouble() * 2 - 1;
		}
	}
}