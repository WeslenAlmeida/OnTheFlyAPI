using DomainAPI.Models.Aircraft;
using DomainAPI.Models.Flight;
using DomainAPI.Models.Passenger;
using DomainAPI.Models.Sale;
using DomainAPI.Services.Passenger;
using DomainAPI.Services.Sale;
using DomainAPI.Utils.Passenger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace Saler.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase {
        private readonly SalesService _salesService;
        Sales sl = new();
        public SaleController(SalesService salesServices) {
            _salesService = salesServices;
        }

        [HttpGet]
        public ActionResult<List<Sales>> Get() => _salesService.Get();


        [HttpPost("Sold/cpf")]
        public ActionResult<Sales> CreateSold(string cpf, DateTime date ,string rab) {

            string[] list = cpf.Split(',');
           

            for (int i = 0; i < list.Length; i++) {
                var passenger = new ConsumerController().GetPassengerAsync(list[i]);
                int age = DateTime.Now.Year - passenger.Result.DtBirth.Year;
                if (i == 0 && age < 18)
                    return BadRequest("Precisa ser Maior de 18 Anos para Comprar a Passagem!");

                sl.Passengers.Add(passenger.Result);
            }

            var fligth = new ConsumerController().GetFlightAsync(date, rab);
            sl.Flight = fligth.Result;
            if (sl.Flight.Departure==date && sl.Flight.Plane.RAB.Equals(rab)) {
                var operation = sl.Flight.Sales + sl.Passengers.Count;
                if (operation <= sl.Flight.Plane.Capacity) {
                    sl.Sold = true;
                    sl.Reserved = false;
                } else {
                    return BadRequest("Capacidade de Assentos da Aeronave está Esgotado!");
                }
            } else {
                return BadRequest("Não exite Voo Marcado para essa Data!");
            }
            
            return Ok(sl);
        }

        [HttpPost("Reserverd/cpf")]
        public ActionResult<Sales> CreateReserved(string cpf, DateTime date, string rab) {

            string[] list = cpf.Split(',');


            for (int i = 0; i < list.Length; i++) {
                var passenger = new ConsumerController().GetPassengerAsync(list[i]);
                int age = DateTime.Now.Year - passenger.Result.DtBirth.Year;
                if (i == 0 && age < 18)
                    return BadRequest("Precisa ser Maior de 18 Anos para Comprar a Passagem!");

                sl.Passengers.Add(passenger.Result);
            }
            
            sl.Flight = new ConsumerController().GetFlightAsync(date, rab).Result;
            
            if (sl.Flight.Departure == date && sl.Flight.Plane.RAB.Equals(rab)) {
                var operation = sl.Flight.Sales + sl.Passengers.Count;
                if (operation <= sl.Flight.Plane.Capacity) {
                    sl.Sold = false;
                    sl.Reserved = true;
                } else {
                    return BadRequest("Capacidade de Assentos da Aeronave está Esgotado!");
                }
            } else {
                return BadRequest("Não exite Voo Marcado para essa Data!");
            }

            return Ok(sl); 
        }

        [HttpGet("Sale")]
        public ActionResult<Sales> GetsSpecificSale(string cpf, DateTime date, string rab) {

            var passenger = new ConsumerController().GetPassengerAsync(cpf);
            sl.Flight = new ConsumerController().GetFlightAsync(date,rab).Result;

            if (passenger ==null && sl.Flight == null) {
                return BadRequest("Passageiro ou Voo não foi Encntrado!");

            } else {
                var sale = _salesService.GetSpecificSale(cpf,date,rab);
                if (sale == null) {
                    return BadRequest("Venda não Encontrada!");
                } else {
                    return Ok(sale);
                }
            }
        }

    }
}
