using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Company.Dto
{
    public class AddressDto
    {
        [Required]
        [StringLength(9)]
        [JsonProperty("zipCode")]

        public string ZipCode { get; set; }
        [StringLength(100)]
        [JsonProperty("street")]
        public string Street { get; set; }
        public int Number { get; set; }
        [StringLength(10)]
        public string? Complement { get; set; }

        [StringLength(30)]
        [JsonProperty("city")]
        public string City { get; set; }
        [StringLength(2)]
        [JsonProperty("state")]
        public string State { get; set; }
    }
}
