using CandidateTesting.SaraRego.ConsoleApp.Business.LogTreatment.Interfaces;
using CandidateTesting.SaraRego.ConsoleApp.Business.ReadMenu.Interface;
using CandidateTesting.SaraRego.ConsoleApp.Business.Validator;
using Moq;

namespace CandidateTesting.SaraRego.ConsoleAppTests
{
    public class ValidatorTests
    {
        private Validator _validator;
        private Mock<ILogProcessor> _logProcessorMock;
        private Mock<IReadMenu> _readMenuMock;

        string inputValue = "convert http://example.com/log.txt ./local/file.txt";

        public ValidatorTests()
        {
            _logProcessorMock = new Mock<ILogProcessor>();
            _readMenuMock = new Mock<IReadMenu>();

            _validator = new Validator(_logProcessorMock.Object, _readMenuMock.Object);
        }

        [Theory]
        [InlineData(true, 1)]
        [InlineData(false, 0)]
        public void Validator_ValidateTest(bool greenPath, int expectedTimes)
        {
            // Arrange
            var sourceUrl = inputValue.Split(" ")[1];
            var targetPath = inputValue.Split(" ")[2];

            if (!greenPath)
            {
                inputValue = inputValue.Replace("convert ", ""); //No "convert" command so it fails validation
            }

            // Act
            _validator.Validate(inputValue);

            // Assert
            _logProcessorMock.Verify(mock => mock.ProcessLog(sourceUrl, targetPath), Times.Exactly(expectedTimes));
            _readMenuMock.Verify(mock => mock.ReadInput(), Times.Once);
        }

        [Fact]
        public void Validator_Validate_TooManyErrors_ExitsProgram()
        {
            // Arrange
            var invalidInput = "invalid command";

            // Act
            for (int i = 0; i < 4; i++) //Exceeding max error count of 3
            {
                _validator.Validate(invalidInput);
            }

            // Assert
            _readMenuMock.Verify(mock => mock.ExitProgram(), Times.Once);
            _logProcessorMock.Verify(mock => mock.ProcessLog(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }
    }
}