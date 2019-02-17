using ExpensesCoreAPI.Models;
using ExpensesCoreAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ExpensesCoreAPI.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IService<Account> _accountService;

        public AccountController(IService<Account> service)
        {
            _accountService = service;
        }

        [Route("")]
        public IActionResult GetAll()
        {
            return Ok(_accountService.GetAll());
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var result = _accountService.Get(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody]Account model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _accountService.Create(model);
            _accountService.Save();

            return Created($"{Request.Host}/api/accounts/{model.AccountID}", model);
        }

        [HttpPut]
        [Route("")]
        public IActionResult Put([FromBody] Account model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _accountService.Update(model);
            _accountService.Save();

            return Ok(model);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            _accountService.Remove(id);
            _accountService.Save();

            return Ok();
        }
        
        [HttpGet]
        [Route("{id}/transactions")]
        public IActionResult GetTransactions(int id, [FromServices]IService<Transaction> service)
        {
            var result = service.GetWhere(x => x.AccountID == id).ToList();

            return Ok(result);
        }
    }
}
