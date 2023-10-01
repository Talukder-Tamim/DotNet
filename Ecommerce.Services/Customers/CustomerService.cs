using Ecommerce.Models.EntityModels;
using Ecommerce.Models.UtilityModel;
using Ecommerce.Repositories.Abstraction;
using Ecommerce.Repositories.Abstraction.Base;
using Ecommerce.Services.Abstructions.Customers;
using Ecommerce.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Customers
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        ICustomerRepository _repository;
        public CustomerService(ICustomerRepository _repo) : base(_repo)
        {
            _repository = _repo;
        }

        public bool DeleteById(int id)
        {
            return _repository.DeleteById(id);
        }

        public Customer GetByID(int id)
        {
            return _repository.GetByID(id);
        }

        public ICollection<Customer> Search(CustomerSearchCriteria searchCriteria)
        {
            return _repository.Search(searchCriteria);
        }

        
    }
}
