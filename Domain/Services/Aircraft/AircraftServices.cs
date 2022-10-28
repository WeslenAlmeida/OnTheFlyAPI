using Aircraft.Models;
using Aircraft.Utils.Interface;
using Company.Dto;
using Company.Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aircraft.Services
{
    public class AircraftServices
    {
        private readonly IMongoCollection<Aircrafts> _aircraft;

        public AircraftServices(IDatabaseSettings settings)
        {
            var aircraft = new MongoClient(settings.ConnectionString);
            var database = aircraft.GetDatabase(settings.DatabaseName);
            _aircraft = database.GetCollection<Aircrafts>(settings.AircraftCollectionName);
        }

        public async Task<List<Aircrafts>> Get() => await _aircraft.Find(aircraft => true).ToListAsync();

        public async Task<Aircrafts> Get(string rab) => await _aircraft.Find(aircraft => aircraft.RAB == rab).FirstOrDefaultAsync();

        public async Task Create(Aircrafts aircraft) => await _aircraft.InsertOneAsync(aircraft);

        public async Task Put(string rab, Aircrafts aircraftIn) => await _aircraft.ReplaceOneAsync(aircraft => aircraft.RAB == rab, aircraftIn);

        public async Task Remove(string rab) => await _aircraft.DeleteOneAsync(Aircraft => Aircraft.RAB == rab);

        public async Task<Companys> GetCompany(string cnpj)
        {
            CompanyDto companyDto;
            using (HttpClient _companyClient = new HttpClient())
            {
                HttpResponseMessage response = await _companyClient.GetAsync($"https://localhost:44379/api/Company/{cnpj}");

                var companyJson = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    companyDto = JsonConvert.DeserializeObject<CompanyDto>(companyJson);

                    var company = new Companys()
                    {
                        Id = companyDto.Id,
                        CNPJ = companyDto.CNPJ,
                        Name = companyDto.Name,
                        NameOpt = companyDto.NameOpt,
                        DtOpen = companyDto.DtOpen,
                        Status = companyDto.Status,
                        Address = new()
                        {
                            ZipCode = companyDto.Address.ZipCode,
                            Street = companyDto.Address.Street,
                            Number = companyDto.Address.Number,
                            Complement = companyDto.Address.Complement,
                            City = companyDto.Address.City,
                            State = companyDto.Address.State
                        }
                    };

                    return company;
                }
                return null;
            }
        }
    }
}
