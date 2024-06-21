using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Backend_TimTour.Models.LocationModels
{
    [BsonIgnoreExtraElements]
    public class Reservation
    {
        
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("customerName")]
        public string CustomerName { get; set; }
        [BsonElement("customerEmail")]
        public string CustomerEmail { get; set; }
        [BsonElement("reservationDate")]
        public long ReservationDate { get; set; }
        [BsonElement("locationName")]
        public string LocationName { get; set; }
        [BsonElement("locationType")]
        public string LocationType { get; set; }
        [BsonElement("status")]
        public string Status { get; set; }

    }
}
