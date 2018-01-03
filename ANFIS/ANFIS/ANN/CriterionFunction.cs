using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable SuggestBaseTypeForParameter
// ReSharper disable LoopCanBeConvertedToQuery
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace ANFIS.ANN
{
	public static class CriterionFunction
	{
		public static double[] EvaluatePerSymbol(double[][] givenOutputs, int[][] expectedOutputs)
		{
			if (givenOutputs.Length != expectedOutputs.Length)
				throw new Exception("Arrays must be of same size!");

			double[] perSymbolError = new double[givenOutputs[0].Length];

			for (int i = 0; i < givenOutputs[0].Length; i++)
			{
				for (int j = 0; j < givenOutputs.Length; j++)
				{
					perSymbolError[i] += Math.Pow(expectedOutputs[j][i] - givenOutputs[j][i], 2) / (2 * givenOutputs.Length);
				}
			}
			return perSymbolError;
		}

		[SuppressMessage("ReSharper", "UnusedMember.Global")]
		public static double Evaluate(double[][] givenOutputs, int[][] expectedOutputs)
		{
			if (givenOutputs.Length != expectedOutputs.Length)
				throw new Exception("Arrays must be of same size!");

			double error = 0;
			for (int i = 0; i < givenOutputs.Length; i++)
			{
				error += EvaluateSingleSample(givenOutputs[i], expectedOutputs[i]);
			}
			return error/(2 * givenOutputs.Length);
		}

		private static double EvaluateSingleSample(double[] givenOutputs, int[] expectedOutputs)
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
}