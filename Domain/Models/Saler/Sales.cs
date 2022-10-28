using System.Collections.Generic;
using Flight.Models;
using Passenger.Models;

namespace Saler.Model
{
    public class Sales
    {
        public Flights Flight { get; set; }
        public List<Passengers> Passengers { get; set; }
        public bool Reserved { get; set; }
        public bool Sold { get; set; }
    }
}
