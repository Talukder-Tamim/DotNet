using Ecommerce.Models.EntityModels;
using Ecommerce.Models.UtilityModel;
using Ecommerce.Services.Abstructions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Abstructions.Customers
{
    public interface ICustomerService : IService<Customer>
    {
        ICollection<Customer> Search(CustomerSearchCriteria searchCriteria);
        bool DeleteById(int id);
        Customer GetByID(int id);
    }
}
