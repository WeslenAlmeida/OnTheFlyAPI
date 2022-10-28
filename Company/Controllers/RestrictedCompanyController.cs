﻿using DomainAPI.Models.Company;
using DomainAPI.Services.Company;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestrictedCompanyController : ControllerBase
    {
        private readonly RestrictedCompanyServices _restrictedCompanyServices;
        private readonly CompanyServices _companyServices;

        public RestrictedCompanyController(RestrictedCompanyServices restrictedCompanyServices, CompanyServices companyServices)
        {
            _restrictedCompanyServices = restrictedCompanyServices;
            _companyServices = companyServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<RestrictedCompany>>> Get() => await _restrictedCompanyServices.Get();

        [HttpGet("{cnpj}", Name = "GetRestrict")]
        public async Task<ActionResult<RestrictedCompany>> Get(string cnpj) => await _restrictedCompanyServices.Get(cnpj);

        [HttpPost]
        public async Task<ActionResult<RestrictedCompany>> Create(RestrictedCompany restrict)
        {
            var cnpj = restrict.CNPJ.Replace(".", "").Replace("-", "").Replace("/", "").Replace("%2F", "");

            restrict.CNPJ = cnpj;

            var restrictIn = await _restrictedCompanyServices.Get(cnpj);

            if (restrictIn is not null) return BadRequest("CNPJ já cadastrado!");

            var company = await _companyServices.Get(cnpj);

            if (company is not null)
            {
                company.Status = false;
            }

            await _companyServices.Put(cnpj, company);

            await _restrictedCompanyServices.Create(restrict);

            return Ok(restrict);
        }

        [HttpPut]
        public async Task<IActionResult> Put(string cnpj)
        {
            var restricted = await _restrictedCompanyServices.Get(cnpj);

            if (restricted is null) return BadRequest();

            await _restrictedCompanyServices.Put(cnpj, restricted);
            
            return Ok();
        }

        [HttpDelete("{cnpj}")]
        public async Task<IActionResult> Remove(string cnpj)
        {
            var cnpjIn = cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Replace("%2F", "");
                        
            var restricted = await _restrictedCompanyServices.Get(cnpjIn);

            if(restricted is null) return NotFound();

            var company = await _companyServices.Get(cnpj);

            if (company is not null) company.Status = true;
                       
            await _companyServices.Put(cnpj, company);

            await _restrictedCompanyServices.Remove(cnpj);

            return NoContent();
        } 
    }
}