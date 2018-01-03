namespace ANFIS.ANN.Interfaces
{
	public interface IActivationFunction
	{
		double Function(double x);
		double Derivation(double x);
	}
}