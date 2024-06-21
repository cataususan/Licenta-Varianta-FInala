using Backend_TimTour.Interfaces.LocationInterfaces;
using Backend_TimTour.Models;
using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.LocationModels.SimplifiedLocations;
using Backend_TimTour.Models.ResultsModels;
using MongoDB.Driver;
using ZstdSharp.Unsafe;

namespace Backend_TimTour.Repositories
{
    public class RestaurantRepository:IRestaurantRepository
    {
        private readonly IMongoCollection<Restaurant> _restaurants;
        private readonly IMongoCollection<SimplifiedRestaurant> _simplifiedRestaurants;

        public RestaurantRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("Restaurants2");
            _restaurants = database.GetCollection<Restaurant>("Restaurants2");
            _simplifiedRestaurants = database.GetCollection<SimplifiedRestaurant>("Restaurants2");
        }

        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
        {
            return await _restaurants.Find(restaurant => true).ToListAsync();
        }

        public async Task<IEnumerable<SimplifiedRestaurant>> GetSimplifiedRestaurantsAsync()
        {
            return await _simplifiedRestaurants.Find(restaurant => true).Limit(10).ToListAsync();
        }
        public async Task<IEnumerable<Restaurant>> GetPendingRestaurantsAsync()
        {
            var filter = Builders<Restaurant>.Filter.Eq("status", "pending");

            return await _restaurants.Find(filter).Limit(10).ToListAsync();
        }

        public async Task<(RepositoryResult, Restaurant)> FindByNameAsync(string name)
        {
            var filter = Builders<Restaurant>.Filter.Eq(restaurant => restaurant.Name, name);

            var restaurantFound = await _restaurants.Find(filter).FirstOrDefaultAsync();

            if (restaurantFound != null)
            {
                return (RepositoryResult.RESTAURANT_FOUND, restaurantFound);
            }
            else
            {
                return (RepositoryResult.RESTAURANT_NOT_FOUND, restaurantFound);
            }
        }
        public async Task<RepositoryResult> SwitchRatingAsync(double rating, string name, double personsThatRated)
        {
            var filter = Builders<Restaurant>.Filter.Eq(restaurant => restaurant.Name, name);
            var update = Builders<Restaurant>.Update
                .Set("rating.ratingValue", rating)
                .Set("rating.personsNumber", personsThatRated);
            try
            {
                await _restaurants.UpdateOneAsync(filter, update);
                return RepositoryResult.RESTAURANT_SUCCESFULLY_UPDATED;
            }
            catch (MongoWriteException ex)
            {
                Console.WriteLine($"Update failed: {ex.Message}");
                return RepositoryResult.RESTAURANT_CAN_NOT_BE_UPDATED;
            }
        }
        public async Task<RepositoryResult> SwitchStatusAsync(string newStatus, string name)
        {
            var filter = Builders<Restaurant>.Filter.Eq(restaurant => restaurant.Name, name);
            var update = Builders<Restaurant>.Update
                .Set("status", newStatus);
            try
            {
                await _restaurants.UpdateOneAsync(filter, update);
                return RepositoryResult.RESTAURANT_SUCCESFULLY_UPDATED;
            }
            catch (MongoWriteException ex)
            {
                Console.WriteLine($"Update failed: {ex.Message}");
                return RepositoryResult.RESTAURANT_CAN_NOT_BE_UPDATED;
            }
        }
        public async Task<RepositoryResult> AddRestaurantAsync(Restaurant restaurant)
        {
            try
            {
                await _restaurants.InsertOneAsync(restaurant);
                return RepositoryResult.RESTAURANT_ADDED;
            }
            catch (MongoWriteException ex)
            {
                Console.WriteLine($"Insert failed: {ex.Message}");
                return RepositoryResult.RESTAURANT_NOT_ADDED;
            }
        }
    }
}
