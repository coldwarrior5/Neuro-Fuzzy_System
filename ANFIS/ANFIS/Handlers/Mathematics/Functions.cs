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
			return "((x-1)^2 + (y+2)^2 - 5xy + 3) * cos(x/5)^2";
		}

		public override string Name()
		{
			return "FunctionAlpha";
		}
	}
}