using DomainAPI.Models.Flight;
using DomainAPI.Models.Passenger;
using System.Collections.Generic;

namespace DomainAPI.Models.Sale
{
    public class Sales
    {
        public Flights Flight { get; set; }
        public List<Passengers> Passengers { get; set; }
        public bool Reserved { get; set; }
        public bool Sold { get; set; }
    }
}
