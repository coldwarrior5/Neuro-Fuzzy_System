using System;

namespace ANFIS.Structures
{
	public class Instance
	{
		private int _index;
		public int NumSamples { get; }
		public int NumVariables { get; }
		public readonly Sample[] Samples;

		public Instance(int numSamples, int numVariables)
		{
			_index = 0;
			NumSamples = numSamples;
			NumVariables = numVariables;
			Samples = new Sample[NumSamples];
		}

		public void AddSample(double[] variables)
		{
			if(variables.Length != NumVariables)
				throw new Exception("There are too many defined variables.");
			if (_index + 1 > NumVariables)
				throw new Exception("There are too many samples.");
			Samples[_index++] = new Sample(variables);
		}

		protected bool Equals(Instance other)
		{
			return NumVariables == other.NumVariables && NumSamples == other.NumSamples;
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