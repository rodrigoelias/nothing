using System.Threading.Tasks;
namespace BusinessLayer.Services
{
    public interface IAccountService
    {
        Task<int> CreateNewAccount(string customerName, string initialPassword);

        Task<bool> Deposit(int accountId, string password, decimal value);

        Task CheckCredentials(int accountId, string password);

        Task<bool> Withdraw(int accountId, string password, decimal value);

        Task<decimal> GetBalance(int accountId, string password);
    }
}
