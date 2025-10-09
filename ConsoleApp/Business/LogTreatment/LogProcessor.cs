using CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment.Interfaces;
using CandidateTesting.SaraRego.ConsoleApp.Business.ReadMenu.Interface;

namespace CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment
{
    public class LogProcessor : ILogProcessor
    {
        private ILogRetriever _logRetriever;
        private ILogConverter _logConverter;
        private ILogWriter _logWriter;
        private IReadMenu _readMenu;

        public LogProcessor(ILogRetriever logRetriever, ILogConverter logConverter, ILogWriter logWriter, IReadMenu readMenu)
        {
            _logRetriever = logRetriever;
            _logConverter = logConverter;
            _logWriter = logWriter;
            _readMenu = readMenu;
        }

        public void ProcessLog(string urlPath, string targetPath)
        {
            var log = _logRetriever.RetrieveLog(urlPath);

            if(string.IsNullOrEmpty(log))
            {
                EmptyResponse();
                return;
            }

            Console.WriteLine("||||| Processing the log...");
            var convertedlog = _logConverter.ConvertLog(log);

            if (string.IsNullOrEmpty(convertedlog))
            {
                EmptyResponse();
                return;
            }

            Console.WriteLine($"\n||||| Saving new log in {targetPath}");
            _logWriter.SaveLogToFile(convertedlog, targetPath);
        }

        private void EmptyResponse()
        {
            Console.WriteLine("\n Invalid log format. Please check with source or try a different url.\n");
            _readMenu.ReadInput();
        }
    }
}
