using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesCoreAPI.Services
{
    public static class PaginationService
    {
        public static IEnumerable<T> GetPagination<T>(IQueryable<T> query, int pageSize = 25, int pageNumber = 10) where T : class
        {
            var skipCount = (pageNumber - 1) * pageSize;

            return query.Skip(skipCount).Take(pageSize);
        }
    }
}
