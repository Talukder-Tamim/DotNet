using Ecommerce.Repositories.Abstraction.Base;
using Ecommerce.Services.Abstructions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Base
{
    public abstract class BaseService<T> : IService<T> where T : class
    {
        IRepository<T> _repository;

        public BaseService(IRepository<T> _repo)
        {
            _repository = _repo;
        }
        public virtual bool Add(T entity)
        {
            return _repository.Add(entity);
            
        }

        public virtual bool Delete(T entity)
        {
            return _repository.Delete(entity);
        }

        public virtual ICollection<T> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual bool Update(T entity)
        {
            return _repository.Update(entity);
        }
    }
}
