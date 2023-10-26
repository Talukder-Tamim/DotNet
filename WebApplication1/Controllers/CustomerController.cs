using AutoMapper;
using Ecommerce.Models.EntityModels;
using Ecommerce.Models.UtilityModel;
using Ecommerce.Repositories;
using Ecommerce.Repositories.Abstraction;
using Ecommerce.Services.Abstructions.Customers;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Filters;
using WebApplication1.Models;
using WebApplication1.Models.CustomerList;

namespace WebApplication1.Controllers
{
    public class CustomerController : Controller  
    {
        ICustomerService _customerService;
        IMapper _mapper;

        public CustomerController(ICustomerService _repo, IMapper mapper)
        {
            _customerService = _repo;
            _mapper = mapper;
        }

        [SampleActionFilter]
        public IActionResult Index(CustomerSearchCriteria customer)
        {
            var customerList = _customerService.Search(customer);
            ICollection<CustomerListItem> customerModel = customerList.Select(c => new CustomerListItem
            {
                Id = c.Id,
                Name = c.Name,
                Phone = c.Phone,
                Email = c.Email
            }).ToList();

            var customerListModel = new CustomerListViewModel();
            customerListModel.customerList = customerModel;

            return View(customerListModel);
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
                var customer = _mapper.Map<Customer>(model);
                //var customer = new Customer() { 
                //    Name = model.Name,
                //    Email = model.Email,
                //    Phone = model.Phone
                //};

                //DB Operation
                bool isSuccess = _customerService.Add(customer);
                
                if (isSuccess)
                {
                    return RedirectToAction("Index", "Customer");
                }
            }

            return View();
        }

        public IActionResult View(int? id)
        {
            if (id != null)
            {
                var customer = _customerService.GetByID((int)id);
                var model = new CustomerEditViewModel()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Phone = customer.Phone,
                    Email = customer.Email
                };
                return View(model);
            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.Error = "Please return a valid id.";
                return View();
            }
            
            var customer = _customerService.GetByID((int)id);

            if (customer == null)
            {
                ViewBag.Error = "Sorry no customer found!";
                return View();
            }

            var model = new CustomerEditViewModel()
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
            };

            return View(model);
            
            
        }

        [HttpPost]
        public IActionResult Edit(CustomerEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _customerService.GetByID(model.Id);

                if (customer == null)
                {
                    ViewBag.Error = "No customer found to update";
                    return View();
                }
                else
                {
                    _mapper.Map(model, customer);
                    //customer.Id = model.Id;
                    //customer.Name = model.Name;
                    //customer.Email = model.Email;
                    //customer.Phone = model.Phone;

                    bool isSuccess = _customerService.Update(customer);
                    return RedirectToAction("Index");
                }
            }
            
            ViewBag.Error = "Some required data missing";
            return View(model);
            
            
        }

        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                bool isSuccess = _customerService.DeleteById((int)id);
                if (isSuccess)
                {
                    return RedirectToAction("index");
                }

            }
            return RedirectToAction("index");
        }
    }
}
