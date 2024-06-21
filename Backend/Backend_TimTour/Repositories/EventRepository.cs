using Backend_TimTour.Interfaces.LocationInterfaces;
using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.LocationModels.SimplifiedLocations;
using Backend_TimTour.Models.ResultsModels;
using MongoDB.Driver;

namespace Backend_TimTour.Repositories
{
    public class EventRepository:IEventRepository
    {
        private readonly IMongoCollection<Event> _events;
        private readonly IMongoCollection<SimplifiedEvent> _simplifiedEvents;

        public EventRepository(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("Events");
            _events = database.GetCollection<Event>("Events");
            _simplifiedEvents = database.GetCollection<SimplifiedEvent>("Events");

        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _events.Find(events => true).ToListAsync();
        }
        public async Task<IEnumerable<SimplifiedEvent>> GetSimplifiedEventsAsync()
        {
            return await _simplifiedEvents.Find(events => true).ToListAsync();
        }
        public async Task<IEnumerable<SimplifiedEvent>> GetPendingEventsAsync()
        {
            var filter = Builders<SimplifiedEvent>.Filter.Eq("status", "pending");

            return await _simplifiedEvents.Find(filter).ToListAsync();
        }

        public async Task<RepositoryResult> AddEventAsync(Event eventAdded)
        {
            try
            {
                await _events.InsertOneAsync(eventAdded);
                return RepositoryResult.EVENT_ADDED;
            }
            catch (MongoWriteException ex)
            {
                Console.WriteLine($"Insert failed: {ex.Message}");
                return RepositoryResult.EVENT_NOT_ADDED;
            }
        }
    }
}
