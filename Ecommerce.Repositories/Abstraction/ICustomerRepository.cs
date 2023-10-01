using Ecommerce.Models.EntityModels;
using Ecommerce.Models.UtilityModel;
using Ecommerce.Repositories.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositories.Abstraction
{
    public interface ICustomerRepository : IRepository<Customer>
    {

        bool DeleteById(int id);

        //ICollection<Customer> GetAll();

        Customer GetByID(int id);

        ICollection<Customer> Search(CustomerSearchCriteria searchCriteria);

    }
}
