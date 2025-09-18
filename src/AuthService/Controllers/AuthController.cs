using AuthService.Common;
using AuthService.Models;
using AuthService.Entities;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUp signUp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = new Tb_User_Info
            {
                UserAccount = signUp.UserAccount,
                UserPassword = signUp.UserPassword,
                UserName = signUp.UserName,
                UserEmail = signUp.UserEmail,
                UserPhone = signUp.UserPhone
            };

            var result = await _userService.AddUserAsync(newUser);

            if (result.IsFailure)
            {
                return Conflict(new { message = result.Error });
            }

            var userResponse = new UserResponse
            {
                UserId = result.Value.User_Id,
                UserAccount = result.Value.UserAccount,
                UserName = result.Value.UserName,
                UserEmail = result.Value.UserEmail,
                UserPhone = result.Value.UserPhone,
                CreatedDatetime = result.Value.CreatedDatetime
            };

            return CreatedAtAction(nameof(SignUp), new { id = userResponse.UserId }, userResponse);
        }
    }
}