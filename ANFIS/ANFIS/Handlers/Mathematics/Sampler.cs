using System;
using System.Collections.Generic;
using ANFIS.Handlers.IO;
using ANFIS.Handlers.Mathematics.Interface;
using ANFIS.Structures;

namespace ANFIS.Handlers.Mathematics
{
	public static class Sampler
	{
		private const int HardcodedMin = -4;
		private const int HardcodedMax = 4;

		public static void HardcodedSampling(IFunction function, out Instance instance, Parser parser)
		{
			int numVariables = function.GetNumVariables();
			int[] starts = new int[numVariables];
			int[] ends = new int[numVariables];
			for (int i = 0; i < numVariables; i++)
			{
				starts[i] = HardcodedMin;
				ends[i] = HardcodedMax;
			}
			List<Sample> samples = SampleFunction(function, starts, ends);
			instance = new Instance(samples.Count, numVariables);
			instance.AddFunction(function.ToString());
			instance.AddSamples(samples);
			parser.FormatAndSaveResult(function.Name(), instance);
		}

		public static List<Sample> SampleFunction(IFunction function, int[] starts, int[] ends)
		{
			int numVariables = function.GetNumVariables();
			if(numVariables != starts.Length || numVariables != ends.Length)
				throw new ArgumentException("There needs to exactly limits as are the variables.");
			
			double[] tempVariableSet = new double[numVariables];
			for (int i = 0; i < numVariables; i++)
				tempVariableSet[i] = starts[i];

			List<Sample> set = new List<Sample>();
			set.Add(new Sample(tempVariableSet, function.ValueAt(tempVariableSet)));
			
			while (!LastCombination(ends, tempVariableSet))
			{
				MathHandler.IncrementRolloverArray(starts, ends, tempVariableSet);
				set.Add(new Sample(tempVariableSet, function.ValueAt(tempVariableSet)));
			}
			return set;
		}

		private static bool LastCombination(int[] ends, double[] tempSet)
		{
			for (int i = 0; i < ends.Length; i++)
			{
				if (Math.Abs(tempSet[i] - ends[i]) > double.Epsilon)
					return false;
			}
			return true;
		}
	}
}