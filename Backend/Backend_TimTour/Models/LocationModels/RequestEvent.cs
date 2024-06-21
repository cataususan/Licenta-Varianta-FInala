using Backend_TimTour.Models.LocationModels.LocationFeatures;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Backend_TimTour.Models.LocationModels
{
    [BsonIgnoreExtraElements]
    public class RequestEvent
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("location")]
        public LocationModel Location { get; set; }
        [BsonElement("address")]
        public string Adress { get; set; }
        [BsonElement("eventDate")]
        public long EventDate { get; set; }
        [BsonElement("status")]
        public string Status { get; set; }
        [BsonElement("features")]
        public RequestEventFeature Features { get; set; }
    }
}
