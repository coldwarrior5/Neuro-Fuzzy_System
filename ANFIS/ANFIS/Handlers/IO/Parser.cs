using System.Text.RegularExpressions;
using ANFIS.Handlers.Error;
using ANFIS.Handlers.IO.Interfaces;
using ANFIS.Structures;

namespace ANFIS.Handlers.IO
{
	public class Parser : IParser
	{
		private const string Comment = "%%";
		private const string InstanceInfo = "% ";
		private const string EmptyLine = "";
		private const string Sample = "sample";
		
		private const string SamplesInFile = "Samples";
		private const string VariablesPerSample = "Per sample variables";

		private readonly FileHandler _fileHandler;
		private Instance _instance;
		
		private int _numSamples;
		private int _numVariables;
		
		private int _iSamples;

		public Parser()
		{
			_fileHandler = new FileHandler();
			_iSamples = 0;
		}

		public Instance ParseData(string fileName)
		{
			var data = _fileHandler.ReadFile(fileName);
			for (var i = 0; i < data.Length; i++)
			{
				ParseLine(data[i], i + 1);
			}
			CheckParameters();
			return _instance;
		}

		public void FormatAndSaveResult(string fileName, Instance result)
		{
			_fileHandler.SaveFile(fileName, FormatData(result));
		}

		private static string[] FormatData(Instance input)
		{
			const int displacement = 3;
			string[] result = new string[input.NumSamples + displacement];

			result[0] = InstanceInfo + SamplesInFile + " " + input.NumSamples;
			result[1] = InstanceInfo + VariablesPerSample + " " + input.NumVariables;
			result[2] = EmptyLine;

			for (var i = 0; i < input.NumSamples; i++)
			{
				result[i + displacement] = Sample + " [";
				for (var j = 0; j < input.NumVariables; j++)
				{
					result[i + displacement] += "'" + input.Samples[i].Variables[j].ToString("G17");
					if (j != input.NumVariables - 1)
						result[i + displacement] += "',";
					else
						result[i + displacement] += "'], ";
				}
				result[i + displacement] += "['" + input.Samples[i].Value + "']";
			}
			return result;
		}

		private void ParseLine(string line, int position)
		{
			if (line.StartsWith(Comment) || line is EmptyLine)
			{
				Instance temp = new Instance(_numSamples, _numVariables);
				if (_instance is null || !_instance.Equals(temp))
					_instance = new Instance( _numSamples, _numVariables);
				return;
			}
				
			if (line.StartsWith(InstanceInfo))
				ParseInstance(line);
			else if (line.StartsWith(Sample))
				ParseSample(line, position);
			else
				ErrorHandler.TerminateExecution(ErrorCode.ImproperLine, "Line " + position + " is not valid.");
		}

		private void ParseInstance(string line)
		{
			var splits = line.Split(' ');
			if (line.Contains(SamplesInFile))
			{
				var success = int.TryParse(splits[splits.Length - 1], out _numSamples);
				if (!success)
					ErrorHandler.TerminateExecution(ErrorCode.ImproperLine);
			}
			else if (line.Contains(VariablesPerSample))
			{
				var success = int.TryParse(splits[splits.Length - 1], out _numVariables);
				if (!success)
					ErrorHandler.TerminateExecution(ErrorCode.ImproperLine);
			}
		}

		private void ParseSample(string line, int position)
		{
			double[] variables = new double[_numVariables];
			double[] value = new double[1];

			if (_iSamples++ >= _numSamples)
				ErrorHandler.TerminateExecution(ErrorCode.TooManySamples);

			var splits = line.Split(' ');
			if(FillDoubleArray(variables, splits[1], position))
				ErrorHandler.TerminateExecution(ErrorCode.ImproperLine, "Line " + position + " must have " + _numVariables + " variables in total.");
			if (FillDoubleArray(value, splits[2], position))
				ErrorHandler.TerminateExecution(ErrorCode.ImproperLine, "Line " + position + " must have defined value for the sample.");
			_instance.AddSample(variables, value[0]);
		}

		private static bool FillDoubleArray(double[] values, string line, int position)
		{
			int i = -1;
			if (line != null)
			{
				var matches = Regex.Matches(line, "[-0-9.]+");
				var instances = matches.GetEnumerator();
				while (instances.MoveNext())
				{
					if (i++ > values.Length)
						break;
					if (instances.Current == null) continue;
					double.TryParse(instances.Current.ToString(), out values[i]);
				}
			}
			else
				ErrorHandler.TerminateExecution(ErrorCode.ImproperLine, "Line " + position + " does not contain x or y values.");
			return i == values.Length;
		}

		private void CheckParameters()
		{
			if (_iSamples < _numSamples - 1)
				ErrorHandler.TerminateExecution(ErrorCode.NotEnoughSamples);
		}
	}
}