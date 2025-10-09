using CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment.Interfaces;
using CandidateTesting.SaraRego.ConsoleApp.Business.ReadMenu.Interface;
using CandidateTesting.SaraRego.ConsoleApp.Business.Validator.Interface;

namespace CandidateTesting.SaraRego.ConsoleApp.Business.Validator
{
    public class Validator : IValidator
    {
        private static string[] delimiterValues = ["convert", " "];
        private int errorCount = 0;
        private int errorMax = 3;
        private string errorMessage = "\n Invalid input. Please try again.\n";


        public readonly ILogProcessor _logProcessor;
        public readonly IReadMenu _readMenu;

        public Validator(ILogProcessor logProcessor, IReadMenu readMenu)
        {
            _logProcessor = logProcessor;
            _readMenu = readMenu;
        }

        public void Validate(string? inputvalue)
        {
            if (string.IsNullOrEmpty(inputvalue) || !inputvalue.Contains("convert"))
            {
                ErrorValidator(errorMessage);
            }
            else
            {
                var parsedValue = inputvalue.Split(delimiterValues, StringSplitOptions.RemoveEmptyEntries);
                if (parsedValue.Length != 2)
                {
                    ErrorValidator(errorMessage);
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

        public void ErrorValidator(string message)
        {
            errorCount++;
            if (errorCount > errorMax)
            {
                Console.WriteLine("\n Too many invalid attempts.");
                _readMenu.ExitProgram();
            }

            Console.WriteLine(errorMessage);
            _readMenu.ReadInput();
        }
    }
}
