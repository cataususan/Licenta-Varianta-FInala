using Backend_TimTour.Models.ResultsModels;

namespace Backend_TimTour.Interfaces.RatingInterfaces
{
    public interface IRatingService
    {
        Task<ServiceResult> RateLocationAsync(string name, string type, int rating);
    }
}
