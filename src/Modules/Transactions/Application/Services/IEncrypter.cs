namespace BudgetUnderControl.Modules.Transactions.Application.Services
{
    public interface IEncrypter
    {
        string GetSalt();
        string GetHash(string value, string salt);
    }
}
