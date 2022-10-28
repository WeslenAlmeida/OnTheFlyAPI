using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Company
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
