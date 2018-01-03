using System.Collections.Generic;

namespace ANFIS.ANN.Interfaces
{
	public interface INeuron
	{
		double GetOutput(double[] x);
		void Reset();
		void ApplyChange(List<double> getRange);
		double Backpropagation(double[] inputs, double[] outputs, double[] desired, ref List<double> changes, int j);

		double Backpropagation(double[] inputs, double[] outputs, double[] deltaNext, Neuron[] neurons,
			ref List<double> changes, int j);

	}
}