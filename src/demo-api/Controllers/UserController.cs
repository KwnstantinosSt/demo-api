using demo_api.Dtos.UserDtos;
using Microsoft.AspNetCore.Mvc;

namespace demo_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {

        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterUserDto user)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginUserDto user)
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult Get([FromBody] UserDto user)
        {
            return Ok();
        }

    }
}