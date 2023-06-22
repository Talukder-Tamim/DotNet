using Ecommerce.Models.EntityModels;
using Ecommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CustomerController : Controller  
    {
        CustomerRepository _customerrepository;

        public CustomerController()
        {
            _customerrepository = new CustomerRepository();
        }
        public IActionResult Index()
        {
            return View();
        }

        public string List()
        {
            return "This is a customer list..";
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerCreate model)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer() { 
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone
                };

                //DB Operation
                bool isSuccess = _customerrepository.Add(customer);
                
                if (isSuccess)
                {
                    return View();
                }
            }

            return View();
        }
    }
}
