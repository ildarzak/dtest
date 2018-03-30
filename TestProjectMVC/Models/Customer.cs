using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TestProjectMVC.Models
{
    public class CustomersContext : DbContext
    {
        public CustomersContext(DbContextOptions<CustomersContext> options) : base(options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
    }

    public class Customer
    {   
        public int Id
        {
            get;
            set;
        }
        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }
        public string Phone { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Pasport { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsBlocked { get; set; }
        public bool Admin { get; set; }
    }
}