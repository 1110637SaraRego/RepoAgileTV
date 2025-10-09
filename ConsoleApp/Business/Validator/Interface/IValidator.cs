namespace CandidateTesting.SaraRego.ConsoleApp.Business.Validator.Interface
{
    public interface IValidator
    {
        void Validate(string? inputvalue);

        void ErrorValidator(string message);
    }
}
