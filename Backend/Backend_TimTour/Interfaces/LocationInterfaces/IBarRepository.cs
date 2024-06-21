using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.LocationModels.SimplifiedLocations;
using Backend_TimTour.Models.ResultsModels;

namespace Backend_TimTour.Interfaces.LocationInterfaces
{
    public interface IBarRepository
    {
        Task<IEnumerable<Bar>> GetAllBarsAsync();
        Task<IEnumerable<SimplifiedBar>> GetSimplifiedBarsAsync();
        Task<IEnumerable<SimplifiedBar>> GetPendingBarsAsync();
        Task<(RepositoryResult, Bar)> FindByNameAsync(string name);
        Task<RepositoryResult> SwitchRatingAsync(double rating, string name,double personsThatRated);
        Task<RepositoryResult> SwitchStatusAsync(string newStatus, string name);
        Task<RepositoryResult> AddBarAsync(Bar bar);
    }
}
