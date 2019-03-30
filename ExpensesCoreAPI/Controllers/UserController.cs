using System.Linq;
using System.Threading.Tasks;
using ExpensesCoreAPI.Models;
using ExpensesCoreAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ExpensesCoreAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IService<AppUser> _userService;
        private readonly UserManager<AppUser> _userManager;

        public UserController(IService<AppUser> service, UserManager<AppUser> userManager)
        {
            _userService = service;
            _userManager = userManager;
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
        public async Task<IActionResult> Post([FromBody] RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new AppUser { UserName = model.UserName };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest();
            }

            //_userService.Create(model);
            _userService.Save();

            return Created($"{Request.Host}/api/users/{user.UserID}", model);
        }

        [HttpPut]
        [Route("")]
        public IActionResult Put([FromBody] AppUser model)
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
        [Route("{id}/accounts")]
        public IActionResult GetAccounts(int id, [FromServices] IService<Account> service)
        {
            var result = service.GetWhere(x => x.UserID == id).ToList();

            return Ok(result);
        }
        
    }

    public class RegistrationViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
