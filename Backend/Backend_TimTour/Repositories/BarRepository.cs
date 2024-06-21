using Backend_TimTour.Interfaces.LocationInterfaces;
using Backend_TimTour.Models;
using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.LocationModels.SimplifiedLocations;
using Backend_TimTour.Models.PrefferenceModels;
using Backend_TimTour.Models.ResultsModels;
using MongoDB.Driver;

namespace Backend_TimTour.Repositories
{
    public class BarRepository:IBarRepository
    {
        private readonly IMongoCollection<Bar> _bars;
        private readonly IMongoCollection<SimplifiedBar> _simplifiedBars;

        public BarRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("Bars2");
            _bars = database.GetCollection<Bar>("Bars2");
            _simplifiedBars = database.GetCollection<SimplifiedBar>("Bars2");
        }
        public async Task<IEnumerable<Bar>> GetAllBarsAsync()
        {
            return await _bars.Find(bar => true).ToListAsync();
        }
        public async Task<IEnumerable<SimplifiedBar>> GetSimplifiedBarsAsync()
        {
            return await _simplifiedBars.Find(bar => true).ToListAsync();
        }
        public async Task<IEnumerable<SimplifiedBar>> GetPendingBarsAsync()
        {
            var filter = Builders<SimplifiedBar>.Filter.Eq("status", "pending");

            return await _simplifiedBars.Find(filter).ToListAsync();
        }
        public async Task<(RepositoryResult, Bar)> FindByNameAsync(string name)
        {
            var filter = Builders<Bar>.Filter.Eq(bar => bar.Name, name);

            var barFound = await _bars.Find(filter).FirstOrDefaultAsync();

            if (barFound != null)
            {
                return (RepositoryResult.BAR_FOUND, barFound);
            }
            else
            {
                return (RepositoryResult.BAR_NOT_FOUND, barFound);
            }
        }
        public async Task<RepositoryResult> SwitchRatingAsync(double rating, string name, double personsThatRated)
        {
            var filter = Builders<Bar>.Filter.Eq(bar => bar.Name, name);
            var update = Builders<Bar>.Update
                .Set("rating.ratingValue", rating)
                .Set("rating.personsNumber", personsThatRated);
            try
            {
                await _bars.UpdateOneAsync(filter, update);
                return RepositoryResult.BAR_SUCCESFULLY_UPDATED;
            }
            catch (MongoWriteException ex)
            {
                Console.WriteLine($"Update failed: {ex.Message}");
                return RepositoryResult.BAR_CAN_NOT_BE_UPDATED;
            }
        }
        public async Task<RepositoryResult> SwitchStatusAsync(string newStatus, string name)
        {
            var filter = Builders<Bar>.Filter.Eq(bar => bar.Name, name);
            var update = Builders<Bar>.Update
                .Set("status", newStatus);
            try
            {
                await _bars.UpdateOneAsync(filter, update);
                return RepositoryResult.BAR_SUCCESFULLY_UPDATED;
            }
            catch (MongoWriteException ex)
            {
                Console.WriteLine($"Update failed: {ex.Message}");
                return RepositoryResult.BAR_CAN_NOT_BE_UPDATED;
            }
        }
        public async Task<RepositoryResult> AddBarAsync(Bar bar)
        {
            try
            {
                await _bars.InsertOneAsync(bar);
                return RepositoryResult.BAR_ADDED;
            }
            catch (MongoWriteException ex)
            {
                Console.WriteLine($"Insert failed: {ex.Message}");
                return RepositoryResult.BAR_NOT_ADDED;
            }
        }
    }
}
