using Backend_TimTour.Interfaces.RecommandationInterfaces;
using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.PrefferenceModels;
using Backend_TimTour.Models.RequestModels;
using Backend_TimTour.Models.RequestModels.ServiceResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace Backend_TimTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommandationController : Controller
    {
        private readonly IRecommandationService _recommandationService;
        public RecommandationController(IRecommandationService recommandationService)
        {
            _recommandationService = recommandationService;
        }
        [HttpGet("getRestaurantRecommandation")]
        [Authorize]
        public async Task<ActionResult<RestaurantRecommendationResponse>> GetRestaurantRecommandation()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            try
            {
                var (result,prefference) = await _recommandationService.GetRestaurantRecommandationAsync(email);
                return Ok(prefference);
            }
            catch (Exception ex)
            {
            
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }
        [HttpGet("getBarRecommandation")]
        [Authorize]
        public async Task<ActionResult<BarRecommandationResponse>> GetBarRecommandation()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            try
            {
                var (result, prefference) = await _recommandationService.GetBarRecommandationAsync(email);
                return Ok(prefference);
            }
            catch (Exception ex)
            {
            
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }
        [HttpGet("getMuseumRecommandation")]
        [Authorize]
        public async Task<ActionResult<MuseumRecommandationResponse>> GetMuseumRecommandation()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            try
            {
                var (result, prefference) = await _recommandationService.GetMuseumRecommandationAsync(email);
                return Ok(prefference);
            }
            catch (Exception ex)
            {
            
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }
        [HttpGet("getEventRecommandation")]
        [Authorize]
        public async Task<ActionResult<EventRecommandationResponse>> GetEventRecommandation()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            try
            {
                var (result, prefference) = await _recommandationService.GetEventRecommandationAsync(email);
                return Ok(prefference);
            }
            catch (Exception ex)
            {
            
                return StatusCode(500, new { message = "Internal server error: " + ex.Message });
            }
        }
    }
}
