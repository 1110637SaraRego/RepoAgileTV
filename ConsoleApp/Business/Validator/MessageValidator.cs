using CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment.Interfaces;
using CandidateTesting.SaraRego.ConsoleApp.Business.ReadMenu.Interface;
using CandidateTesting.SaraRego.ConsoleApp.Business.Validator.Interface;

namespace CandidateTesting.SaraRego.ConsoleApp.Business.Validator
{
    public class MessageValidator : IMessageValidator
    {
        private static string[] delimiterValues = ["convert", " "];
        private string errorMessage = "\n Invalid input. Please try again.\n";


        public readonly ILogProcessor _logProcessor;
        public readonly IReadMenu _readMenu;
        public readonly IErrorValidator _errorCounter;

        public MessageValidator(ILogProcessor logProcessor, IReadMenu readMenu, IErrorValidator errorCounter)
        {
            _logProcessor = logProcessor;
            _readMenu = readMenu;
            _errorCounter = errorCounter;
        }

        public void Validate(string? inputvalue)
        {
            if (string.IsNullOrEmpty(inputvalue) || !inputvalue.Contains("convert"))
            {
                _errorCounter.ErrorCounter(errorMessage);
            }
            else
            {
                var parsedValue = inputvalue.Split(delimiterValues, StringSplitOptions.RemoveEmptyEntries);
                if (parsedValue.Length != 2)
                {
                    _errorCounter.ErrorCounter(errorMessage);
                }
                else
                {
                    _logProcessor.ProcessLog(parsedValue[0], parsedValue[1]);

                    Console.WriteLine($"\n||||| Log conversion finished!");
                    Console.WriteLine($"||||| Enter log command to convert or \"1\" to exit.");
                    _readMenu.ReadInput();
                }
            }
        }
    }
}
