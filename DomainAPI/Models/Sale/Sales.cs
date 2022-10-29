using DomainAPI.Models.Flight;
using DomainAPI.Models.Passenger;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DomainAPI.Models.Sale
{
    public class Sales
    {
        //public Flights Flight { get; set; }
        public Passengers Passengers { get; set; }
        //public bool Reserved { get; set; }
        //public bool Sold { get; set; }
    }
}
