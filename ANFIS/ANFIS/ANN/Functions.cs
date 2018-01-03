using System;
using System.Collections.Generic;
using ANFIS.ANN.Abstract;

namespace ANFIS.ANN
{
	public static class Functions
	{
		public static List<AFunction> GetFunctions()
		{
			return new List<AFunction> { new FuntionAlpha() };
		}
	}

	public class FuntionAlpha : AFunction
	{
		public override double Function(double[] variables)
		{
			if(variables.Length != 2)
				throw new ArgumentException("This function accepts only two parameters");
			double x = variables[0];
			double y = variables[1];
			return (Math.Pow(x - 1, 2) + Math.Pow(y + 2, 2) - 5 * x * y + 3) * Math.Pow(Math.Cos(x / 5), 2);
		}

		public override string ToString()
		{
			return "((x-1)^2 + (y+2)^2 - 5xy + 3) * cos(x/5)^2";
		}
	}
}