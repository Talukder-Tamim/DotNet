using Ecommerce.Database;
using Ecommerce.Models.EntityModels;
using Ecommerce.Repositories.Abstraction;
using Ecommerce.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositories
{
    public class ProductRepository :BaseRepository<Product>, IProductRepository
    {
        ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
