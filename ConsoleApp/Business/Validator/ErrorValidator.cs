using CandidateTesting.SaraRego.ConsoleApp.Business.ReadMenu.Interface;
using CandidateTesting.SaraRego.ConsoleApp.Business.Validator.Interface;

namespace CandidateTesting.SaraRego.ConsoleApp.Business.Validator
{
    public class ErrorValidator : IErrorValidator
    {
        private int errorCount = 0;
        private int errorMax = 3;

        public readonly IReadMenu _readMenu;

        public ErrorValidator(IReadMenu readMenu)
        {
            _readMenu = readMenu;
        }

        public void ErrorCounter(string message)
        {
            errorCount++;
            if (errorCount > errorMax)
            {
                Console.WriteLine("\n Too many invalid attempts.");
                _readMenu.ExitProgram();
            }

            Console.WriteLine(message);
            _readMenu.ReadInput();
        }
    }
}
