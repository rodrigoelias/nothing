using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using BusinessLayer.Repository;
using BusinessLayer.Services;
using Moq;
using NUnit.Framework;
using SharpTestsEx;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.Tests
{
    [TestFixture]
    public class TestUserCredentials
    {
        private Mock<IAccountRepository> _repositoryMock;
        private String _invalidPassword;
        private int _invalidAccount;
        private decimal _amountToDeposit;
        private AccountService _service;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IAccountRepository>();
            _invalidPassword = String.Empty;
            _invalidAccount = default(int);
            _amountToDeposit = default(decimal);

            _repositoryMock.Setup(it => it.CheckPassword(It.IsAny<int>(), It.IsAny<String>())).ReturnsAsync(false);
            _service = new AccountService(_repositoryMock.Object, null);
        }

        [Test]
        public async void Given_InvalidUserCredentials_Should_Throw_When_Depositing()
        {
            await ShouldEx.ThrowsAsync<InvalidCredentialsException>(async () =>
                await _service.Deposit(_invalidAccount, _invalidPassword, _amountToDeposit));
        }

        [Test]
        public async Task Given_InvalidUserCredentials_Should_Throw_When_Withdrawing()
        {
            await ShouldEx.ThrowsAsync<InvalidCredentialsException>(async () =>
                await _service.Withdraw(_invalidAccount, _invalidPassword, _amountToDeposit));
        }

        [Test]
        public async Task Given_InvalidUserCredentials_Should_Throw_When_ConsultingBalance()
        {
            await ShouldEx.ThrowsAsync<InvalidCredentialsException>(async () =>
                await _service.GetBalance(_invalidAccount, _invalidPassword));
        }
    }
}
