using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.LocationModels.SimplifiedLocations;
using Backend_TimTour.Models.ResultsModels;

namespace Backend_TimTour.Interfaces.LocationInterfaces
{
    public interface IMuseumRepository
    {
        Task<IEnumerable<Museum>> GetAllMuseumsAsync();
        Task<IEnumerable<SimplifiedMuseum>> GetSimplifiedMuseumsAsync();
        Task<IEnumerable<SimplifiedMuseum>> GetPendingMuseumsAsync();
        Task<(RepositoryResult, Museum)> FindByNameAsync(string name);
        Task<RepositoryResult> SwitchRatingAsync(double rating, string name, double personsThatRated);
        Task<RepositoryResult> SwitchStatusAsync(string newStatus, string name);
        Task<RepositoryResult> AddMuseumAsync(Museum museum);
    }
}
