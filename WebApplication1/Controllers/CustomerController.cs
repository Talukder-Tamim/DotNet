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

        public string Create(CustomerCreate customer)
        {
            return $"Create customer..., Name: {customer.Name}, Phone:{customer.Phone}, Email: {customer.Email}";
        }
    }
}
