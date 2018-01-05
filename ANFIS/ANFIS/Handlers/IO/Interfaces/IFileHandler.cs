namespace ANFIS.Handlers.IO.Interfaces
{
	public interface IFileHandler
	{
		string[] ReadFile(string fileName);
		void SaveFile(string fileName, string[] outputBuffer);
	}
}