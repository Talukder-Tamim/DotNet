using Ecommerce.Models.EntityModels;
using Ecommerce.Services.Abstructions.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var customers = _customerService.GetAll();
            if(customers == null)
            {
                var responseObj = new 
                {
                    statusCode = 200,
                    errorMessage = "Not found"
                };
                return NotFound(responseObj);
            }
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
        { 
            var customers = _customerService.GetByID(id);

            if(customers == null)
            {
                var responseObj = new
                {
                    statusCode = 200,
                    errorMessage = "Not found"
                };
                return NotFound(responseObj);
            }
            return Ok(customers);
        }
    }
}
