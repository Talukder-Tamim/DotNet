using Ecommerce.Models.EntityModels;
using Ecommerce.Models.UtilityModel;
using Ecommerce.Repositories.Abstraction;
using Ecommerce.Services.Abstructions.Customers;
using Ecommerce.Services.Abstructions.Products;
using Ecommerce.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Products
{

    public class ProductService : BaseService<Product>, IProductService
    {
        IProductRepository _repository;
        public ProductService(IProductRepository _repo) : base(_repo)
        {
            _repository = _repo;
        }

    }

}
