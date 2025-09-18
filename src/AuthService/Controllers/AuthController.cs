using AuthService.Common;
using AuthService.Models;
using AuthService.Entities;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] UserLogin userLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authResponse = await _userService.AuthenticateAsync(userLogin.UserAccount, userLogin.UserPassword);

            if (authResponse == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            return Ok(authResponse);
        }

        [HttpPost("signup")]
        public async Task<ActionResult<AuthResponse>> SignUp([FromBody] UserSignUp signUp)
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

            var authResponse = await _userService.AddUserAsync(newUser);

            if (authResponse == null)
            {
                return Conflict(new { message = "User registration failed, possibly duplicate account." });
            }

            return CreatedAtAction(nameof(SignUp), new { id = authResponse.UserId }, authResponse);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.GetUserByRefreshTokenAsync(request.RefreshToken);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid or expired refresh token" });
            }

            var newAccessToken = _tokenService.GenerateJwtToken(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            await _tokenService.SaveRefreshTokenAsync(user.User_Id, newRefreshToken);

            return Ok(new AuthResponse
            {
                UserId = user.User_Id,
                UserAccount = user.UserAccount,
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
            });
        }
    }
}