using Backend_TimTour.Interfaces.RatingInterfaces;
using Backend_TimTour.Models.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend_TimTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : Controller
    {
        private readonly IRatingService _ratingService;
        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<string>> RateLocationsAsync([FromBody] RateModel Request)
        {
            try
            {
                var ratingValue = Request.Rating;
                var locationName = Request.Name;
                var locationType = Request.Type;
                var result = await _ratingService.RateLocationAsync(locationName,locationType,ratingValue);
                return Ok(new { result = result.ToString() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "Internal server error: " + ex.Message });
            }
        }
    }
}
