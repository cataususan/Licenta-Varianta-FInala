using Backend_TimTour.Interfaces.LoginInterfaces;
using Backend_TimTour.Models.RequestModels;
using Backend_TimTour.Models;
using Microsoft.AspNetCore.Mvc;
using Backend_TimTour.Models.ResultsModels;

namespace Backend_TimTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequestModel Request)
        {
            try
            {
                var loggedUserEmail = Request.Email;
                var loggedUserPassword = Request.Password;
                var (result, token) = await _loginService.LoginAsync(loggedUserEmail, loggedUserPassword);
                if (result == ServiceResult.LOGIN_SUCCESFUL)
                {
                    return Ok(new { tokenValue = token });
                }
                else if(result == ServiceResult.PASSWORD_INVALID)
                {
                    return StatusCode(401, new { tokenValue = token });
                }
                else if(result ==ServiceResult.USER_NOT_FOUND_IN_DATABASE)
                {
                    return StatusCode(409, new { tokenValue = token });
                }
                else
                {
                    return StatusCode(500, new { tokenValue = token });
                }


            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "Internal server error: " + ex.Message });
            }
        }

    }
}
