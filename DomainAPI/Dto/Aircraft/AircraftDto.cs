using System.ComponentModel.DataAnnotations;

namespace DomainAPI.Dto.Aircraft
{
    public class AircraftDto
    {
        [Required]
        public string RAB { get; set; }
        [Required]
        public int Capacity { get; set; }
    }
}
