using CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment.Interfaces;
using CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment.Wrapper;

namespace CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment
{
    public class LogWriter : ILogWriter
    {
        private string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private IFileWrapper _fileWrapper;

        public LogWriter(IFileWrapper fileWrapper)
        {
            _fileWrapper = fileWrapper;
        }

        public void SaveLogToFile(string convertedLog, string targetPath)
        {
            try
            {
                _fileWrapper.WriteAllText(targetPath, convertedLog);
            }
            catch (Exception)
            {
                ErrorFilePathLog(convertedLog);
            }
        }

        private void ErrorFilePathLog(string convertedLog)
        {
            var fileName = "/ConvertedLog_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
            _fileWrapper.WriteAllText(desktopPath + fileName, convertedLog);
            Console.WriteLine($"\n||||| Target path was not found. By default, new log was saved in: {desktopPath} as {fileName.Replace("/", "")}.\n");
        }
    }
}
