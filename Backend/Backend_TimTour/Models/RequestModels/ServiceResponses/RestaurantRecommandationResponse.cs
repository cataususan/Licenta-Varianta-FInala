using System.Text.Json.Serialization;

namespace Backend_TimTour.Models.RequestModels.ServiceResponses
{
    public class RestaurantRecommendationResponse
    {
        [JsonPropertyName("recommandation")]  
        public Recommendation Recommendation { get; set; }
    }

    public class Recommendation
    {
        [JsonPropertyName("atmosphere")]
        public string Atmosphere { get; set; }

        [JsonPropertyName("cuisineTypes")]  
        public string CuisineTypes { get; set; }

        [JsonPropertyName("dietaryRestrictions")]
        public string DietaryRestrictions { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("priceRange")]
        public string PriceRange { get; set; }

        [JsonPropertyName("specialFeatures")]
        public string SpecialFeatures { get; set; }
    }
}
