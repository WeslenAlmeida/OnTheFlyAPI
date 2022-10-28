using Company.Models;
using Company.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Dtos.Company;

namespace Company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyServices _companyServices;
        private readonly DeadfileCompanyServices _deadfilesServices;
        private readonly AddressServices _addressServives;

        public CompanyController(CompanyServices companyServices, DeadfileCompanyServices deadfilesServices, 
            AddressServices addressSerives)
        {
            _companyServices = companyServices;
            _deadfilesServices = deadfilesServices;
            _addressServives = addressSerives;
        }

        [HttpGet]
        public async Task<ActionResult<List<Companys>>> Get() => await _companyServices.Get();

        [HttpGet("{cnpjIn}")]
        public async Task<ActionResult<Companys>> Get(string cnpjIn)
        {
            var cnpj = cnpjIn.Replace(".", "").Replace("-", "").Replace("/", "").Replace("%2F", "");

            var company = await _companyServices.Get(cnpj);

            if (company is null) return NotFound();

            return company;
        }

        [HttpPost]
        public async Task<ActionResult<Companys>> Create(CompanyDtoTwo company)
        {            
            var cnpj = company.CNPJ.Replace(".", "").Replace("-", "").Replace("/", "").Replace("%2F", "");

            if (!IsCnpj(cnpj)) return BadRequest("CNPJ informado não é válido!");

            var companyIn = await _companyServices.Get(cnpj);

            if (companyIn is null)
            {
                var address = await _addressServives.GetAddress(company.Address.ZipCode);

                var addressIn = new Address()
                {
                    ZipCode = address.ZipCode,
                    Street = address.Street,
                    Number = company.Address.Number,
                    Complement = company.Address.Complement,
                    City = address.City,
                    State = address.State
                };

                companyIn = new Companys()
                {
                    CNPJ = cnpj,
                    Name = company.Name,
                    NameOpt = company.NameOpt,
                    DtOpen = company.DtOpen,
                    Address = addressIn,
                    Status = true
                };
                if (string.IsNullOrWhiteSpace(companyIn.Name) || companyIn.Name == "string")
                    return BadRequest("Nome da Razão Social não pode ser Nulo");

                if (string.IsNullOrWhiteSpace(companyIn.NameOpt) || companyIn.NameOpt == "string")
                    companyIn.NameOpt = companyIn.Name;

                if (companyIn.DtOpen > DateTime.Now) return BadRequest("Data de abertura do CNPJ não pode ser futura!");
                
                await _companyServices.Create(companyIn);              

                return Created("Get", companyIn);
            }

            return BadRequest("Companhia já possue cadastro!");
        }

        [HttpPut("Status/{cnpjIn}")]
        public async Task<IActionResult> PutStatus(string cnpjIn)
        {
            var cnpj = cnpjIn.Replace(".", "").Replace("-", "").Replace("/", "").Replace("%2F", "");

            var companyIn = await _companyServices.Get(cnpj);

            if (companyIn is null) return NotFound();

            if (companyIn.Status == true) companyIn.Status = false;

            else companyIn.Status = true;

            await _companyServices.Put(cnpj, companyIn);

            return Ok();
        }

        [HttpPut("Cep/{cnpjIn}/{cep}")]
        public async Task<IActionResult> PutCep(string cnpjIn, string cep)
        {
            var cnpj = cnpjIn.Replace(".", "").Replace("-", "").Replace("/", "").Replace("%2F", "");

            var companyIn = await _companyServices.Get(cnpj);

            if (companyIn is null) return NotFound();

            var address = await _addressServives.GetAddress(cep);

            companyIn.Address.ZipCode = address.ZipCode;
            companyIn.Address.Street = address.Street;
            companyIn.Address.City = address.City;
            companyIn.Address.State = address.State;

            await _companyServices.Put(cnpj, companyIn);

            return Ok();
        }

        [HttpPut("Numero/{cnpjIn}/{numero}")]
        public async Task<IActionResult> PutNumber(string cnpjIn, int numero)
        {
            var cnpj = cnpjIn.Replace(".", "").Replace("-", "").Replace("/", "").Replace("%2F", "");

            var companyIn = await _companyServices.Get(cnpj);

            if (companyIn is null) return NotFound();            

            companyIn.Address.Number = numero;            

            await _companyServices.Put(cnpj, companyIn);

            return Ok();
        }

        [HttpPut("Complemento/{cnpjIn}/{complemento}")]
        public async Task<IActionResult> PutComplement(string cnpjIn, string complemento)
        {
            var cnpj = cnpjIn.Replace(".", "").Replace("-", "").Replace("/", "").Replace("%2F", "");

            var companyIn = await _companyServices.Get(cnpj);

            if (companyIn is null) return NotFound();

            companyIn.Address.Complement = complemento;

            await _companyServices.Put(cnpj, companyIn);

            return Ok();
        }

        [HttpDelete("{cnpjIn}")]
        public async Task Remove(string cnpjIn)
        {
            var cnpj = cnpjIn.Replace(".", "").Replace("-", "").Replace("/", "").Replace("%2F", "");

            var company = await _companyServices.Get(cnpj);

            if (company is null) return;

            var archiveCompany = new DeadfileCompany();

            archiveCompany.FileCompany = company;

            await _deadfilesServices.Create(archiveCompany);

            await _companyServices.Remove(cnpj);
        }

        public static bool IsCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }
    }
}