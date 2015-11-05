using System;
using NUnit.Framework;
using Moq;
using SharpTestsEx;
using BusinessLayer.Repository;
using BusinessLayer.Services;
using BusinessLayer.Models;
using BusinessLayer.Exceptions;
using System.Threading.Tasks;

namespace WebAPI.Tests
{
    [TestFixture]
    public class TestCreateAccount
    {
        private Mock<IAccountRepository>_repositoryMock;
        private Mock<ICustomerService> _customerServiceMock;
        private string newCustomerName;
        private string newInvalidCustomerName;
        private string customerInitialPassword;
        private string invalidPassowrd;
        
        [TestFixtureSetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IAccountRepository>();
            _customerServiceMock = new Mock<ICustomerService>();
            newCustomerName = "Rodrigo";
            newInvalidCustomerName = String.Empty;
            customerInitialPassword = "123456";
            invalidPassowrd = String.Empty;
        }
        [Test]
        public void Given_NewUser_ShouldCreateAnAccount()
        {
            var newAccountId = 123;
            var newCustomerId = 321;
            var newCustomerInstance = new Customer(){Id = newCustomerId};
            var someAccountObj = new Account() { Customer = new Customer() { Id = newCustomerId } };

            _repositoryMock
                .Setup( it=>it.InsertAccount(It.IsAny<Account>()))
                .ReturnsAsync(newAccountId);

            _customerServiceMock.Setup(it => it.CreateCustomer(It.IsAny<String>()))
                .Returns(newCustomerInstance);

            var service =  new AccountService(_repositoryMock.Object, _customerServiceMock.Object);
            
            var result = service.CreateNewAccount(newCustomerName, customerInitialPassword).Result;

            result.Should().Be.EqualTo(newAccountId);
        }

        [Test]
        public void Given_InvalidUserName_ShouldThrow()
        {
            var service = new AccountService(_repositoryMock.Object, _customerServiceMock.Object);

            Executing.This(() => service.CreateNewAccount(newInvalidCustomerName, customerInitialPassword))
                .Should().Throw<InvalidCustomerNameException>();
        }

        [Test]
        public void Given_InvalidPassword_ShouldThrow_InvalidPasswordException()
        {
            var service = new AccountService(_repositoryMock.Object, _customerServiceMock.Object);

            Executing.This(() => service.CreateNewAccount(newCustomerName, invalidPassowrd))
                .Should().Throw<InvalidPasswordException>();
        }
    }
}
