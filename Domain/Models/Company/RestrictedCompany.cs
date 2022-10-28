using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Company.Models
{
    public class RestrictedCompany
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string CNPJ { get; set; }
    }
}
