using System.Collections.Generic;
using System.Threading.Tasks;
using Airport.Models;
using Airport.Services;
using Microsoft.AspNetCore.Mvc;

namespace Airport.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AirportsController : ControllerBase
    {
        private readonly AirportsServices _airportsServices;

        public AirportsController(AirportsServices airportsServices)
        {
            _airportsServices = airportsServices;
        }

        [HttpPost]
        public async Task<ActionResult<Airports>> CreateAirportAsync(Airports airportIn)
        {
            var airport = await _airportsServices.GetOneIataAsync(airportIn.IATA);

            if (airport == null)
            {
                await _airportsServices.CreateAirportAsync(airportIn);
                return CreatedAtRoute("GetAirport", new { id = airportIn.IATA }, airportIn);
            }
            return Conflict();
        }

        [HttpGet]
        public async Task<ActionResult<List<Airports>>> GetAllAsync() => await _airportsServices.GetAllAsync();

        [HttpGet("deleted")]
        public async Task<ActionResult<List<Airports>>> GetDeletedAsync() => await _airportsServices.GetDeletedAsync();

        [HttpGet("{iata:length(3)}", Name = "GetAirport")]
        public async Task<ActionResult<Airports>> GetOneIataAsync(string iata)
        {
            var airport = await _airportsServices.GetOneIataAsync(iata);

            if (airport == null)
            {
                return NotFound();
            }

            return Ok(airport);
        }

        [HttpGet("{country:length(2)}")]
        public async Task<ActionResult<List<Airports>>> GetOneCountryAsync(string country)
        {
            var airport = await _airportsServices.GetOneCountryAsync(country);

            if (airport == null)
            {
                return NotFound();
            }

            return Ok(airport);
        }

        [HttpPut]
        public async Task<ActionResult<Airports>> UpdateAsync(string iata, Airports airportIn)
        {
            var airport = await _airportsServices.GetOneIataAsync(iata);

            if (airport == null)
            {
                return NotFound();
            }

            await _airportsServices.UpdateAsync(iata, airportIn);

            return CreatedAtRoute("GetAirport", new { id = airportIn.IATA }, airportIn);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveOneAsync(string iata)
        {
            var airport = await _airportsServices.GetOneIataAsync(iata);

            if (airport == null)
            {
                return NotFound();
            }

            await _airportsServices.CreateTrashAsync(airport);

            await _airportsServices.RemoveOneAsync(airport);

            return NoContent();
        }
    }
}
