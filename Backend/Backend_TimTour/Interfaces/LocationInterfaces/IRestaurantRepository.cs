using Backend_TimTour.Models;
using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.LocationModels.SimplifiedLocations;
using Backend_TimTour.Models.ResultsModels;

namespace Backend_TimTour.Interfaces.LocationInterfaces
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
        Task<IEnumerable<SimplifiedRestaurant>> GetSimplifiedRestaurantsAsync();
        Task<IEnumerable<Restaurant>> GetPendingRestaurantsAsync();
        Task<(RepositoryResult, Restaurant)> FindByNameAsync(string name);
        Task<RepositoryResult> SwitchRatingAsync(double rating, string name, double personsThatRated);
        Task<RepositoryResult> SwitchStatusAsync(string newStatus, string name);
        Task<RepositoryResult> AddRestaurantAsync(Restaurant restaurant);
    }
}
