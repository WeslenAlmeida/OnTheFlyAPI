﻿using DomainAPI.Models.Aircraft;
using DomainAPI.Models.Flight;
using DomainAPI.Models.Passenger;
using DomainAPI.Models.Sale;
using DomainAPI.Services.Flight;
using DomainAPI.Services.Passenger;
using DomainAPI.Services.Sale;
using DomainAPI.Utils.Passenger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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
        public ActionResult<Sales> CreateSold(string cpf, DateTime date, string rab) {
          
            string[] list = cpf.Split(',');
            
           
            List<Passengers> peoples = new List<Passengers>();
           
            for (int i = 0; i < list.Length; i++) {
                var passenger = new ConsumerController().GetPassengerAsync(list[i]);
                int age = DateTime.Now.Year - passenger.Result.DtBirth.Year;
                if (i == 0 && age < 18) {
                    return BadRequest("Precisa ser Maior de 18 Anos para Comprar a Passagem!");
                } else {
                   
                    peoples.Add(passenger.Result);
                    var sale = _salesService.GetSpecificSale(passenger.Result,date,rab);
                    if (sale == null) {
                        sl.Passengers = peoples;
                    } else {
                        return BadRequest("Venda já foi Cadastrada com esse CPF!");
                    }
                }
            }
            
            var fligth = new ConsumerController().GetFlightAsync(date, rab);
            sl.Flight = fligth.Result;
            if (sl.Flight.Departure == date && sl.Flight.Plane.RAB.Equals(rab)) {
                
                if (sl.Flight.Sales + sl.Passengers.Count <= sl.Flight.Plane.Capacity) {
                    sl.Sold = true;
                    sl.Reserved = false;
                    sl.Flight.Sales += sl.Passengers.Count;
                    
                } else {
                    return BadRequest("Capacidade de Assentos da Aeronave está Esgotado!");
                }
            } else {
                return BadRequest("Não exite Voo Marcado para essa Data!");
            }


            var result = new ConsumerController().PutFlightAsync(sl.Flight);
            _salesService.Create(sl);
            
            return Ok(sl);
        }

        [HttpPost("Reserverd/cpf")]
        public ActionResult<Sales> CreateReserved(string cpf, DateTime date, string rab)
        {

            string[] list = cpf.Split(',');


            List<Passengers> peoples = new List<Passengers>();

            for (int i = 0; i < list.Length; i++)
            {
                var passenger = new ConsumerController().GetPassengerAsync(list[i]);
                int age = DateTime.Now.Year - passenger.Result.DtBirth.Year;
                if (i == 0 && age < 18)
                {
                    return BadRequest("Precisa ser Maior de 18 Anos para Comprar a Passagem!");
                }
                else
                {
                    //date.ToString("yyyy/MM/ddZ");
                    peoples.Add(passenger.Result);
                    var sale = _salesService.GetSpecificSale(passenger.Result, date, rab);
                    if (sale == null)
                    {
                        sl.Passengers = peoples;
                    }
                    else
                    {
                        return BadRequest("Venda já foi Cadastrada com esse CPF!");
                    }
                }
            }

            var fligth = new ConsumerController().GetFlightAsync(date, rab);
            sl.Flight = fligth.Result;
            if (sl.Flight.Departure == date && sl.Flight.Plane.RAB.Equals(rab))
            {
                if (sl.Flight.Sales + sl.Passengers.Count <= sl.Flight.Plane.Capacity)
                {
                    sl.Sold = false;
                    sl.Reserved = true;
                    sl.Flight.Sales += sl.Passengers.Count;
                }
                else
                {
                    return BadRequest("Capacidade de Assentos da Aeronave está Esgotado!");
                }
            }
            else
            {
                return BadRequest("Não exite Voo Marcado para essa Data!");
            }

            var result = new ConsumerController().PutFlightAsync(sl.Flight);
            _salesService.Create(sl);

            return Ok(sl);
        }

        [HttpGet("Sale")]
        public ActionResult<Sales> GetsSpecificSale(string cpf, DateTime date, string rab)
        {

            var passenger = new ConsumerController().GetPassengerAsync(cpf);
            sl.Flight = new ConsumerController().GetFlightAsync(date, rab).Result;

            if (passenger == null && sl.Flight == null)
            {
                return BadRequest("Passageiro ou Voo não foi Encntrado!");

            }
            else
            {
                var sale = _salesService.GetSpecificSale(passenger.Result, date, rab);
                if (sale == null)
                {
                    return BadRequest("Venda não Encontrada!");
                }
                else
                {
                    return Ok(sale);
                }
            }
        }

    }
}
