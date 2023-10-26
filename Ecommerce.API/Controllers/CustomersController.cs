using AutoMapper;
using Ecommerce.API.APIModels;
using Ecommerce.Models.EntityModels;
using Ecommerce.Models.UtilityModel;
using Ecommerce.Services.Abstructions.Customers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Ecommerce.API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        ICustomerService _customerService;
        IMapper _mapper;

        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get([FromForm] CustomerSearchCriteria _customerSearch)
        {
            var customers = _customerService.Search(_customerSearch);

            

            if (customers == null)
            {
                var responseObj = new 
                {
                    statusCode = 404,
                    errorMessage = "Not found"
                };
                return NotFound(responseObj);
            }
            else
            {
                var responseObj = new
                {
                    statusCode = 200,
                    errorMessage = "Success",
                    customer = customers
                };
                return Ok(responseObj);
            }
            
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

        [HttpPost]
        public IActionResult Post([FromBody]CustomerCreate _customer)
        {
            if(ModelState.IsValid)
            {
                var customer = _mapper.Map<Customer>(_customer);
                var isAdded = _customerService.Add(customer);

                if (isAdded)
                {
                    var responseObj = new
                    {
                        statusCode = 200,
                        errorMessage = "Customer added successful"
                    };

                    return Ok(responseObj);
                }

                return BadRequest();
            }
            return BadRequest();
            
        }
    }
}
