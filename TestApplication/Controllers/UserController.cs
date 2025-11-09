using Microsoft.AspNetCore.Mvc;
using TestApplication.Interfaces;
using TestApplication.Models;

namespace TestApplication.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController: ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService) {
            _userService = userService;
        }
        [HttpGet]
        public ActionResult Login([FromQuery] string email, [FromQuery] string password)
        {
            return Ok(_userService.login(email, password));
        }

        [HttpPost]
        public async Task<ActionResult> RegisterAsync([FromBody] User user)
        {
            User result = await _userService.registerUser(user);
            return Ok(new {result.Name, result.Email, result.Role});
        }
    }
}
