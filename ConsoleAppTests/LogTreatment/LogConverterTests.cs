using CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment;

namespace CandidateTesting.SaraRego.ConsoleAppTests
{
    public class LogConverterTests
    {
        private LogConverter _logConverter;

        public LogConverterTests()
        {
            _logConverter = new LogConverter();
        }

        [Fact]
        public void LogProcessorTests_ConvertLog_InvalidFormat_ReturnsEmpty()
        {
            // Arrange
            var invalidLog = "312|200";

            // Act
            var result = _logConverter.ConvertLog(invalidLog);

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void LogProcessorTests_ConvertLog_ValidFormat_ReturnsConvertedLog()
        {
            // Arrange
            var validLog = "312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2";
            var expectedConvertedLog = "\"MINHA CDN\" GET 200 /robots.txt 100 312 HIT";

            // Act
            var result = _logConverter.ConvertLog(validLog);

            // Assert
            Assert.Contains(expectedConvertedLog, result);
        }
    }
}