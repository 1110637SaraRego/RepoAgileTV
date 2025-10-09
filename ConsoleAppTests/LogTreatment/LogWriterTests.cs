using CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment;
using CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment.Wrapper;
using Moq;

namespace CandidateTesting.SaraRego.ConsoleAppTests
{
    public class LogWriterTests
    {
        private LogWriter _logWriter;
        private Mock<IFileWrapper> _fileWrapper;

        private string targetPath = "./local/file.txt";
        private string convertedLog = "\"MINHA CDN\" GET 200 /robots.txt 100 312 HIT";

        public LogWriterTests()
        {
            _fileWrapper = new Mock<IFileWrapper>();
        }

        [Fact]
        public void LogProcessorTests_GreenPath()
        {
            // Arrange
            _fileWrapper.Setup(mock => mock.WriteAllText(targetPath, convertedLog));
            _logWriter = new LogWriter(_fileWrapper.Object);

            // Act
            _logWriter.SaveLogToFile(convertedLog, targetPath);

            // Assert
            _fileWrapper.Verify(mock => mock.WriteAllText(targetPath, convertedLog), Times.Once);
        }

        [Fact]
        public void LogProcessorTests_InvalidTargetPath_WritesInDefaultPath()
        {
            // Arrange
            _fileWrapper.Setup(mock => mock.WriteAllText(targetPath, convertedLog)).Throws(new Exception());
            _logWriter = new LogWriter(_fileWrapper.Object);
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            // Act
            _logWriter.SaveLogToFile(convertedLog, targetPath);

            // Assert
            _fileWrapper.Verify(mock => mock.WriteAllText(targetPath, convertedLog), Times.Once);
            _fileWrapper.Verify(mock => mock.WriteAllText(It.Is<string>(s => s.StartsWith(desktopPath)), convertedLog), Times.Once);
        }
    }
}