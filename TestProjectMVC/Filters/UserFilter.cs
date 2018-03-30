using System;
using System.Linq;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TestProjectMVC.Models;

namespace TestProjectMVC.Filters
{
    public class AutorisingFilter :  Attribute, IActionFilter
    {
        
        private readonly IDataProtector _protector;
        private CustomersContext _context;

        public AutorisingFilter(IDataProtectionProvider protector, CustomersContext customers)
        {
            _protector = protector.CreateProtector("user");
            _context = customers;
        }
        
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string currentCookie = context.HttpContext.Request.Cookies["password"];
            if (string.IsNullOrEmpty(currentCookie) || _protector.Unprotect(currentCookie) == null)
            {
                context.Result = new RedirectToActionResult("login", "Autorization", null);
            }

            var User = _context.Customers.FirstOrDefault(e => e.Login == _protector.Unprotect(currentCookie));
            if (User != null && !User.Admin) 
            {
                context.Result = new RedirectToActionResult("View", "View", new {id = User.Id});
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // do something after the action executes
        }
    }
}