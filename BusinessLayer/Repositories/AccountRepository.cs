using BusinessLayer.Models;
using BusinessLayer.Repositories;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Transactions;

namespace BusinessLayer.Repository
{
    public class AccountRepository : IAccountRepository
    {
        public async Task<int> InsertAccount(Account newAccount)
        {
            using (var context = new BaseContext())
            {
                context.Accounts.Add(newAccount);
                await context.SaveChangesAsync();
                return newAccount.Id;
            }
        }

        public async Task IncrementBalance(int accountId, decimal amount)
        {
            using (var context = new BaseContext())
            {
                await context.Database.Connection.OpenAsync();
                using (var transaction = context.Database.Connection.BeginTransaction())
                {
                    var accountObj = await context.Accounts.FirstOrDefaultAsync(account => accountId == account.Id);
                    accountObj.Balance += amount;
                    context.Entry(accountObj).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    transaction.Commit();
                }
                context.Database.Connection.Close();
            }
        }

        public Task<bool> CheckPassword(int accountId, string password)
        {
            using (var context = new BaseContext())
            {
                return context.Accounts.AnyAsync(a=> a.Password == password && a.Id == accountId);
            }
        }

        public async Task DecrementBalance(int accountId, decimal amount)
        {
            using (var context = new BaseContext())
            {
                await context.Database.Connection.OpenAsync();
                using (var transaction = context.Database.Connection.BeginTransaction())
                {
                    var accountObj = await context.Accounts.FirstOrDefaultAsync(account => accountId == account.Id);
                    accountObj.Balance -= amount;
                    context.Entry(accountObj).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    transaction.Commit();
                }
                context.Database.Connection.Close();
            }
        }
        public Task<Account> GetBalance(int accountId)
        {
            using (var context = new BaseContext())
            {
                return context.Accounts.FirstOrDefaultAsync(a => a.Id == accountId);
            }
        }
    }
}
