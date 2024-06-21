using Backend_TimTour.Interfaces.LocationInterfaces;
using Backend_TimTour.Interfaces.ReservationInterfaces;
using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.PrefferenceModels;
using Backend_TimTour.Models.RequestModels;
using Backend_TimTour.Models.ResultsModels;
using Backend_TimTour.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace Backend_TimTour.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationRepository reservationRepository, IReservationService reservationService)
        {
            _reservationService = reservationService;
            _reservationRepository = reservationRepository;
        }
        [HttpGet("customerEmail")]
        [Authorize(Policy = "TouristAuth")]
        public async Task<ActionResult<List<Reservation>>> GetReservationsByUserEmail()
        {
            try
            {
                var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var (result, reservations) = await _reservationRepository.FindAllReservationsByCustomerEmailAsync(email);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("restaurantName")]
        public async Task<ActionResult<List<Reservation>>> GetReservationsByRestaurantName([FromQuery] FindReservationsByRestaurant Request)
        {
            try
            {
                var (result, reservations) = await _reservationRepository.FindAllReservationsByRestaurantNameAsync(Request.Name);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPost("makeReservation")]
        [Authorize(Policy = "TouristAuth")]
        public async Task<ActionResult<ServiceResult>> MakeReservation([FromBody] MakeReservationRequestModel Request)
        {
            try
            {
                var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var customerName = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                DateTime dateAndTime;
                try
                {
                    dateAndTime = DateTime.ParseExact(Request.ReservationDateAndTime, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                    if(dateAndTime < DateTime.Now)
                    {
                        return StatusCode(400, new { message = "Date is from the past,learn to count time" });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(400, new { message = "[reservationDateAndTime] malformed" });
                }
                var locationName = Request.LocationName;
                var locationType = Request.LocationType;
                string status = "pending";
                var dayOfTheReservation = dateAndTime.DayOfWeek;
                var hourOfTheReservation = dateAndTime.Hour;
                var minuteOfTheReservation = dateAndTime.Minute;

                var locationStatus = await _reservationService.CheckIfLocationIsOpen(dayOfTheReservation, hourOfTheReservation, minuteOfTheReservation, locationType, locationName);

                if (locationStatus == ServiceResult.LOCATION_IS_OPEN)
                {
                    var reservationObject = new Reservation
                    {
                        CustomerName = customerName,
                        CustomerEmail = email,
                        ReservationDate = ((DateTimeOffset)dateAndTime).ToUnixTimeSeconds(),
                        LocationName = locationName,
                        LocationType = locationType,
                        Status = status
                    };
                    var reservationResult = await _reservationRepository.SendReservation(reservationObject);
                    if (reservationResult == RepositoryResult.RESERVATION_CREATED)
                    {
                        return Ok(new { result = reservationResult.ToString() });
                    }
                    else
                    {
                        return StatusCode(500, new { result = reservationResult.ToString() });
                    }
                }
                else
                {
                    return StatusCode(403, new { result = locationStatus.ToString() });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        [HttpPatch("updateReservationStatus")]
        public async Task<ActionResult<ServiceResult>> UpdateReservationStatus([FromBody] UpdateReservationRequestModel Request)
        {
            try
            {
                var reservationResult = await _reservationRepository.UpdateReservationAsync(Request.LocationName, Request.CustomerEmail, Request.Status);
                if (reservationResult == RepositoryResult.RESERVATION_SUCCESFULLY_UPDATED)
                {
                    return Ok(new { result = reservationResult.ToString() });
                }
                else
                {
                    return StatusCode(500, new { result = reservationResult.ToString() });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
