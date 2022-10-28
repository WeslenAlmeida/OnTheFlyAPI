﻿using Company.Models;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System;

namespace Company.Dto
{
    public class CompanyDto
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [JsonProperty("id")]
        public string Id { get; set; }
        [Required]
        [StringLength(19)]
        [JsonProperty("cnpj")]
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
        public AddressDto Address { get; set; }
        [JsonProperty("status")]
        public bool Status { get; set; }
    }
}
