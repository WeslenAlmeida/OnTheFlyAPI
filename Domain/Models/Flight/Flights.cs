using MongoDB.Bson.Serialization.Attributes;
using Airport.Models;
using System;
using System.ComponentModel.DataAnnotations;
using Aircraft.Models;

namespace Flight.Models
{
    public class Flights
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        public Airports Destiny { get; set; }

        [Required]
        public AirCraft Plane { get; set; }

        [Required]
        public int Sales { get; set; }

        [Required]
        public DateTime Departure { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
