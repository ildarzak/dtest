using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SQLitePCL;
using TestProjectMVC.Models;

namespace TestProjectMVC.Controllers
{
    public class AutorizationController : Controller
    {
        private readonly CustomersContext _context;
        
        private readonly IDataProtector _protector;
        
        public AutorizationController(CustomersContext context, IDataProtectionProvider protector)
        {
            _context = context;
            _protector = protector.CreateProtector("user");

        }
        
        public IActionResult login()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult login(AutorizationContext user)
        {
            var User = _context.Customers.FirstOrDefault(e => e.Login == user.Login && e.Password == user.Password);
            if (User != null)
            {
                if (User.IsBlocked)
                {
                    ViewBag.error = "Данный пользователь заблокирован";
                    return View(user);
                }
                Response.Cookies.Append("password" ,_protector.Protect(user.Login));
                if (User.Admin)
                {
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    return RedirectToAction("view", "View", new{id=User.Id});
                }
            }

            ViewBag.error = "Неправильный логин или пароль";
            return View(user);
        }
        
        public IActionResult Regisration()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Regisration(NewCustomer newCustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(new Customer()
                {
                    Name = newCustomer.Name,
                    Phone = newCustomer.PhoneNumber,
                    Login = newCustomer.Login,
                    Password = newCustomer.Password,
                    
                });

                _context.SaveChanges();
            
                return RedirectToAction("login", "Autorization");
            }

            if (_context.Customers.FirstOrDefault(e => e.Login == newCustomer.Login) != null)
            {
                ViewBag.error = "Такой логин уже существует";
            }

            return View(newCustomer);
        }  
    }
    
    public class AutorizationContext 
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
    
    public class NewCustomer
    {   

        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Pasport { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }

    }
}