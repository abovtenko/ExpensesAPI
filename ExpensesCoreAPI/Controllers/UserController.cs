using System.Linq;
using System.Net;
using System.Net.Http;
using ExpensesCoreAPI.Models;
using ExpensesCoreAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesCoreAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IService<User> _userService;

        public UserController(IService<User> service)
        {
            _userService = service;
        }

        [Route("")]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var result = _userService.Get(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        public IActionResult Post([FromBody]User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _userService.Create(model);
            _userService.Save();

            return Created($"{Request.Host}/api/users/{model.UserID}", model);
        }

        [HttpPut]
        [Route("")]
        public IActionResult Put([FromBody] User model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _userService.Update(model);
            _userService.Save();

            return Ok(model);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.Remove(id);
            _userService.Save();

            return Ok();
        }

        [HttpGet]
        [Route("{id}/transactions")]
        public IActionResult GetTransactions(int id, IService<Transaction> service)
        {
            var transactionService = service;
            var result = transactionService.GetWhere(x => x.UserID == id).ToList();

            return Ok(result);
        }
    }
}
