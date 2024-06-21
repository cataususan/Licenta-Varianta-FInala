using System.Text.Json.Serialization;

namespace Backend_TimTour.Models.RequestModels.ServiceResponses
{
    public class MuseumRecommandationResponse
    {

        [JsonPropertyName("recommandation")]
        public MuseumRecommendation Recommendation { get; set; }
    }
    public class MuseumRecommendation
    {
        [JsonPropertyName("museumAccesibility")]
        public string Accesibility { get; set; }

        [JsonPropertyName("museumExhibitsTypes")]
        public string ExhibitsTypes { get; set; }

        [JsonPropertyName("museumTypes")]
        public string MuseumTypes { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("priceRange")]
        public string PriceRange { get; set; }

        [JsonPropertyName("museumVisitorService")]
        public string VisitorService { get; set; }
    }
}
