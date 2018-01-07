using ANFIS.Handlers.Mathematics.Interface;

namespace ANFIS.Handlers.Mathematics.Abstract
{
	public abstract class AFunction : IFunction
	{
		public abstract double ValueAt(double[] variables);
		public abstract int GetNumVariables();
		public abstract override string ToString();
		public abstract string Name();
	}
}