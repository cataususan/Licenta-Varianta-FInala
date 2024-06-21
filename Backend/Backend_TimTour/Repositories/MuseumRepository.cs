using Backend_TimTour.Interfaces.LocationInterfaces;
using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.LocationModels.SimplifiedLocations;
using Backend_TimTour.Models.ResultsModels;
using MongoDB.Driver;

namespace Backend_TimTour.Repositories
{
    public class MuseumRepository:IMuseumRepository
    {
        private readonly IMongoCollection<Museum> _museums;
        private readonly IMongoCollection<SimplifiedMuseum> _simplifiedMuseums;

        public MuseumRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("Museums2");
            _museums = database.GetCollection<Museum>("Museums2");
            _simplifiedMuseums = database.GetCollection<SimplifiedMuseum>("Museums2");
        }
        public async Task<IEnumerable<Museum>> GetAllMuseumsAsync()
        {
            return await _museums.Find(museum => true).ToListAsync();
        }
        public async Task<IEnumerable<SimplifiedMuseum>> GetSimplifiedMuseumsAsync()
        {
            return await _simplifiedMuseums.Find(museum => true).ToListAsync();
        }
        public async Task<IEnumerable<SimplifiedMuseum>> GetPendingMuseumsAsync()
        {
            var filter = Builders<SimplifiedMuseum>.Filter.Eq("status", "pending");

            return await _simplifiedMuseums.Find(filter).ToListAsync();
        }
        public async Task<(RepositoryResult, Museum)> FindByNameAsync(string name)
        {
            var filter = Builders<Museum>.Filter.Eq(museum => museum.Name, name);

            var museumFound = await _museums.Find(filter).FirstOrDefaultAsync();

            if (museumFound != null)
            {
                return (RepositoryResult.MUSEUM_FOUND, museumFound);
            }
            else
            {
                return (RepositoryResult.MUSEUM_NOT_FOUND, museumFound);
            }
        }
        public async Task<RepositoryResult> SwitchRatingAsync(double rating, string name, double personsThatRated)
        {
            var filter = Builders<Museum>.Filter.Eq(museum => museum.Name, name);
            var update = Builders<Museum>.Update
                .Set("rating.ratingValue", rating)
                .Set("rating.personsNumber", personsThatRated);
            try
            {
                await _museums.UpdateOneAsync(filter, update);
                return RepositoryResult.MUSEUM_SUCCESFULLY_UPDATED;
            }
            catch (MongoWriteException ex)
            {
                Console.WriteLine($"Update failed: {ex.Message}");
                return RepositoryResult.MUSEUM_CAN_NOT_BE_UPDATED;
            }
        }
        public async Task<RepositoryResult> SwitchStatusAsync(string newStatus, string name)
        {
            var filter = Builders<Museum>.Filter.Eq(museum => museum.Name, name);
            var update = Builders<Museum>.Update
                .Set("status", newStatus);
            try
            {
                await _museums.UpdateOneAsync(filter, update);
                return RepositoryResult.MUSEUM_SUCCESFULLY_UPDATED;
            }
            catch (MongoWriteException ex)
            {
                Console.WriteLine($"Update failed: {ex.Message}");
                return RepositoryResult.MUSEUM_CAN_NOT_BE_UPDATED;
            }
        }
        public async Task<RepositoryResult> AddMuseumAsync(Museum museum)
        {
            try
            {
                await _museums.InsertOneAsync(museum);
                return RepositoryResult.MUSEUM_ADDED;
            }
            catch (MongoWriteException ex)
            {
                Console.WriteLine($"Insert failed: {ex.Message}");
                return RepositoryResult.MUSEUM_NOT_ADDED;
            }
        }

    }
}
