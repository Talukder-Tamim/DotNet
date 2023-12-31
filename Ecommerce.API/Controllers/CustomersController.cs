﻿using Amazon.DataPipeline.Model;
using AutoMapper;
using Ecommerce.API.APIModels;
using Ecommerce.Models.EntityModels;
using Ecommerce.Models.UtilityModel;
using Ecommerce.Services.Abstructions.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Security.Cryptography.Xml;
using System.Text.Json;
using static Ecommerce.API.Controllers.CustomersController;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ecommerce.API.Controllers
{
    [Route("api/customers")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        ICustomerService _customerService;
        IMapper _mapper;
        HttpClient _client;
        private readonly IMemoryCache _memoryCache;

        public CustomersController(ICustomerService customerService, IMapper mapper, HttpClient client, IMemoryCache memoryCache)
        {
            _customerService = customerService;
            _mapper = mapper;
            _client = client;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult Get([FromForm] CustomerSearchCriteria _customerSearch)
        {
            var cacheKey = "customerList";

            if (!_memoryCache.TryGetValue(cacheKey, out List<Customer> customerList))
            {
                customerList = (List<Customer>?)_customerService.Search(_customerSearch);

                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromSeconds(20)
                };
                //setting cache entries
                _memoryCache.Set(cacheKey, customerList, cacheExpiryOptions);

                if (customerList == null)
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
                        customer = customerList
                    };
                    return Ok(responseObj);
                }
            }
            var responseObj1 = new
            {
                statusCode = 200,
                errorMessage = "Success",
                customer = customerList
            };
            return Ok(responseObj1);

        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var customers = _customerService.GetByID(id);

            if (customers == null)
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
        public IActionResult Post([FromBody] CustomerCreate _customer)
        {
            if (ModelState.IsValid)
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

        public class CSharpMember
        {
            public string? Message { get; set; }
            public string? messageType { get; set; }
        }

        [HttpPost]
        [Route("getdata")]
        public async Task<IActionResult> Post()
        {

            var requestData = new Dictionary<string, string>
{
            { "userId", "24102" },
            { "decodeId", "T8B8Rx" }
};

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri("https://my.bdjobs.com/apps/mybdjobs/v1/apps_lastUpdate_t.asp"),
                Method = HttpMethod.Post,
                Content = new FormUrlEncodedContent(requestData)
            };

            var response = await _client.SendAsync(request);

            var responseString1 = await response.Content.ReadAsStringAsync();

            var responseModel = System.Text.Json.JsonSerializer.Deserialize<CSharpMember>(responseString1);

            return Ok(responseModel);
        }
    }
}
