using LibraryMSv3.Models.DTO;
using LibraryMSv3.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMSv3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        public ActionResult Login([FromBody] LoginDto request)
        {
            var response = _userService.Login(request.Username, request.Password);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return Ok(response.Message);
        }

        [HttpPost("Signup")]
        public ActionResult<ResponseDto> Signup([FromBody] LoginDto request)
        {
            var response = _userService.Signup(request.Username, request.Password);
            if (!response.IsSuccess)
                return BadRequest(response.Message);
            return response;
        }
    }
}
