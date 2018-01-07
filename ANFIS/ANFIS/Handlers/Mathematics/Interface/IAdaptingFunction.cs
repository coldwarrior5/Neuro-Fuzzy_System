using System.Collections.Generic;

namespace ANFIS.Handlers.Mathematics.Interface
{
	public interface IAdaptingFunction
	{
		double ValueAt(double[] variables);
		double[] GetParameters();
		void UpdateParameters(double[] correction);
		void UpdateParameters(List<double> correction);
		void ResetParameters();
	}
}