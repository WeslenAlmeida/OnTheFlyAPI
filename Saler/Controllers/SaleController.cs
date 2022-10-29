using DomainAPI.Models.Passenger;
using DomainAPI.Models.Sale;
using DomainAPI.Services.Sale;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Saler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly SalesService _salesService;
        Sales sl = new();
        public SaleController(SalesService salesServices)
        {
            _salesService = salesServices;
        }

        [HttpGet]
        public ActionResult<List<Sales>> Get() => _salesService.Get();


        [HttpPost("cpf")]
        public ActionResult<Sales> Create(string cpf)
        {
            
            string[] list = cpf.Split(',');

            for(int i = 0; i < list.Length; i++)
            {
                var passenger = new ConsumerController().GetPassengerAsync(list[i]);
                int idade = DateTime.Now.Year - passenger.Result.DtBirth.Year;
                if (i == 0 && idade < 18)
                    return BadRequest("Precisa ser maior de 18 anos para comprara a passagem!");
                
                sl.Passengers.Add(passenger.Result);
            }

         
            
            //var flight = new ConsumerController().GetFlightAsync(flightDate, aircraftrab);
          
            return Ok(sl);

        }
    }
}
