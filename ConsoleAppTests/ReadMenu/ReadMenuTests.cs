using CandidateTesting.SaraRego.ConsoleApp.Business.ReadMenu;
using CandidateTesting.SaraRego.ConsoleApp.Business.Validator.Interface;
using Microsoft.Extensions.Hosting;
using Moq;

namespace CandidateTesting.SaraRego.ConsoleAppTests
{
    public class ReadMenuTests
    {
        private ReadMenu readMenu;
        private Mock<IHost> _hostMock;
        private Mock<IMessageValidator> _validator;

        string inputValue = "convert http://example.com/log.txt ./local/file.txt";

        public ReadMenuTests()
        {
            _hostMock = new Mock<IHost>();
            _validator = new Mock<IMessageValidator>();
            _hostMock.Setup(mock => mock.Services.GetService(typeof(IMessageValidator))).Returns(_validator.Object);

            readMenu = new ReadMenu(_hostMock.Object);
        }

        [Fact]
        public void ReadMenu_ReadInputTest()
        {
            // Arrange
            var input = new StringReader(inputValue);
            Console.SetIn(input);

            // Act
            readMenu.ReadInput();

            // Assert
            _validator.Verify(mock => mock.Validate(inputValue), Times.Once);
        }
    }
}