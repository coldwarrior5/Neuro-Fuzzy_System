using System;
using System.Collections.Generic;

namespace ANFIS.Handlers
{
	public static class MathHandler
	{
		public static Random Rand = new Random();

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

		public static void FindDivisor(int number, out int numBatches, out int perBatchElements)
		{
			int[] divisors = {2, 3, 5, 7, 11, 13};
			numBatches = -1;
			perBatchElements = -1;

			foreach (int divisor in divisors)
			{
				numBatches = (int) Math.DivRem(number, divisor, out long remainder);
				if (remainder != 0)
				{
					numBatches = number;
					perBatchElements = 1;
					continue;
				}
				perBatchElements = divisor;
				break;
			}
		}
	}
}