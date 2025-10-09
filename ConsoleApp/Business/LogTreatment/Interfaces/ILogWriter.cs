namespace CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment.Interfaces
{
    public interface ILogWriter
    {
        void SaveLogToFile(string convertedLog, string targetPath);
    }
}
