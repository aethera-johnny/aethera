using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        // TestCode
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello");
        }
    }
}
