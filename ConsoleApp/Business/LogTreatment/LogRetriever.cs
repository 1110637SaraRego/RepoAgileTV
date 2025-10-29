using CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment.Interfaces;
using CandidateTesting.SaraRego.ConsoleApp.Business.Validator.Interface;

namespace CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment
{
    public class LogRetriever : ILogRetriever
    {
        private HttpClient _client;
        private IErrorValidator _errorValidator;

        private string errorMessage = "\nError retrieving the log file.\nPlease try a different url path.";

        public LogRetriever(HttpClient client, IErrorValidator errorValidator)
        {
            _client = client;
            _errorValidator = errorValidator;
        }

        public string RetrieveLog(string urlPath)
        {
            string logValueTask = "";

            try
            {
                logValueTask = _client.GetStringAsync(urlPath).Result;
            }
            catch (AggregateException)
            {
                ErrorRetrieveLog();
                return string.Empty;
            }

            if (string.IsNullOrEmpty(logValueTask))
            {
                ErrorRetrieveLog();
                return string.Empty;
            }

            Console.WriteLine("\n||||| Retrieved the following log:\n");
            Console.WriteLine(logValueTask);

            return logValueTask;
        }

        private void ErrorRetrieveLog()
        {
            _errorValidator.ErrorCounter(errorMessage);
        }
    }
}
