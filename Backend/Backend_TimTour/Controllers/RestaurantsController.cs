using Backend_TimTour.Interfaces.LocationInterfaces;
using Backend_TimTour.Interfaces.Parsers;
using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.LocationModels.SimplifiedLocations;
using Backend_TimTour.Models.RequestModels;
using Backend_TimTour.Models.ResultsModels;
using Backend_TimTour.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Linq;

namespace Backend_TimTour.Controllers
{

        [Route("api/[controller]")]
        [ApiController]
        public class RestaurantsController : ControllerBase
        {
            private readonly IRestaurantRepository _restaurantRepository;
            private readonly IRestaurantParser _restaurantParser;

            public RestaurantsController(IRestaurantRepository restaurantRepository, IRestaurantParser restaurantParser)
            {
                _restaurantRepository = restaurantRepository;
                _restaurantParser = restaurantParser;
            }
            
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurantsAsync()
            {
                try
                {
                    var restaurants = await _restaurantRepository.GetAllRestaurantsAsync();
                    IEnumerable<RequestRestaurant> transformedRestaurants = restaurants
                        .Select(_restaurantParser.ParseRestaurantToDatabaseObject)
                        .ToList();
                return Ok(transformedRestaurants);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { result = "Internal server error: " + ex.Message });
                }
            }
            [HttpGet("simplifiedRestaurant")]
            public async Task<ActionResult<IEnumerable<SimplifiedRestaurant>>> GetSimpleRestaurantsAsync()
            {
                try
                {
                    var restaurants = await _restaurantRepository.GetSimplifiedRestaurantsAsync();
                    return Ok(restaurants);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { result = "Internal server error: " + ex.Message });
                }
            }
            [HttpGet("getRestaurant")]
            [Authorize]
            public async Task<ActionResult<float>> GetRestaurant([FromQuery] RestaurantRequest Request)
            {
                try
                {
                    var (result, restaurants) = await _restaurantRepository.FindByNameAsync(Request.Name);
                    return Ok(restaurants);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { result = "Internal server error: " + ex.Message });
                }
            }
            [HttpGet("pendingRestaurant")]
            public async Task<ActionResult<IEnumerable<SimplifiedRestaurant>>> GetPendingRestaurantsAsync()
            {
                try
                {
                    var restaurants = await _restaurantRepository.GetPendingRestaurantsAsync();
                    return Ok(restaurants);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { result = "Internal server error: " + ex.Message });
                }
            }
            [HttpPost]
            public async Task<ActionResult<RepositoryResult>> InsertRestaurantAsync([FromBody] AddRestaurantRequestModel Request)
            {
                var restaurantToParse = Request.restaurant;
                try
                {
                    var (restaurantToInsert, result) = await _restaurantParser.ParseRequestToRestaurant(restaurantToParse);
                    if (result)
                    {
                        restaurantToInsert.Status = "pending";
                        var insertionResult = await _restaurantRepository.AddRestaurantAsync(restaurantToInsert);
                        
                        return Ok(new { result = insertionResult.ToString() });
                    }
                    else
                    {
                        return StatusCode(500, new { result = "Internal server error: " + "Parsing Failed" });
                    } 
                }
                catch (Exception ex)
                {
                    return StatusCode(500,new { result = "Internal server error: " + ex.Message });
                }
            }
            [HttpPatch("updateRestaurantStatus")]
            public async Task<ActionResult<RepositoryResult>> UpdateRestaurantStatusAsync([FromBody] UpdateRestaurantStatusRequestModel Request)
            {
                string status = Request.restaurantStatus;
                string restaurantName = Request.restaurantName;

                try
                {
                    var insertionResult = await _restaurantRepository.SwitchStatusAsync(status, restaurantName);
                    return Ok(new { result = insertionResult.ToString() });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { result = "Internal server error: " + ex.Message });
                }
            }


        }
}
