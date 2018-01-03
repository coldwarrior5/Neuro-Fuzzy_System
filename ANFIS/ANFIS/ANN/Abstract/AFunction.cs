using ANFIS.ANN.Interfaces;

namespace ANFIS.ANN.Abstract
{
	public abstract class AFunction : IFunction
	{
		public abstract double Function(double[] variables);
		public abstract override string ToString();
	}
}