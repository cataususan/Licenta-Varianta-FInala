using Backend_TimTour.Models.PrefferenceModels;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend_TimTour.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        public Prefference Prefference {get; set;}
    }
}
