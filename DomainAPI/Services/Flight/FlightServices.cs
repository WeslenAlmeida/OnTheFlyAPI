using DomainAPI.Models.Airport;
using DomainAPI.Models.Flight;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using DomainAPI.Database.Flight.Interface;
using Newtonsoft.Json;
using DomainAPI.Utils.FlightUtils;
using DomainAPI.Models.Aircraft;
using Nancy.Json;
using Microsoft.AspNetCore.Mvc;

namespace DomainAPI.Services.Flight
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

        public async Task<Flights> GetOneAsync(DateTime date, string rab)
        {
            var flightsRabList = await _flightsServices.Find(flight => flight.Plane.RAB.ToUpper() == rab.ToUpper()).ToListAsync();
            foreach (var flight in flightsRabList)
            {
                if (flight.Departure.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy")) return flight;
            }
            return null;
        }

        public async Task<List<Flights>> GetByDateAsync(DateTime date)
        {
            var flightsDateList = await _flightsServices.Find(flight => true).ToListAsync();
            List<Flights> Listflight = new();
            foreach (var flight in flightsDateList)
            {
                if (flight.Departure.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy") && flight.Status == true)
                    Listflight.Add(flight);
            }
            return Listflight;
        }

        public async Task<List<Flights>> GetByDateRangeAsync(DateTime initialdate, DateTime finaldate)
        {
            var flightsDateList = await _flightsServices.Find(flight => true).ToListAsync();
            List<Flights> Listflight = new();
            foreach (var flight in flightsDateList)
            {
                var compare1 = DateTime.Compare(initialdate, flight.Departure);
                var compare2 = DateTime.Compare(finaldate, flight.Departure);

                if (compare1 <= 0 && compare2 >= 0 && flight.Status == true)
                    Listflight.Add(flight);
            }
            return Listflight;
        }

        public async Task UpdateCancelFlightAsync(string id, Flights flightIn) => await _flightsServices.ReplaceOneAsync(flight => flight.Id == id, flightIn);

        public async Task UpdateAsync(Flights flightIn) => await _flightsServices.ReplaceOneAsync(flight => flight.Id == flightIn.Id, flightIn);


        public async Task<Airports> GetAirportAPIAsync(string iata)
        {
            var httpclient = new HttpClient();
            var airportresponse = await httpclient.GetAsync(FlightUtils.GetAPIUri("ApiGetAirportUri") + iata);
            var JsonString = await airportresponse.Content.ReadAsStringAsync();
            var airport = JsonConvert.DeserializeObject<Airports>(JsonString);
            return airport;
        }

        public async Task<Aircrafts> GetAircraftAPIAsync(string rab)
        {
            var httpclient = new HttpClient();
            var airportresponse = await httpclient.GetAsync(FlightUtils.GetAPIUri("ApiGetAircraftUri") + rab);
            var JsonString = await airportresponse.Content.ReadAsStringAsync();

            JavaScriptSerializer ser = new JavaScriptSerializer();

            var aircraft = ser.Deserialize<Aircrafts>(JsonString);

            return aircraft;
        }

        public async Task<IActionResult> PutDateAircraftAPIAsync(string rab)
        {
            var httpclient = new HttpClient();
            var airportresponse = await httpclient.GetAsync(FlightUtils.GetAPIUri("ApiPutAircrafUri") + rab);
            var JsonString = await airportresponse.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(JsonString);
            return result;
        }
    }
}
