using Backend_TimTour.Interfaces.LocationInterfaces;
using Backend_TimTour.Interfaces.RatingInterfaces;
using Backend_TimTour.Models;
using Backend_TimTour.Models.ResultsModels;

namespace Backend_TimTour.Services
{
    public class RatingService:IRatingService
    {
        private readonly IBarRepository _barRepository;
        private readonly IMuseumRepository _museumRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public RatingService(IBarRepository barRepository, IMuseumRepository museumRepository, IRestaurantRepository restaurantRepository)
        {
            _barRepository = barRepository;
            _museumRepository = museumRepository;
            _restaurantRepository = restaurantRepository;
        }

        public async Task<ServiceResult> RateLocationAsync(string name,string type,int rating)
        {
            if(type == "bar")
            {
                var(result, bar) = await _barRepository.FindByNameAsync(name);
                if(result == RepositoryResult.BAR_NOT_FOUND)
                {
                    return ServiceResult.LOCATION_NAME_CAN_NOT_BE_FOUND_IN_DATABASE;
                }
                else
                {
                    var personsThatRated = bar.Rating.personsNumber;
                    var oldRating = bar.Rating.ratingValue;

                    double newRating = ((personsThatRated * oldRating) + rating) / (personsThatRated + 1);
                    personsThatRated++;
                    var ratingResult = await _barRepository.SwitchRatingAsync(newRating, name,personsThatRated);
                    if (ratingResult == RepositoryResult.BAR_SUCCESFULLY_UPDATED)
                    {
                        return ServiceResult.LOCATION_RATED_SUCCESFULLY;
                    }
                    else
                        return ServiceResult.UNABLE_TO_UPDATE_LOCATION;
                }
            }
            else if(type == "museum")
            {

                var (result, museum) = await _museumRepository.FindByNameAsync(name);
                if (result == RepositoryResult.MUSEUM_NOT_FOUND)
                {
                    return ServiceResult.LOCATION_NAME_CAN_NOT_BE_FOUND_IN_DATABASE;
                }
                else
                {
                    var personsThatRated = museum.Rating.personsNumber;
                    var oldRating = museum.Rating.ratingValue;

                    double newRating = ((personsThatRated * oldRating) + rating) / (personsThatRated + 1);
                    personsThatRated++;
                    var ratingResult = await _museumRepository.SwitchRatingAsync(newRating, name, personsThatRated);
                    if (ratingResult == RepositoryResult.MUSEUM_SUCCESFULLY_UPDATED)
                    {
                        return ServiceResult.LOCATION_RATED_SUCCESFULLY;
                    }
                    else
                        return ServiceResult.UNABLE_TO_UPDATE_LOCATION;
                }
            }
            else if (type == "restaurant")
            {
                var (result, restaurant) = await _restaurantRepository.FindByNameAsync(name);
                if (result == RepositoryResult.RESTAURANT_NOT_FOUND)
                {
                    return ServiceResult.LOCATION_NAME_CAN_NOT_BE_FOUND_IN_DATABASE;
                }
                else
                {
                    var personsThatRated = restaurant.Rating.personsNumber;
                    var oldRating = restaurant.Rating.ratingValue;

                    double newRating = ((personsThatRated * oldRating) + rating) / (personsThatRated + 1);
                    personsThatRated++;
                    var ratingResult = await _restaurantRepository.SwitchRatingAsync(newRating, name, personsThatRated);
                    if (ratingResult == RepositoryResult.RESTAURANT_SUCCESFULLY_UPDATED)
                    {
                        return ServiceResult.LOCATION_RATED_SUCCESFULLY;
                    }
                    else
                        return ServiceResult.UNABLE_TO_UPDATE_LOCATION;
                }
            }
            else
            {
                return ServiceResult.LOCATION_TYPE_SENT_IS_NOT_TREATED_IN_THE_DATABSE;
            }

        }
    }
}
