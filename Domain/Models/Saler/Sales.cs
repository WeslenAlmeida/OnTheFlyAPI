using System.Collections.Generic;

namespace Saler.Model
{
    public class Sales
    {
        public Flight Flight { get; set; }
        public List<Passenger> Passengers { get; set; }
        public bool Reserved { get; set; }
        public bool Sold { get; set; }
    }
}
