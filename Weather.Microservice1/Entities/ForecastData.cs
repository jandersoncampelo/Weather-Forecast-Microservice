using MongoDB.Bson.Serialization.Attributes;

namespace Weather.Microservice1.Entities
{
    public class ForecastData
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("Data")]
        public object Data { get; set; }
    }
}
