using System.Text.Json.Serialization;

namespace Backend_TimTour.Models.RequestModels.ServiceResponses
{
    public class EventRecommandationResponse
    {
        [JsonPropertyName("recommandation")]
        public EventRecommendation Recommendation { get; set; }
    }
    public class EventRecommendation
    {
        [JsonPropertyName("eventAudience")]
        public string Audience { get; set; }

        [JsonPropertyName("eventDuration")]
        public string Duration { get; set; }

        [JsonPropertyName("eventGenre")]
        public string Genre { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("eventPrice")]
        public string PriceRange { get; set; }

        [JsonPropertyName("eventTypes")]
        public string Types { get; set; }
        [JsonPropertyName("eventVenue")]
        public string Venue { get; set; }
    }
}
