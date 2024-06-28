using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    { 
    
        public List<T> GetAll(Expression<Func<T, bool>>? Fillter = null, string? includeProperties = null);
        public T Get(Expression<Func<T, bool>> Fillter = null, bool tracked = true, string? includeProperties = null);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
        void Add(T entity);
        void Save();
    }
}
