using ANFIS.Structures;

namespace ANFIS.Handlers.IO.Interfaces
{
	public interface IParser
	{
		Instance ParseData(string fileName);
		void FormatAndSaveResult(string fileName, Instance result);
	}
}