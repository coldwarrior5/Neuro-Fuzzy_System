using System.Collections.Generic;

namespace ANFIS.Handlers.IO.Interfaces
{
	public interface IFileHandler
	{
		string[] ReadFile(string fileName, string extension);
		void SaveFile(string fileName, string extension, string[] outputBuffer);
		List<string> FileList(string extension);
	}
}