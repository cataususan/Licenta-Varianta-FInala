using Backend_TimTour.Models.LocationModels.LocationFeatures;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Backend_TimTour.Models.LocationModels.SimplifiedLocations
{
    [BsonIgnoreExtraElements]
    public class SimplifiedBar
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
    }
}
