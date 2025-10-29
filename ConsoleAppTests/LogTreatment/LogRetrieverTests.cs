using CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment;
using CandidateTesting.SaraRego.ConsoleApp.Business.Validator.Interface;
using Moq;
using Moq.Protected;
using System.Net;

namespace CandidateTesting.SaraRego.ConsoleAppTests
{
    public class LogRetrieverTests
    {
        private LogRetriever _logRetriever;
        private Mock<IErrorValidator> _errorValidator;
        private Mock<HttpMessageHandler> _httpMessageHandlerMock;

        string inputUrl = "http://example.com/log.txt";

        public LogRetrieverTests()
        {
            _errorValidator = new Mock<IErrorValidator>();
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            _logRetriever = new LogRetriever(new HttpClient(_httpMessageHandlerMock.Object), _errorValidator.Object);
        }

        [Fact]
        public void LogRetrieverTests_RetrieveLog_EmptyResponse_CallsErrorValidator()
        {
            // Arrange
            MockHttpClientResponse(inputUrl, "");

            // Act
            var result = _logRetriever.RetrieveLog(inputUrl);

            // Assert
            _errorValidator.Verify(mock => mock.ErrorCounter(It.IsAny<string>()), Times.Once);
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void LogRetrieverTests_RetrieveLog_ReturnsValidLog()
        {
            // Arrange
            var logContent = "312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2";
            MockHttpClientResponse(inputUrl, logContent);

            // Act
            var result = _logRetriever.RetrieveLog(inputUrl);

            // Assert
            _errorValidator.Verify(mock => mock.ErrorCounter(It.IsAny<string>()), Times.Never);
            Assert.Equal(logContent, result);
        }

        private void MockHttpClientResponse(string url, string responseContent)
        {
            _httpMessageHandlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.Is<HttpRequestMessage>(req =>
                     req.Method == HttpMethod.Get
                     && req.RequestUri == new Uri(url)
                  ),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(responseContent),
               })
               .Verifiable();

            var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("http://example.com/"),
            };

            _logRetriever = new LogRetriever(httpClient, _errorValidator.Object);
        }
    }
}