using Ecommerce.Database;
using Ecommerce.Models.EntityModels;
using Ecommerce.Models.UtilityModel;
using Ecommerce.Repositories.Abstraction;
using Ecommerce.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositories
{
    public class CustomerRepository :BaseRepository<Customer>, ICustomerRepository
    {
        ApplicationDbContext _db;

        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public bool DeleteById(int id)
        {
            var customer = _db.Customers.Where(c => c.Id==id).First();
            _db.Customers.Remove(customer);
            return _db.SaveChanges() > 0;
        }

        public Customer GetByID(int id)
        {
            return _db.Customers.FirstOrDefault(c => c.Id == id);
        }

        public ICollection<Customer> Search(CustomerSearchCriteria searchCriteria)
        {
            var customer = _db.Customers.AsQueryable();

            if(!string.IsNullOrEmpty(searchCriteria.SearchText))
            { 
                var searchText = searchCriteria.SearchText.ToLower();

                customer = customer.Where(c => c.Name.ToLower().Contains(searchText) || c.Email.ToLower().Contains(searchText) || c.Phone.ToLower().Contains(searchText));
            }

            if (searchCriteria != null && !string.IsNullOrEmpty(searchCriteria.Name))
            {
                customer = customer.Where(c => c.Name.ToLower().Contains(searchCriteria.Name.ToLower()));
            }
            if (searchCriteria != null && !string.IsNullOrEmpty(searchCriteria.Phone))
            {
                customer = customer.Where(c => c.Phone.ToLower().Contains(searchCriteria.Phone.ToLower()));
            }
            if (searchCriteria != null && !string.IsNullOrEmpty(searchCriteria.Email))
            {
                customer = customer.Where(c => c.Email.ToLower().Contains(searchCriteria.Email.ToLower()));
            }

            var skipSize = (searchCriteria.CurrentPage - 1) * searchCriteria.PageSize;

            return customer.Skip(skipSize).Take(searchCriteria.PageSize).ToList();
        }
    }
}
