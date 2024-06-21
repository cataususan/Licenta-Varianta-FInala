using Backend_TimTour.Models.PrefferenceModels;
using Backend_TimTour.Models.RequestModels.ServiceResponses;
using Backend_TimTour.Models.ResultsModels;

namespace Backend_TimTour.Interfaces.RecommandationInterfaces
{
    public interface IRecommandationService
    {
        public Task<(ServiceResult, RestaurantRecommendationResponse)> GetRestaurantRecommandationAsync(string email);
        public Task<(ServiceResult, BarRecommandationResponse)> GetBarRecommandationAsync(string email);
        public Task<(ServiceResult, MuseumRecommandationResponse)> GetMuseumRecommandationAsync(string email);
        public Task<(ServiceResult, EventRecommandationResponse)> GetEventRecommandationAsync(string email);
    }
}
