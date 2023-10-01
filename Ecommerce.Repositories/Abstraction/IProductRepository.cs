using Ecommerce.Models.EntityModels;
using Ecommerce.Repositories.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositories.Abstraction
{
    public interface IProductRepository : IRepository<Product>
    {
    }
}
