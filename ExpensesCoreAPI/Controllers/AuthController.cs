using ExpensesCoreAPI.Models;
using ExpensesCoreAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExpensesCoreAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]CredentialsViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var identity = await GetClaimsIdentity(credentials.UserName, credentials.Password);
            if (identity == null)
                return BadRequest();
            var jwt = await _tokenService.GenerateJwt(identity, credentials.UserName);

            return Ok(jwt);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return await Task.FromResult<ClaimsIdentity>(null);

            var userToVerify = await _userManager.FindByNameAsync(userName);
            if (userToVerify == null)
                return await Task.FromResult<ClaimsIdentity>(null);

            var hasValidCredentials = await _userManager.CheckPasswordAsync(userToVerify, password);
            if (!hasValidCredentials)
                return await Task.FromResult<ClaimsIdentity>(null);   

            return await Task.FromResult(_tokenService.GenerateClaimsIdentity(userName, userToVerify.Id));
        }
    }

    public class CredentialsViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
