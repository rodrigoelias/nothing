using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using BusinessLayer.Repository;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class AccountService : IAccountService
    {
        private IAccountRepository _repository;
        private ICustomerService _customerService;

        public AccountService(IAccountRepository repository, ICustomerService customerService)
        {
            _repository = repository;
            _customerService = customerService;
        }

        public async Task CheckCredentials(int accountId, string password)
        {
            var isVerified = await _repository.CheckPassword(accountId, password);
            if (isVerified == false)
            {
                throw new InvalidCredentialsException();
            }
        }

        public async Task<bool> Deposit(int accountId, string password, decimal value)
        {
            await CheckCredentials(accountId, password);
            await _repository.IncrementBalance(accountId, value);
            return true;
        }

        public Task<int> CreateNewAccount(string customerName, string initialPassword)
        {
            if (ValidateName(customerName) == false)
            {
                throw new InvalidCustomerNameException();
            }
            if (ValidatePassword(initialPassword) == false)
            {
                throw new InvalidPasswordException();
            }
            var newCustomer = _customerService.CreateCustomer(customerName);
            var newAccount = new Account() { Balance = 0.0M, Customer = newCustomer, Password = initialPassword };
            return _repository.InsertAccount(newAccount);
        }

        private bool ValidateName(string customerName)
        {
            return string.IsNullOrWhiteSpace(customerName) == false;
        }

        private bool ValidatePassword(string password)
        {
            return string.IsNullOrWhiteSpace(password) == false;
        }

        public async Task<bool> Withdraw(int accountId, string password, decimal value)
        {
            await CheckCredentials(accountId, password);
            await _repository.DecrementBalance(accountId, value);
            return true;
        }

        public async Task<decimal> GetBalance(int accountId, string password)
        {
            await CheckCredentials(accountId, password);
            var account = await _repository.GetBalance(accountId);
            return account.Balance;
        }
    }
}
