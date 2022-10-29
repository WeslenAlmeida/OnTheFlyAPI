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

        public SaleController(SalesService salesServices)
        {
            _salesService = salesServices;
        }

        [HttpGet]
        public ActionResult<List<Sales>> Get() => _salesService.Get();

       
        [HttpPost("id")]
        public ActionResult<Task<Sales>> CreateAsync(string cpf, Sales sales)
        {
            var Passenger = new ConsumerController().GetPassengerAsync(cpf);
           //var flight = new ConsumerController().GetFlightAsync(flightDate, aircraftrab);

            return Ok(Passenger);

        }
    }
}
