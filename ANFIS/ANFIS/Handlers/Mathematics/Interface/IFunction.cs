namespace ANFIS.Handlers.Mathematics.Interface
{
	public interface IFunction
	{
		double ValueAt(double[] variables);
		int GetNumVariables();
		string ToString();
		string Name();
	}
}