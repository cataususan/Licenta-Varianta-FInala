using Backend_TimTour.Models.LocationModels.LocationFeatures;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend_TimTour.Models.LocationModels
{
    [BsonIgnoreExtraElements]
    public class RequestBar
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("rating")]
        public RatingModel Rating { get; set; }
        [BsonElement("location")]
        public LocationModel Location { get; set; }
        [BsonElement("address")]
        public string Adress { get; set; }
        [BsonElement("weekday_text")]
        public List<Day> Schedule { get; set; }
        [BsonElement("status")]
        public string Status { get; set; }
        [BsonElement("features")]
        public RequestBarFeatures Features { get; set; }
    }
}
