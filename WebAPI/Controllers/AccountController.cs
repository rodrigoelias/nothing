using BusinessLayer.Services;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class AccountController : ApiController
    {
        private IAccountService _service;

        public AccountController()
        {
            _service = new AccountService();
        }

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("Account/GetBalance/{accountId}/{password}")]
        public async Task<decimal> GetBalance(int accountId, string password)
        {
            return await _service.GetBalance(accountId, password);
        }

        [HttpPut]
        [Route("Account/Deposit/{accountId}/{password}/{amount:decimal}/")]
        public async void Deposit(int accountId, string password, decimal amount)
        {
            await _service.Deposit(accountId, password, amount);
        }

        [HttpPut]
        [Route("Account/Withdraw/{accountId}/{password}/{amount:decimal}/")]
        public async void Withdraw(int accountId, string password, decimal amount)
        {
            await _service.Withdraw(accountId, password, amount);
        }

        [HttpPost]
        [Route("Account/Create/{customerName}/{initialPassword}")]
        public async Task<int> Create(string customerName, string initialPassword)
        {
            var accountNumber = await _service.CreateNewAccount(customerName, initialPassword);
            return accountNumber;
        }
    }
}
