using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using TestProjectMVC.Models;

namespace TestProjectMVC.Controllers
{
    public class ViewController : Controller
    {
        private CustomersContext _context;

        public ViewController(CustomersContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> view(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }
    }
}