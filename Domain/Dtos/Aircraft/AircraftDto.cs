using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Aircraft
{
    public class AircraftDto
    {
        [Required]
        public string RAB { get; set; }
        [Required]
        public int Capacity { get; set; }
    }
}
