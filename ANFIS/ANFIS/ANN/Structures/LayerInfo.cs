﻿namespace ANFIS.ANN.Structures
{
	public struct LayerInfo
	{
		public int NumberOfNeurons { get; }
		public int InputSize { get; }
		public NeuronType Type { get; }

		public LayerInfo(int inputSize, int numberOfNeurons, NeuronType type)
		{
			InputSize = inputSize;
			NumberOfNeurons = numberOfNeurons;
			Type = type;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is LayerInfo))
				return false;

			LayerInfo mys = (LayerInfo)obj;
			return mys.InputSize == InputSize && mys.NumberOfNeurons == NumberOfNeurons && mys.Type == Type;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = NumberOfNeurons;
				hashCode = (hashCode * 397) ^ InputSize;
				hashCode = (hashCode * 397) ^ (int) Type;
				return hashCode;
			}
		}
	}
}