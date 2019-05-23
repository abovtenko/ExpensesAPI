using ExpensesCoreAPI.Filters;
using ExpensesCoreAPI.Models;
using ExpensesCoreAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesCoreAPI.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly IService<Transaction> _transactionService;

        public TransactionController(IService<Transaction> service)
        {
            _transactionService = service;
        }

        [Route("")]        
        public IActionResult GetAll([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var result = PaginationService.GetPagination(_transactionService.GetQueryable(), pageNumber ?? 0, pageSize ?? 50);
            return Ok(result);
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var result = _transactionService.Get(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        [ServiceFilter(typeof(ModelValidationFilter<Transaction>))]
        public IActionResult Post([FromBody] Transaction model)
        {
            _transactionService.Create(model);
            _transactionService.Save();

            return Created($"{Request.Host}/api/transactions/{model.TransactionID}", model);
        }

        [HttpPut]
        [Route("")]
        [ServiceFilter(typeof(ModelValidationFilter<Transaction>))]
        public IActionResult Put([FromBody] Transaction model)
        {
            _transactionService.Update(model);
            _transactionService.Save();

            return Ok(model);
        }

        
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            _transactionService.Remove(id);
            _transactionService.Save();

            return Ok();
        }
    }
}
