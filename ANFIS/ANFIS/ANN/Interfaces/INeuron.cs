using System.Collections.Generic;

namespace ANFIS.ANN.Interfaces
{
	public interface INeuron
	{
		double GetOutput(double[] input, int position, double[] variables);
		void ResetWeights();
		void UpdateParameters(double[] correction);
		void UpdateParameters(List<double> correction);
		double[] GetParameters();
		IActivationFunction GetFunction();
	}
}