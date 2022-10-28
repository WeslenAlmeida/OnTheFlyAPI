using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Airport.Controllers;
using Flight.Models;
using Flight.Services;
using Flight.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Flight.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly FlightsServices _flightsServices;

        public FlightsController(FlightsServices flightsServices)
        {
            _flightsServices = flightsServices;
        }


        [HttpPost("{iata:length(3)}/{rab:length(5)}/{departuredate}")]
        public async Task<ActionResult<Flights>> CreateFlightAsync(string iata, string rab, DateTime departuredate)
        {
            var destiny = await _flightsServices.GetAirportAPIAsync(iata);

            if (destiny == null)
                destiny = await _flightsServices.GetAirportWebAPIAsync(iata);

            if (destiny == null)
                return NotFound("Aeroporto não encontrado!");

            if (destiny.Country.ToUpper() != "BR")
                return BadRequest("Só é possível voo internacional!");

            //await _airportsServices.CreateAirportAsync(destiny);



            //var plane = await _aircraftServices.Get(rab);
            //if(plane == null) return NotFound();


            //verificar se a data de voo é futura

            if (FlightUtils.DepartureValidator(departuredate) == false) return BadRequest("Não é possível cadastrar voo com data passada!");




            //if (plane.Company.Status == false) return BadRequest(plane.Company);

            //if (FlightUtils.DateOpenCompanyValidator(plane.Company.DtOpen) == false) return BadRequest(plane.Company.DtOpen);

            //if (_flightsServices.GetOneAsync(departuredate, plane.rab) == null) return BadRequest(departuredate);


            //plane.DtLastFlight = System.DateTime.Now;
            //_aircraftServices.Update(plane.reb, plane);



            var flight = new Flights()
            {
                Departure = departuredate,
                Destiny = destiny,
                Plane = null,
                Sales = 0,
                Status = true
            };


            await _flightsServices.CreateFlightAsync(flight);


            return CreatedAtRoute("GetFlight", new { id = flight.Id }, flight);
        }


        [HttpGet]
        public async Task<ActionResult<List<Flights>>> GetAllAsync() => await _flightsServices.GetAllAsync();


        [HttpGet("{id:length(24)}", Name = "GetFlight")]
        public async Task<ActionResult<Flights>> GetOneAsync(string id)
        {
            var cliente = await _flightsServices.GetOneAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }


        [HttpGet("{date}/{rab:length(6)}")]
        public async Task<ActionResult<Flights>> GetOneAsync(DateTime date, string rab)
        {
            var flight = await _flightsServices.GetOneAsync(date, rab);

            if (flight == null || flight.Status == false)
            {
                return NotFound();
            }

            return flight;
        }


        [HttpPut("cancelflight/{id:length(24)}")]
        public async Task<ActionResult<Flights>> UpdateAsync(string id)
        {
            var flight = await _flightsServices.GetOneAsync(id);

            if (flight == null || flight.Status == false)
            {
                return NotFound();
            }

            flight.Status = false;

            await _flightsServices.UpdateAsync(id, flight);

            return CreatedAtRoute("GetFlight", new { id = flight.Id }, flight);
        }
    }
}
