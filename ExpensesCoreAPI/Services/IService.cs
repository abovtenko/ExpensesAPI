using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpensesCoreAPI.Services
{
    public interface IService<T> where T : class
    {
        void Create(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetQueryable();
        void Update(T entity);
        void Remove(int id);
        void Save();
    }
}
