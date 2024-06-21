using Backend_TimTour.Interfaces.LocationInterfaces;
using Backend_TimTour.Interfaces.ReservationInterfaces;
using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.ResultsModels;
using System.Globalization;
using System.Xml.Linq;

namespace Backend_TimTour.Services
{
    public class ReservationService:IReservationService
    {
        private readonly IBarRepository _barRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public ReservationService(IBarRepository barRepository, IRestaurantRepository restaurantRepository)
        {
            _barRepository = barRepository;
            _restaurantRepository = restaurantRepository;
        }

        public async Task<ServiceResult> CheckIfLocationIsOpen(System.DayOfWeek dayName, int hour, int minutes,string locationType,string locationName)
        {
            if (locationType == "bar")
            {
                var (result, bar) = await _barRepository.FindByNameAsync(locationName);
                if (result == RepositoryResult.BAR_NOT_FOUND)
                {
                    return ServiceResult.LOCATION_NAME_CAN_NOT_BE_FOUND_IN_DATABASE;
                }
                else
                {
                    TimeSpan hourMarginForReservation = new TimeSpan(2, 0, 0);
                    TimeSpan reservationTime = new TimeSpan(hour, minutes, 0);
                    var openHours = bar.Schedule.Find(day => day.Name == dayName.ToString());
                    var openTime = DateTime.ParseExact(openHours.OpenTime, "h:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                    var closeTime = DateTime.ParseExact(openHours.CloseTime, "h:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                    bool isLocationOpen = (reservationTime >= openTime) && (reservationTime < closeTime - hourMarginForReservation);
                    if (isLocationOpen)
                    {
                        return ServiceResult.LOCATION_IS_OPEN;
                    }
                    else
                        return ServiceResult.LOCATION_IS_CLOSED;
                }
            }
            else if (locationType == "restaurant")
            {
                var (result, restaurant) = await _restaurantRepository.FindByNameAsync(locationName);
                if (result == RepositoryResult.RESTAURANT_NOT_FOUND)
                {
                    return ServiceResult.LOCATION_NAME_CAN_NOT_BE_FOUND_IN_DATABASE;
                }
                else
                {
                    TimeSpan hourMarginForReservation = new TimeSpan(2, 0, 0);
                    TimeSpan reservationTime = new TimeSpan(hour, minutes, 0);
                    var openHours = restaurant.Schedule.Find(day => day.Name == dayName.ToString());
                    var openTime = DateTime.ParseExact(openHours.OpenTime,"h:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                    var closeTime = DateTime.ParseExact(openHours.CloseTime, "h:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                    bool isLocationOpen =(reservationTime >= openTime) && (reservationTime < closeTime - hourMarginForReservation); 
                    if (isLocationOpen)
                    {
                        return ServiceResult.LOCATION_IS_OPEN;
                    }
                    else
                        return ServiceResult.LOCATION_IS_CLOSED;
                }
            }
            else
            {
                return ServiceResult.LOCATION_TYPE_SENT_IS_NOT_TREATED_IN_THE_DATABSE;
            }
        }
    }
}
