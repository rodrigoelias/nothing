using BusinessLayer.Models;
using System.Threading.Tasks;

namespace BusinessLayer.Repository
{
    public interface IAccountRepository
    {
        Task<int> InsertAccount(Account newAccount);

        Task IncrementBalance(int accountId, decimal amount);

        Task<bool> CheckPassword(int accountId, string password);

        Task DecrementBalance(int accountId, decimal amount);

        Task<Account> GetBalance(int accountId);
    }
}
