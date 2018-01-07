using System;
using System.Collections.Generic;
using ANFIS.Handlers.Mathematics;
using ANFIS.Handlers.Mathematics.Interface;

namespace ANFIS.Structures
{
	public class Instance
	{
		private int _index;
		public int NumSamples { get; }
		public int NumVariables { get; }
		public readonly Sample[] Samples;
		public IFunction OriginalFunction { get; private set; }

		public Instance(int numSamples, int numVariables)
		{
			_index = 0;
			NumSamples = numSamples;
			NumVariables = numVariables;
			Samples = new Sample[NumSamples];
		}

		public void AddFunction(string stringFunction)
		{
			OriginalFunction = Functions.GetFunction(stringFunction);
		}

		public void AddSample(double[] variables, double value)
		{
			if(variables.Length != NumVariables)
				throw new Exception("There are too many defined variables.");
			if (_index + 1 > NumSamples)
				throw new Exception("There are too many samples.");
			Samples[_index++] = new Sample(variables, value);
		}

		public void AddSamples(List<Sample> samples)
		{
			for (int i = 0; i < NumSamples; i++)
			{
				Samples[i] = samples[i];
			}
		}

		public override bool Equals(Object obj)
		{
			if (!(obj is Instance instance)) return false;
			var other = instance;
			bool mainCheck = NumVariables == other.NumVariables && NumSamples == other.NumSamples;
			if (!mainCheck) return false;
			for (int i = 0; i < NumSamples; i++)
			{
				if (Math.Abs(Samples[i].Value - other.Samples[i].Value) > Double.Epsilon)
					return false;
				for (int j = 0; j < NumVariables; j++)
				{
					if (Math.Abs(Samples[i].Variables[j] - other.Samples[i].Variables[j]) > Double.Epsilon)
						return false;
				}
			}
			return true;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = NumSamples;
				hashCode = (hashCode * 397) ^ NumVariables;
				return hashCode;
			}
		}
	}
}