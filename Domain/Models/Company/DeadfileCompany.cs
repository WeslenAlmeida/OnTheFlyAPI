using MongoDB.Bson.Serialization.Attributes;

namespace Company.Models
{
    public class DeadfileCompany
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public Companys FileCompany { get; set; }
    }
}
