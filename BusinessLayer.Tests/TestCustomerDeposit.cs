using BusinessLayer.Models;
using BusinessLayer.Repository;
using BusinessLayer.Services;
using Moq;
using NUnit.Framework;
using SharpTestsEx;
using System;
using System.Threading.Tasks;

namespace WebAPI.Tests
{
    [TestFixture]
    class TestCustomerDeposit
    {
        private Task CompletedTask;
        [SetUp]
        public void Setup()
        {
            CompletedTask = Task.Delay(0);
        }
        [Test]
        public void Given_CustomerAndAnAmount_Should_BeAbleDeposit()
        {
            var repositoryMock = new Mock<IAccountRepository>();
            var serviceMock = new Mock<IAccountService>();
            var accountId = 123;
            var password = "1234";
            var someAccountObj = new Account() { Id = accountId, Password = password };

            var amountToDeposit = 10000M;

            repositoryMock
                .Setup(it => it.IncrementBalance(It.IsAny<int>(), It.IsAny<decimal>())).Returns(CompletedTask);

            repositoryMock.Setup(it => it.CheckPassword(It.IsAny<int>(), It.IsAny<String>()))
                .ReturnsAsync(true);

            var service = new AccountService(repositoryMock.Object, null);
            var result = service.Deposit(accountId, password, amountToDeposit).Result;
            
            result.Should().Be.True();
        }
    }
}
