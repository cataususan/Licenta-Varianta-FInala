using Backend_TimTour.Models.LocationModels;

namespace Backend_TimTour.Interfaces.Parsers
{
    public interface IRestaurantParser
    {
        Task<(Restaurant, bool)> ParseRequestToRestaurant(RequestRestaurant restaurantToParse);
        RequestRestaurant ParseRestaurantToDatabaseObject(Restaurant restaurantToParse);
    }
}
