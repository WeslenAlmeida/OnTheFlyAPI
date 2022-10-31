using DomainAPI.Models.Flight;
using DomainAPI.Models.Passenger;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Net;
using Nancy.Json;
using Microsoft.AspNetCore.Mvc;

namespace Saler.Controllers
{
    public class ConsumerController
    {
        private readonly string _consumerGetFligth = "https://localhost:44330/api/Flights/GetOneFlight/";
        private readonly string _consumerPutFligth = "https://localhost:44330/api/Flights/cancelflight/";
        private readonly string _consumerGetPassenger = "https://localhost:44388/api/Passenger/StatusValids/Cpf?cpf=";
        public async Task<Flights> GetFlightAsync(DateTime date, string rab) {
            using (HttpClient _adressClient = new()) {
                HttpResponseMessage response = await _adressClient.GetAsync(_consumerGetFligth + date.ToString("yyyy-MM-dd  HH:mm:ss") + "/" + rab);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Flights>(json);
            }
        }

        public async Task<Flights> PutFlightAsync(Flights flight) {
            using (HttpClient _adressClient = new()) {
                string fligthPost = JsonConvert.SerializeObject(flight);
                HttpResponseMessage response = await _adressClient.GetAsync(_consumerPutFligth + fligthPost);
                response.EnsureSuccessStatusCode();
                return flight;
            }
        }

        public async Task<Passengers> GetPassengerAsync(string cpf)
        {
            using (HttpClient _adressClient = new())
            {
                HttpResponseMessage response = await _adressClient.GetAsync(_consumerGetPassenger + cpf);
                var json = await response.Content.ReadAsStringAsync();
                return new JavaScriptSerializer().Deserialize<Passengers>(json);
            }
        }
    }
}
