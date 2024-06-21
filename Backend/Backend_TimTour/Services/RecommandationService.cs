using Amazon.Runtime.Internal;
using Backend_TimTour.Interfaces.LocationInterfaces;
using Backend_TimTour.Interfaces.RecommandationInterfaces;
using Backend_TimTour.Interfaces.UserInterfaces;
using Backend_TimTour.Models.PrefferenceModels;
using Backend_TimTour.Models.RequestModels;
using Backend_TimTour.Models.RequestModels.ServiceResponses;
using Backend_TimTour.Models.ResultsModels;
using Backend_TimTour.Parsers.ServicesRequestsParsers;
using MongoDB.Bson;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Backend_TimTour.Services
{
    public class RecommandationService :IRecommandationService
    {
        private readonly IUserRepository _userRepository;
        static readonly HttpClient client = new HttpClient();
        public RecommandationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<(ServiceResult, RestaurantRecommendationResponse)> GetRestaurantRecommandationAsync(string email)
        {
            RestaurantRecommendationResponse prefferenceReturned = new RestaurantRecommendationResponse();
            var (result,User) = await _userRepository.FindByEmailAsync(email);
            var prefference = RestaurantPrefferenceParser.CreatePreference(User.Prefference.RestaurantFeatures.priceRange.ToString(),
                User.Prefference.RestaurantFeatures.specialFeatures.ToString(),
                User.Prefference.RestaurantFeatures.dietaryRestrictions.ToString(),
                User.Prefference.RestaurantFeatures.cusineTypes.ToString(),
                User.Prefference.RestaurantFeatures.atmosphere.ToString());
            if (result == RepositoryResult.USER_NOT_FOUND)
            {
                return (ServiceResult.EMAIL_DOES_NOT_EXIST_IN_DB, prefferenceReturned);
            }
            else
            {
                string url = "http://host.docker.internal:5000/recommendRestaurant";
                try
                {

                    string json = System.Text.Json.JsonSerializer.Serialize(prefference);

                    using (HttpContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                    {

                        HttpResponseMessage response = await client.PostAsync(url, content);

                        string serviceResult = await response.Content.ReadAsStringAsync();
                        var recommandation = JsonSerializer.Deserialize<RestaurantRecommendationResponse>(serviceResult);
                        return (ServiceResult.EMAIL_DOES_NOT_EXIST_IN_DB, recommandation);
                    }
                    
                }
                catch
                {
                    return (ServiceResult.REQUEST_TO_THE_RECOMMANDATION_SERVICE_FAILED, prefferenceReturned);
                }
            }
        }
        public async Task<(ServiceResult, BarRecommandationResponse)> GetBarRecommandationAsync(string email)
        {
            BarRecommandationResponse prefferenceReturned = new BarRecommandationResponse();
            var (result, User) = await _userRepository.FindByEmailAsync(email);
            var prefference = BarPrefferenceParser.CreatePreference(User.Prefference.BarFeatures.PriceRange.ToString(),
                User.Prefference.BarFeatures.barAmbiance.ToString(),
                User.Prefference.BarFeatures.barDrinkSpecialties.ToString(),
                User.Prefference.BarFeatures.barEvent.ToString(),
                User.Prefference.BarFeatures.barFoodOptions.ToString());
            if (result == RepositoryResult.USER_NOT_FOUND)
            {
                return (ServiceResult.EMAIL_DOES_NOT_EXIST_IN_DB, prefferenceReturned);
            }
            else
            {
                string url = "http://host.docker.internal:5000/recommendBar";
                try
                {

                    string json = System.Text.Json.JsonSerializer.Serialize(prefference);

                    using (HttpContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                    {
                        HttpResponseMessage response = await client.PostAsync(url, content);

                        string serviceResult = await response.Content.ReadAsStringAsync();
                        var recommandation = JsonSerializer.Deserialize<BarRecommandationResponse>(serviceResult);
                        return (ServiceResult.EMAIL_DOES_NOT_EXIST_IN_DB, recommandation);
                    }

                }
                catch
                {
                    return (ServiceResult.REQUEST_TO_THE_RECOMMANDATION_SERVICE_FAILED, prefferenceReturned);
                }
            }
        }
        public async Task<(ServiceResult, MuseumRecommandationResponse)> GetMuseumRecommandationAsync(string email)
        {
            MuseumRecommandationResponse prefferenceReturned = new MuseumRecommandationResponse();
            var (result, User) = await _userRepository.FindByEmailAsync(email);
            var prefference = MuseumPrefferenceParser.CreatePreference(User.Prefference.MuseumFeatures.PriceRange.ToString(),
                User.Prefference.MuseumFeatures.museumAccesibility.ToString(),
                User.Prefference.MuseumFeatures.museumExhibitsTypes.ToString(),
                User.Prefference.MuseumFeatures.museumTypes.ToString(),
                User.Prefference.MuseumFeatures.museumVisitorService.ToString());
            if (result == RepositoryResult.USER_NOT_FOUND)
            {
                return (ServiceResult.EMAIL_DOES_NOT_EXIST_IN_DB, prefferenceReturned);
            }
            else
            {
                string url = "http://host.docker.internal:5000/recommendMuseum";
                try
                {

                    string json = System.Text.Json.JsonSerializer.Serialize(prefference);

                    using (HttpContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                    {
                        HttpResponseMessage response = await client.PostAsync(url, content);

                        string serviceResult = await response.Content.ReadAsStringAsync();
                        var recommandation = JsonSerializer.Deserialize<MuseumRecommandationResponse>(serviceResult);
                        return (ServiceResult.EMAIL_DOES_NOT_EXIST_IN_DB, recommandation);
                    }

                }
                catch
                {
                    return (ServiceResult.REQUEST_TO_THE_RECOMMANDATION_SERVICE_FAILED, prefferenceReturned);
                }
            }
        }
        public async Task<(ServiceResult, EventRecommandationResponse)> GetEventRecommandationAsync(string email)
        {
            EventRecommandationResponse prefferenceReturned = new EventRecommandationResponse();
            var (result, User) = await _userRepository.FindByEmailAsync(email);
            var prefference = EventPrefferenceParser.CreatePreference(User.Prefference.EventFeatures.eventPrice.ToString(),
                User.Prefference.EventFeatures.eventAudience.ToString(),
                User.Prefference.EventFeatures.eventDuration.ToString(),
                User.Prefference.EventFeatures.eventGenre.ToString(),
                User.Prefference.EventFeatures.eventTypes.ToString(),
                User.Prefference.EventFeatures.eventVenue.ToString());
            if (result == RepositoryResult.USER_NOT_FOUND)
            {
                return (ServiceResult.EMAIL_DOES_NOT_EXIST_IN_DB, prefferenceReturned);
            }
            else
            {
                string url = "http://host.docker.internal:5000/recommendEvent";
                try
                {

                    string json = System.Text.Json.JsonSerializer.Serialize(prefference);

                    using (HttpContent content = new StringContent(json, Encoding.UTF8, "application/json"))
                    {
                        HttpResponseMessage response = await client.PostAsync(url, content);

                        string serviceResult = await response.Content.ReadAsStringAsync();
                        var recommandation = JsonSerializer.Deserialize<EventRecommandationResponse>(serviceResult);
                        return (ServiceResult.EMAIL_DOES_NOT_EXIST_IN_DB, recommandation);
                    }

                }
                catch
                {
                    return (ServiceResult.REQUEST_TO_THE_RECOMMANDATION_SERVICE_FAILED, prefferenceReturned);
                }
            }
        }

    }
}
