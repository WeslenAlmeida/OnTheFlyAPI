using DomainAPI.Models.Aircraft;
using DomainAPI.Services.Aircraft;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aircraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeadfileAircraftController : ControllerBase
    {
        private readonly DeadfileAircraftServices _deadfileAircraftServices;

        public DeadfileAircraftController(DeadfileAircraftServices deadfileAircraftServices)
        {
            _deadfileAircraftServices = deadfileAircraftServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<DeadfileAircrafts>>> Get() => await _deadfileAircraftServices.Get();

        [HttpGet("{rab}", Name = "GetFile")]
        public async Task<ActionResult<DeadfileAircrafts>> Get(string rab) => await _deadfileAircraftServices.Get(rab);

        [HttpPost]
        public async Task Create(DeadfileAircrafts deadfile)
        {
            await _deadfileAircraftServices.Create(deadfile);
        }

        [HttpPut]
        public async Task<IActionResult> Put(string rab)
        {
            var file = await _deadfileAircraftServices.Get(rab);

            if (file is null) return NotFound();

            return Ok("Objeto encontrado, porém não pode ser editado!");
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(string rab)
        {
            var file = await _deadfileAircraftServices.Get(rab);

            if (file is null) return NotFound();

            return Ok("Objeto encontrado, porém não pode ser excluido!");
        }
    }
}
