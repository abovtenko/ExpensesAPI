using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesAPI.Services
{
    public interface IService<T> where T : class
    {
        void Create(T entity);
        T Get(int id);
        IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        void Remove(int id);
    }
}
