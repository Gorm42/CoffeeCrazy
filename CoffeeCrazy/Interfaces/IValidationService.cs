namespace CoffeeCrazy.Interfaces
{
    public interface IValidationService
    {
        Task<bool> CompareEmailsAsync(string userEmail);

        Task<List<string>> RetrieveAllEmails();
    }
}
