using BusinessLayer.Models;
namespace BusinessLayer.Services
{
    public interface ICustomerService
    {
        Customer CreateCustomer(string name);
    }
}
