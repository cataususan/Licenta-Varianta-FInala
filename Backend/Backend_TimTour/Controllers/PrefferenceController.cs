using Backend_TimTour.Models.RequestModels;
using Backend_TimTour.Models.ResultsModels;
using Backend_TimTour.Models;
using Microsoft.AspNetCore.Mvc;
using Backend_TimTour.Interfaces.UserInterfaces;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Backend_TimTour.Models.PrefferenceModels;
using Backend_TimTour.Interfaces.Parsers;
using Backend_TimTour.Models.LocationEnums;

namespace Backend_TimTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrefferenceController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserPrefferenceParser _userPrefferenceParser;
        public PrefferenceController(IUserRepository userRepository, IUserPrefferenceParser userPrefferenceParser)
        {
            _userRepository = userRepository;
            _userPrefferenceParser = userPrefferenceParser;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> PostPrefferences([FromBody] PostPrefferenceRequestModel Request)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var (prefference,parsingResult) = await _userPrefferenceParser.ParseRequestToPrefference(Request.BarFeatures, Request.EventFeatures, Request.MuseumFeatures, Request.RestaurantFeatures);
            
            try
            {
                RepositoryResult result = await _userRepository.InsertUserPrefferences(email, prefference);
                return StatusCode(200, new { result = result.ToString() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { result = "Internal server error: " + ex.Message });
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetPrefferences()
        {
            var barAmbiance = Enum.GetNames(typeof(BarAmbiance)).ToList();
            var barEvents = Enum.GetNames(typeof(BarEvents)).ToList();
            var barDrinks = Enum.GetNames(typeof(BarDrinkSpecialties)).ToList();
            var barFoods = Enum.GetNames(typeof(BarFoodOptions)).ToList();
            var barType = Enum.GetNames(typeof (BarType)).ToList();

            var eventAudience = Enum.GetNames (typeof (EventAudience)).ToList();
            var eventDuration = Enum.GetNames(typeof(EventDuration)).ToList();
            var eventGenre = Enum.GetNames(typeof(EventGenre)).ToList();
            var eventType = Enum.GetNames(typeof(EventTypes)).ToList();
            var eventVenue = Enum.GetNames(typeof(EventVenue)).ToList();

            var museumAccesibility = Enum.GetNames(typeof(MuseumAccesibility)).ToList();
            var museumExhibits = Enum.GetNames(typeof(MuseumExhibitsTypes)).ToList();
            var museumTypes = Enum.GetNames(typeof(MuseumTypes)).ToList();
            var museumVisitorsService = Enum.GetNames(typeof(MuseumVisitorService)).ToList();

            var restaurantAtmosphere = Enum.GetNames(typeof(RestaurantAtmosphere)).ToList();
            var restaurantCusine = Enum.GetNames(typeof(RestaurantCusineTypes)).ToList();
            var restaurantDietaryRestrictions = Enum.GetNames(typeof(RestaurantDietaryRestrictions)).ToList();
            var restaurantSpecialFeatures = Enum.GetNames(typeof(RestaurantSpecialFeatures)).ToList();
            
            var priceRange = Enum.GetNames(typeof(UniversalPriceRange)).ToList();
            

            var result = new
            {
                BarAmbiance = barAmbiance,
                BarEvents = barEvents,
                BarFoodOptions = barFoods,
                BarDrinkSpecialties = barDrinks,
                BarType = barType,
                EventAudience = eventAudience,
                EventDuration = eventDuration,
                EventGenre = eventGenre,
                EventTypes = eventType,
                EventVenue = eventVenue,
                MuseumAccesibility = museumAccesibility,
                MuseumExhibitsTypes = museumExhibits,
                MuseumTypes = museumTypes,
                MuseumVisitorService = museumVisitorsService,
                RestaurantAtmosphere= restaurantAtmosphere,
                RestaurantCusineTypes = restaurantCusine,
                RestaurantDietaryRestrictions = restaurantDietaryRestrictions,
                RestaurantSpecialFeatures = restaurantSpecialFeatures,
                UniversalPriceRange = priceRange
            };

            return Ok(result);
        }
    }
}
