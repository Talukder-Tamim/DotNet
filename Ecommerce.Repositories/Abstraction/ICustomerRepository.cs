using Ecommerce.Models.EntityModels;
using Ecommerce.Models.UtilityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositories.Abstraction
{
    public interface ICustomerRepository
    {
        bool Add(Customer customer);

        bool Update(Customer customer);

        bool Delete(Customer customer);

        bool DeleteById(int id);

        ICollection<Customer> GetAll();

        Customer GetByID(int id);

        ICollection<Customer> Search(CustomerSearchCriteria searchCriteria);

    }
}
