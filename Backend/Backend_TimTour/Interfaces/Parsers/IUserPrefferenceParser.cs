using Backend_TimTour.Models.LocationModels;
using Backend_TimTour.Models.LocationModels.LocationFeatures;
using Backend_TimTour.Models.PrefferenceModels;

namespace Backend_TimTour.Interfaces.Parsers
{
    public interface IUserPrefferenceParser
    {
        Task<(Prefference, bool)> ParseRequestToPrefference(RequestBarFeatures barFeatureToParse, RequestEventFeature eventFeatureToParse, RequestMuseumFeature museumFeatureToParse, RequestRestaurantFeature restaurantFeatureToParse);
    }
}
