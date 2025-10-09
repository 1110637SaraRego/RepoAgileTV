using CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment.Interfaces;
using CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment.Wrapper;
using CandidateTesting.SaraRego.ConsoleApp.Business.ReadMenu.Interface;
using CandidateTesting.SaraRego.ConsoleApp.Business.Validator.Interface;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CandidateTesting.SaraRego.ConsoleApp.Business.Main
{
    public class Program
    {
        private static IHost? host;

        static void Main(string[] args)
        {
            host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddHttpClient();
                services.AddSingleton<IFileWrapper, FileWrapper>();
                services.AddSingleton<IValidator, Validator.Validator>();
                services.AddSingleton<ILogProcessor, LogTreatment.LogProcessor>();
                services.AddSingleton<ILogConverter, LogTreatment.LogConverter>();
                services.AddSingleton<ILogRetriever, LogTreatment.LogRetriever>();
                services.AddSingleton<ILogWriter, LogTreatment.LogWriter>();
                services.AddSingleton<IReadMenu, ReadMenu.ReadMenu>();
            })
            .Build();

            var ReadMenu = new ReadMenu.ReadMenu(host);
            ReadMenu.Menu();
        }
    }
}
