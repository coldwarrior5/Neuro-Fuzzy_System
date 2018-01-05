namespace ANFIS.Structures
{
	public class Sample
	{
		public double[] Variables { get; }
		public double Value { get; }

		public Sample(double[] variables, double value)
		{
			Variables = new double[variables.Length];
			for (int i = 0; i < variables.Length; i++)
				Variables[i] = variables[i];
			Value = value;
		}
	}
}