using System.Collections.Generic;

namespace ANFIS.ANN.Interfaces
{
	public interface INeuronLayer
	{
		double[] GetOutputs(double[] inputs, double[] variables);
		void ResetLayer();
		void UpdateParameters(List<List<double>> correction);
		double[] GetParameters(int index);
		T GetFunction<T>(int index);
	}
}