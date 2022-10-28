using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Airport.Data.Interface;
using Airport.Models;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Airport.Services
{
    public class AirportsServices
    {
        private readonly IMongoCollection<Airports> _airportsServices;
        private readonly IMongoCollection<Airports> _airportsTrashServices;

        public AirportsServices(IDatabaseSettings settings)
        {
            var airport = new MongoClient(settings.ConnectionString);
            var database = airport.GetDatabase(settings.DatabaseName);
            _airportsServices = database.GetCollection<Airports>(settings.AirportsCollectionName);
            _airportsTrashServices = database.GetCollection<Airports>(settings.AirportsTrashCollectionName);
        }

        public async Task<Airports> CreateAirportAsync(Airports airport)
        {
            await _airportsServices.InsertOneAsync(airport);
            return airport;
        }

        public async Task<Airports> CreateTrashAsync(Airports airportIn)
        {
            var airportdeleted = await _airportsTrashServices.Find(airport => airport.IATA == airportIn.IATA).FirstOrDefaultAsync();

            if (airportdeleted == null)
            {
                await _airportsTrashServices.InsertOneAsync(airportIn);
                return airportIn;
            }

            return airportIn;
        }

        public async Task<List<Airports>> GetAllAsync() => await _airportsServices.Find(airport => true).ToListAsync();

        public async Task<List<Airports>> GetDeletedAsync() => await _airportsTrashServices.Find(airport => true).ToListAsync();

        public async Task<Airports> GetOneIataAsync(string iata) => await _airportsServices.Find(airport => airport.IATA.ToUpper() == iata.ToUpper()).FirstOrDefaultAsync();

        public async Task<List<Airports>> GetOneCountryAsync(string country) => await _airportsServices.Find(airport => airport.Country.ToUpper() == country.ToUpper()).ToListAsync();

        public async Task UpdateAsync(string iata, Airports airportIn) => await _airportsServices.ReplaceOneAsync(airport => airport.IATA.ToUpper() == iata.ToUpper(), airportIn);

        public async Task RemoveOneAsync(Airports airportRemove) => await _airportsServices.DeleteOneAsync(airport => airport.Id == airportRemove.Id);

    }
}
