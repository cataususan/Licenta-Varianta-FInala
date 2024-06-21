using System.Text.Json.Serialization;

namespace Backend_TimTour.Models.RequestModels.ServiceResponses
{
    public class BarRecommandationResponse
    {
        [JsonPropertyName("recommandation")]
        public BarRecommendation Recommendation { get; set; }
    }
    public class BarRecommendation
    {
        [JsonPropertyName("barAmbiance")]
        public string Ambiance { get; set; }

        [JsonPropertyName("barDrinkSpecialties")]
        public string DrinkSpecialties { get; set; }

        [JsonPropertyName("barEvent")]
        public string Event { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("priceRange")]
        public string PriceRange { get; set; }

        [JsonPropertyName("barFoodOptions")]
        public string FoodOptions { get; set; }
    }
}
