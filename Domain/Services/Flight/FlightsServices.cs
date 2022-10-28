using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Airport.Models;
using Flight.Data.Interface;
using Flight.Models;
using Flight.Utils;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Flight.Services
{
    public class FlightsServices
    {
        private readonly IMongoCollection<Flights> _flightsServices;

        public FlightsServices(IDatabaseSettings settings)
        {
            var flight = new MongoClient(settings.ConnectionString);
            var database = flight.GetDatabase(settings.DatabaseName);
            _flightsServices = database.GetCollection<Flights>(settings.FlightsCollectionName);
        }

        public async Task<Flights> CreateFlightAsync(Flights flight)
        {
            await _flightsServices.InsertOneAsync(flight);
            return flight;
        }

        public async Task<List<Flights>> GetAllAsync() => await _flightsServices.Find(flight => true).ToListAsync();

        public async Task<Flights> GetOneAsync(string id) => await _flightsServices.Find(flight => flight.Id == id).FirstOrDefaultAsync();

        public async Task<Flights> GetOneAsync(DateTime date, string rab) => await _flightsServices.Find(flight => flight.Departure.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy") &&  flight.Plane.RAB.ToUpper() == rab.ToUpper()).FirstOrDefaultAsync();

        public async Task UpdateAsync(string id, Flights flightIn) => await _flightsServices.ReplaceOneAsync(flight => flight.Id == id, flightIn);
        
        public async Task<Airports> GetAirportWebAPIAsync(string iata)
        {
            var httpclient = new HttpClient();
            var airportresponse = await httpclient.GetAsync(FlightUtils.GetAirportWEBAPIUri() + iata);
            var JsonString = await airportresponse.Content.ReadAsStringAsync();
            var airport = JsonConvert.DeserializeObject<Airports>(JsonString);

            return airport;
        }

        public async Task<Airports> GetAirportAPIAsync(string iata)
        {
            var httpclient = new HttpClient();
            var airportresponse = await httpclient.GetAsync(FlightUtils.GetAirportAPIUri() + iata);
            var JsonString = await airportresponse.Content.ReadAsStringAsync();
            var airport = JsonConvert.DeserializeObject<Airports>(JsonString);

            return airport;
        }

        public async Task<Airports> CreateAirportAPIAsync(string iata)
        {
            var httpclient = new HttpClient();
            var airportresponse = await httpclient.GetAsync(FlightUtils.GetAirportWEBAPIUri() + iata);
            var JsonString = await airportresponse.Content.ReadAsStringAsync();
            var airport = JsonConvert.DeserializeObject<Airports>(JsonString);

            return airport;
        }
    }
}
