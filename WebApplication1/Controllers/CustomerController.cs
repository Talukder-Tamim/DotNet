using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string List()
        {
            return "This is a customer list..";
        }

        public IActionResult Create(CustomerCreate customer)
        {
            return View();

        }
    }
}
