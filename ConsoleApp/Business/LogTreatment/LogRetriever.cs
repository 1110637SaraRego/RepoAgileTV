using CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment.Interfaces;
using CandidateTesting.SaraRego.ConsoleApp.Business.Validator.Interface;

namespace CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment
{
    public class LogRetriever : ILogRetriever
    {
        private HttpClient _client;
        private IValidator _validator;

        private string errorMessage = "\nError retrieving the log file.\nPlease try a different url path.";

        public LogRetriever(HttpClient client, IValidator validator)
        {
            _client = client;
            _validator = validator;
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
            _validator.ErrorValidator(errorMessage);
        }
    }
}
