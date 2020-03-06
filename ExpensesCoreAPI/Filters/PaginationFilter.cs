using ExpensesCoreAPI.Models;
using ExpensesCoreAPI.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesCoreAPI.Filters
{
    public class PaginationFilter : IActionFilter
    {
        private readonly IService<Transaction> _transactionService;
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public PaginationFilter(IService<Transaction> service)
        {
            _transactionService = service;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var query = context.HttpContext.Request.Query;
            PageNumber = query["pageNumber"].Count == 0 ? 0 : int.Parse(query["pageNumber"].ToString());
            PageSize = query["pageSize"].Count == 0 ? 50 : int.Parse(query["pageSize"].ToString());

            var result = PaginationService.GetPagination(_transactionService.GetQueryable(), PageNumber ?? 0, PageSize ?? 50);

            context.HttpContext.Items.Add("result", result);
        }
    }
}
