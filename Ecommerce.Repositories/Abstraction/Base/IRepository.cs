using Ecommerce.Models.EntityModels;
using Ecommerce.Models.UtilityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repositories.Abstraction.Base
{
    public interface IRepository<T> where T : class
    {
        bool Add(T entity);
        //bool AddRange(ICollection<T> entity);
        bool Update(T entity);
        //bool UpdateRange(ICollection<T> entity);
        bool Delete(T entity);
        ICollection<T> GetAll();
    }
}
