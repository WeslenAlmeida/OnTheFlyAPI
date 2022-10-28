using System.ComponentModel.DataAnnotations;
using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Passenger.Models {
    public class Passengers {

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [Required(ErrorMessage = "CPF Precisa de 11 Digitos...")]
        [StringLength(14)]
        public string Cpf { get; set; }
        
        [Required(ErrorMessage = "Nome é Campo Obrigatorio!")]
        [StringLength(30)]
        public string Name { get; set; }
        
        [StringLength(1)]
        public string Gender { get; set; }
       
        [StringLength(14)]
        public string Phone { get; set; }

        public DateTime DtBirth { get; set; }
       
        public DateTime DtRegister { get; set; }
       
        public bool Status { get; set; }
       
        public Address Address { get; set; }

    }
}
