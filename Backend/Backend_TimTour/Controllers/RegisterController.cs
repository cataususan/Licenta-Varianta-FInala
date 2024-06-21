using Backend_TimTour.Interfaces.UserInterfaces;
using Backend_TimTour.Models;
using Backend_TimTour.Models.RequestModels;
using Backend_TimTour.Models.ResultsModels;
using Microsoft.AspNetCore.Mvc;

namespace Backend_TimTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly IUserRegisterService _userRegisterService;

        public RegisterController(IUserRegisterService userRegisterService)
        {
            _userRegisterService = userRegisterService;
        }
        [HttpPost]
        public async Task<ActionResult<User>> Register([FromBody] RegisterRequestModel Request)
        {
            try
            {
                var registeredUserName = Request.Name;
                var registeredUserEmail = Request.Email;
                var registeredUserPassword = Request.Password;
                var registeredUserType = Request.Type;
                ServiceResult result = await _userRegisterService.RegisterUserAsync(registeredUserName, registeredUserPassword, registeredUserEmail, registeredUserType);
                if (result == ServiceResult.USER_SUCCESFULLY_REGISTERED)
                {
                    return Ok(new {result = result.ToString()});
                }
                else if(result == ServiceResult.EMAIL_ALREADY_USED)
                {
                    return StatusCode(409,new { result = "The email already exists in the database" });
                }
                else
                {
                    return StatusCode(500, new { result = "Internal server error: " });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "Internal server error: " + ex.Message });
            }
        }
    }
}
