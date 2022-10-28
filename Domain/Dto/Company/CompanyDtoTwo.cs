using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Company
{
    public class CompanyDtoTwo
    {
        public string CNPJ { get; set; }
        [Required]
        [StringLength(30)]
        [JsonProperty("name")]
        public string Name { get; set; }
        [StringLength(30)]
        [JsonProperty("nameOpt")]
        public string NameOpt { get; set; }
        [JsonProperty("dtOpen")]
        public DateTime DtOpen { get; set; }
        [JsonProperty("address")]
        public AddressDtoTwo Address { get; set; }
    }
}
