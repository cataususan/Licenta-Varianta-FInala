using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.LocationModels.SimplifiedLocations;
using Backend_TimTour.Models.ResultsModels;

namespace Backend_TimTour.Interfaces.LocationInterfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<IEnumerable<SimplifiedEvent>> GetPendingEventsAsync();
        Task<IEnumerable<SimplifiedEvent>> GetSimplifiedEventsAsync();
        Task<RepositoryResult> AddEventAsync(Event eventAdded);
    }
}
