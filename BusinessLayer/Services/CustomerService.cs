using BusinessLayer.Repositories;

namespace BusinessLayer.Services
{
    public class CustomerService : ICustomerService
    {
        private CustomerRepository repository;
        public CustomerService()
        {
            repository = new CustomerRepository();

        }
        public Models.Customer CreateCustomer(string name)
        {
            return repository.CreateUser(name);
        }
    }
}
