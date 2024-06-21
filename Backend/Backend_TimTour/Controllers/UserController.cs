using Microsoft.AspNetCore.Mvc;
using Backend_TimTour.Models;
using Backend_TimTour.Interfaces.UserInterfaces;
using System.Security.Claims;

namespace Backend_TimTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {

            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "Internal server error: " + ex.Message });
            }
        }
        [HttpGet("name")]
        public async Task<ActionResult<string>> GetUserName()
        {
            var name = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            try
            {
                return Ok(new { user_name = name });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "Internal server error: " + ex.Message });
            }
        }
    }
}
