using BusinessLayer.Models;
using System.Data.Entity;
using System.Data.SqlServerCe;

namespace BusinessLayer.Repositories
{
    public class BaseContext : DbContext
    {
        static BaseContext()
        {
            Database.SetInitializer<BaseContext>(new DbInitializer());
            
            using (var db = new BaseContext())
                db.Database.Initialize(false);
        }        

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
