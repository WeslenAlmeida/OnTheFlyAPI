using Company.Models;
using Company.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeadfileCompanyController : ControllerBase
    {
        private readonly DeadfileCompanyServices _deadfileCompanyServices;

        public DeadfileCompanyController(DeadfileCompanyServices deadfileCompanyServices)
        {
            _deadfileCompanyServices = deadfileCompanyServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<DeadfileCompany>>> Get() => await _deadfileCompanyServices.Get();

        [HttpGet("{cnpj}")]
        public async Task<ActionResult<DeadfileCompany>> Get(string cnpj)
        {
            var deadfileCompany = await _deadfileCompanyServices.Get(cnpj);

            if (deadfileCompany is null) return NotFound();

            return Ok(deadfileCompany);
        }

        [HttpPost]
        public async Task Create(DeadfileCompany deadfile)
        {
            await _deadfileCompanyServices.Create(deadfile);            
        }

        [HttpPut]
        public async Task Put(string cnpj, DeadfileCompany deadfile) => await _deadfileCompanyServices.Put(cnpj, deadfile);

        [HttpDelete("{cnpj}")]
        public async Task Remove(string cnpj) => await _deadfileCompanyServices.Remove(cnpj);
    }
}
