using CandidateTesting.SaraRego.ConsoleApp.Business.ReadMenu.Interface;
using CandidateTesting.SaraRego.ConsoleApp.Business.Validator.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CandidateTesting.SaraRego.ConsoleApp.Business.ReadMenu
{
    public class ReadMenu : IReadMenu
    {
        private IHost? _host;

        public ReadMenu(IHost host)
        {
            _host = host;
        }

        public void Menu()
        {
            Console.WriteLine("\n\n||||| LogConvert");
            Console.WriteLine("\n||||| To convert a log, please type the respective command:");
            Console.WriteLine("||||| \"convert URLpath targetPath\"");
            Console.WriteLine("\n||||| Press \"1\" to exit.");

            ReadInput();
        }

        public void ReadInput()
        {
            Console.Write("\n - ");
            var input = Console.ReadLine();

            if (input == "1")
            {
                ExitProgram();
            }

            var validator = _host!.Services.GetRequiredService<IMessageValidator>();
            validator!.Validate(input);
        }

        public void ExitProgram()
        {
            Console.WriteLine("\n||||| Exiting the program. . .");
            Thread.Sleep(2000);
            Environment.Exit(0);
        }
    }
}
