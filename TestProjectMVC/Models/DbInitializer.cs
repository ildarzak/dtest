using System;
using System.Linq;
using Newtonsoft.Json;

namespace TestProjectMVC.Models
{
    public class DbInitializer
    {
        public static void Initialize(CustomersContext furstCustomer)
        {
            furstCustomer.Database.EnsureCreated();

            if (!furstCustomer.Customers.Any())
            {

                furstCustomer.Customers.Add(new Customer()
                {
                    Name = "Test",
                    Phone = "+7 999 999 99 99",
                    BirthDate = new DateTime(2000, 12, 16),
                    Pasport = "11 11 111111",
                    Login = "Test",
                    Password = "123456",
                    Admin = true,
                });
            }

            furstCustomer.SaveChanges();
        }
    }
}