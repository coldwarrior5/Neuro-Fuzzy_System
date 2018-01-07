using System;
using System.Collections.Generic;
using System.IO;
using ANFIS.Handlers.IO.Interfaces;

namespace ANFIS.Handlers.IO
{
	public class FileHandler : IFileHandler
	{
		private readonly string _folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ANFIS");
		public const string FunctionExtension = ".ann";
		public const string ResultExtension = ".res";

		public FileHandler()
		{	
			if (!Directory.Exists(_folder))
				Directory.CreateDirectory(_folder);
		}

		public string[] ReadFile(string fileName, string extension)
		{
			var inputFileName = fileName + extension;
			var filePath = Path.Combine(_folder, inputFileName);
			return File.Exists(filePath) ? File.ReadAllLines(filePath) : null;
		}

		public void SaveFile(string fileName, string extension, string[] outputBuffer)
		{
			int i = 1;
			var num = "";
			string filePath;

			while (true)
			{
				var outputName = fileName + num + extension;
				filePath = Path.Combine(_folder, outputName);
				if (File.Exists(filePath))
				{
					num = i.ToString();
					i++;
					continue;
				}
				break;
			}
			File.WriteAllLines(filePath, outputBuffer);
		}

		public List<string> FileList(string extension)
		{
			List<string> retList = new List<string>();
			IEnumerator<string> iter = Directory.EnumerateFiles(_folder).GetEnumerator();
			while (iter.MoveNext())
			{
				if(iter.Current != null && Path.GetExtension(iter.Current).Equals(extension))
				{
					retList.Add(Path.GetFileNameWithoutExtension(iter.Current));
				}
			}
			iter.Dispose();
			return retList;
		}
	}
}