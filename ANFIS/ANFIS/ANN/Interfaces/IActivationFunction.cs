namespace ANFIS.ANN.Interfaces
{
	public interface IActivationFunction
	{
		double ValueAt(double x);
		void ResetWeights();
		double[] GetWeights();
	}
}