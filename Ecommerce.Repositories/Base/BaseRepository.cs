using Ecommerce.Models.EntityModels;
using Ecommerce.Repositories.Abstraction.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositories.Base
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        DbContext _dbContext;

        public BaseRepository(DbContext db) 
        {
            _dbContext = db;
        }

        private DbSet<T> Table
        {
            get 
            { 
                return _dbContext.Set<T>();
            }
        }

        public bool Add(T entity)
        {
            Table.Add(entity);
            return _dbContext.SaveChanges() > 0;
        }

        public bool Delete(T entity)
        {
            Table.Remove(entity);
            return _dbContext.SaveChanges() > 0;
        }

        public ICollection<T> GetAll()
        {
            return Table.ToList();
        }

        public bool Update(T entity)
        {
            Table.Update(entity);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
