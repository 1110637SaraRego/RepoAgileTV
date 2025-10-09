using CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment.Interfaces;
using CandidateTesting.SaraRego.ConsoleApp.Model;
using System.Text;
using System.Text.RegularExpressions;

namespace CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment
{
    public class LogConverter : ILogConverter
    {
        private readonly LogModel logModel;
        private static string[] delimiterValues = ["|", "\""];
        private static string regexPattern = @"\w* \/";

        public LogConverter()
        {
            logModel = new LogModel();
        }

        public string ConvertLog(string originalLog)
        {
            var convertedLog = CreateLogHeader();
            try
            {
                foreach (var line in originalLog.Split('\n', StringSplitOptions.RemoveEmptyEntries))
                {
                    var convertedLine = ConvertLogLine(line);

                    convertedLog.AppendLine(convertedLine);
                }
            }
            catch (FormatException)
            {
                convertedLog.Clear();
            }

            return convertedLog.ToString();
        }

        private StringBuilder CreateLogHeader()
        {
            var header = new StringBuilder();

            header.AppendLine("#Version: " + logModel.LogVersion);
            header.AppendLine("#Date: " + logModel.Date);
            header.AppendLine("#Fields: " + addFields());
            return header;
        }

        private string addFields()
        {
            var fields = new StringBuilder();
            for (int i = 0; i < logModel.Fields.Length; i++)
            {
                fields.Append(logModel.Fields[i]);
                if (i < logModel.Fields.Length - 1)
                {
                    fields.Append(" ");
                }
            }
            return fields.ToString();
        }

        private string ConvertLogLine(string line)
        {
            var logParts = line.Split(delimiterValues, StringSplitOptions.RemoveEmptyEntries);

            if (logParts.Length < 5)
            {
                LogWrongFormat(line);
                throw new FormatException("Log line does not contain enough parts.");
            }

            var responseSize = logParts[0];
            var statusCode = logParts[1];
            var cacheStatus = logParts[2];

            // Validate cache status type - Default to MISS if invalid; If INVALIDATE, change to REFRESH_HIT
            if (!Enum.TryParse<LogModel.CacheStatus>(cacheStatus, out _))
            {
                cacheStatus = LogModel.CacheStatus.MISS.ToString();
            }
            if (cacheStatus == LogModel.CacheStatus.INVALIDATE.ToString())
            {
                cacheStatus = "REFRESH_HIT";
            }

            // Extract HTTP method and request path
            var regex = new Regex(regexPattern);
            var httpRequest = Regex.Replace(logParts[3], regexPattern, "/").Trim().Split(" ").First();
            var httpMethod = logParts[3].Split(" /").First();

            // Extract and round time taken
            var timeValue = logParts[4].Trim().Replace(".", ",", StringComparison.InvariantCulture);
            var timetaken = Math.Round(Convert.ToDecimal(timeValue));

            var newLogLine = logModel.Provider + " " + httpMethod + " " + statusCode + " "
                + httpRequest + " " + timetaken + " " + responseSize + " " + cacheStatus;

            return newLogLine;
        }

        private void LogWrongFormat(string line)
        {
            Console.WriteLine("||||| Original log line: " + line + "\n");
            Console.WriteLine("\n\n");
        }
    }
}