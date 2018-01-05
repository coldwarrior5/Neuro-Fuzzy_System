using System;
using System.Collections.Generic;

namespace ANFIS.Handlers.Mathematics
{
	public static class MathHandler
	{
		public static readonly Random Rand = new Random();

		public static double ArithmeticMean(List<double> values)
		{
			double mean = 0;
			foreach (double t in values)
				mean += t;
			return mean / values.Count;
		}

		public static double AbsMax(List<double> values)
		{
			double max = 0;
			foreach (double t in values)
			{
				double temp = Math.Abs(t);
				if (temp > max)
					max = temp;
			}
			return max;
		}

		public static int Clamp(int value, int min, int max)
		{
			return value < min ? min : value > max ? max : value;
		}

		public static double Clamp(double value, double min, double max)
		{
			return value < min ? min : value > max ? max : value;
		}

		public static void IncrementRolloverArray(int[] starts, int[] ends, double[] tempVariableSet)
		{
			int numVariables = tempVariableSet.Length;
			if (numVariables != starts.Length || numVariables != ends.Length)
				throw new ArgumentException("There needs to exactly limits as are the variables.");

			bool rollover = false;
			for (int k = 0; k < numVariables; k++)
			{
				if (k != 0 && rollover == false)
					break;
				rollover = tempVariableSet[k] + 1 > ends[k];
				tempVariableSet[k] = (tempVariableSet[k] - starts[k] + 1) % (ends[k] - starts[k] + 1) + starts[k];
			}
		}
	}
}