using System;

namespace ANFIS.Handlers
{
    public enum ErrorCode
    {
	    UserTermination = 0,
		InvalidNumInputParameters,
        InvalidInputParameter,
        NoSuchFile,
        ImproperLine,
	    NotEnoughSamples,
	    TooManySamples
	}
    
    public static class ErrorHandler
    {
        public static void TerminateExecution(ErrorCode code, string explanation = "")
        {
            Environment.Exit((int) code);
        }

	    // ReSharper disable once UnusedMember.Local
        private static string ErrorMessage(ErrorCode code)
        {
            string explanation;
            
            switch (code)
            {
                case ErrorCode.InvalidNumInputParameters:
                    explanation = "Invalid number of input parameters.";
                    break;
                case ErrorCode.UserTermination:
                    explanation = "User has terminated the application.";
                    break;
                case ErrorCode.NoSuchFile:
                    explanation = "There is no such file.";
                    break;
                case ErrorCode.InvalidInputParameter:
                    explanation = "Invalid input parameter.";
                    break;
                case ErrorCode.ImproperLine:
                    explanation = "File contains irregular line.";
                    break;
	            case ErrorCode.NotEnoughSamples:
		            explanation = "There is not enough defined samples.";
					break;
	            case ErrorCode.TooManySamples:
		            explanation = "There is too many defined samples.";
					break;
	            default:
                    throw new ArgumentException("Such error is non existant.");
            }
            return explanation;
        }
    }
}