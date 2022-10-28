﻿using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Company.Models
{
    public class Address
    {               
        [Required]
        [StringLength(9)]
        [JsonProperty("cep")]
        public string ZipCode { get; set; }
        [StringLength(100)]
        [JsonProperty("logradouro")]        
        public string Street { get; set; }
        public int Number { get; set; }
        [StringLength(10)]
        public string? Complement { get; set; }        

        [StringLength(30)]
        [JsonProperty("localidade")]
        public string City { get; set; }
        [StringLength(2)]
        [JsonProperty("uf")]
        public string State { get; set; }     
    }
}
