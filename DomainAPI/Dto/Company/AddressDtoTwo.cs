using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DomainAPI.Dto.Company
{
    public class AddressDtoTwo
    {
        [Required]
        [StringLength(9)]
        [JsonProperty("zipCode")]
        public string ZipCode { get; set; }
        public int Number { get; set; }
        [StringLength(10)]
        public string? Complement { get; set; }
    }
}
