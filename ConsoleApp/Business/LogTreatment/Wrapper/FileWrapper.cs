namespace CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment.Wrapper
{
    public class FileWrapper : IFileWrapper
    {
        public void WriteAllText(string path, string contents)
        {
            File.WriteAllText(path, contents);
        }
    }
}
