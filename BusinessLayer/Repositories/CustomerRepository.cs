using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repositories
{
    class CustomerRepository 
    {
        public Customer CreateUser(string name)
        {
            using (var db = new BaseContext())
            {
                var newCustomer = new Models.Customer() { Name = name };
                db.Customers.Add(newCustomer);
                db.SaveChanges();

                return newCustomer;
            }
        }
    }
}
