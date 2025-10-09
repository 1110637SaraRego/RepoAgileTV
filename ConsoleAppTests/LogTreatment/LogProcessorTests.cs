using CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment;
using CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment.Interfaces;
using CandidateTesting.SaraRego.ConsoleApp.Business.ReadMenu.Interface;
using Moq;

namespace CandidateTesting.SaraRego.ConsoleAppTests
{
    public class LogProcessorTests
    {
        private LogProcessor _logProcessor;
        private Mock<ILogRetriever> _logRetrieverMock;
        private Mock<ILogConverter> _logConverterMock;
        private Mock<ILogWriter> _logWriterMock;
        private Mock<IReadMenu> _readMenuMock;

        public LogProcessorTests()
        {
            _logRetrieverMock = new Mock<ILogRetriever>();
            _logConverterMock = new Mock<ILogConverter>();
            _logWriterMock = new Mock<ILogWriter>();
            _readMenuMock = new Mock<IReadMenu>();
            _logProcessor = new LogProcessor(_logRetrieverMock.Object, _logConverterMock.Object, _logWriterMock.Object, _readMenuMock.Object);
        }

        [Fact]
        public void LogProcessorTests_AllLogMethodsAreCalled_GreenPath()
        {
            // Arrange
            var inputUrl = "http://example.com/log.txt";
            var targetPath = "./local/file.txt";
            var retrievedLog = "312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2";
            var convertedLog = "\"MINHA CDN\" GET 200 /robots.txt 100 312 HIT";

            _logRetrieverMock.Setup(mock => mock.RetrieveLog(inputUrl)).Returns(retrievedLog);
            _logConverterMock.Setup(mock => mock.ConvertLog(retrievedLog)).Returns(convertedLog);

            // Act
            _logProcessor.ProcessLog(inputUrl, targetPath);

            // Assert
            _logRetrieverMock.Verify(mock => mock.RetrieveLog(inputUrl), Times.Once);
            _logConverterMock.Verify(mock => mock.ConvertLog(retrievedLog), Times.Once);
            _logWriterMock.Verify(mock => mock.SaveLogToFile(convertedLog, targetPath), Times.Once);

            _readMenuMock.Verify(mock => mock.ReadInput(), Times.Never);
        }

        [Fact]
        public void LogProcessorTests_InvalidLogFormat_ReadInputIsCalled()
        {
            // Arrange
            var inputUrl = "http://example.com/log.txt";
            var targetPath = "./local/file.txt";
            var retrievedLog = "312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2";
            var convertedLog = "";

            _logRetrieverMock.Setup(mock => mock.RetrieveLog(inputUrl)).Returns(retrievedLog);
            _logConverterMock.Setup(mock => mock.ConvertLog(retrievedLog)).Returns(convertedLog);

            // Act
            _logProcessor.ProcessLog(inputUrl, targetPath);

            // Assert
            _logRetrieverMock.Verify(mock => mock.RetrieveLog(inputUrl), Times.Once);
            _logConverterMock.Verify(mock => mock.ConvertLog(retrievedLog), Times.Once);
            _logWriterMock.Verify(mock => mock.SaveLogToFile(It.IsAny<string>(), It.IsAny<string>()), Times.Never);

            _readMenuMock.Verify(mock => mock.ReadInput(), Times.Once);
        }
    }
}