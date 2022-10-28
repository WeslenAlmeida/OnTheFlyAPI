using Passenger.Util;
using System.ComponentModel.DataAnnotations;
using System;

namespace Passenger.Models {
    public class PassengerDTO {

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

        public PassengerDTO(string cpf, string phone, bool status, string gender) {


            this.Cpf = PassengerUtil.MaskCPF(cpf);
            this.Phone = PassengerUtil.MaskPhone(phone);
            this.Gender = gender.ToUpper();
            this.Status = status;




        }


    }
}
