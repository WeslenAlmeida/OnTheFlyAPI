using MongoDB.Bson.Serialization.Attributes;

namespace Aircraft.Models
{
    public class DeadfileAircrafts
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public Aircrafts DeadfilesAircrafts { get; set; }               
    }
}
