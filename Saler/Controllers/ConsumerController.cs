using DomainAPI.Models.Flight;
using DomainAPI.Models.Passenger;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Net;
using Nancy.Json;

namespace Saler.Controllers
{
    public class ConsumerController
    {
        private readonly string _consumerGetFligth = "https://localhost:44330/api/Flights/";
        private readonly string _consumerGetPassenger = "https://localhost:44388/api/Passenger/StatusValids/Cpf?cpf=";
        public async Task<Flights> GetFlightAsync(DateTime date, string rab)
        {
            using (HttpClient _adressClient = new())
            {
                HttpResponseMessage response = await _adressClient.GetAsync(_consumerGetFligth + date + "/" + rab);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Flights>(json);
            }
        }

        public async Task<Flights> PostFlightAsync(Flights flight)
        {
            using (HttpClient _adressClient = new())
            {
                HttpResponseMessage response = await _adressClient.GetAsync(_consumerGetFligth);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Flights>(json);
            }
        }

        public async Task<Passengers> GetPassengerAsync(string cpf)
        {
            using (HttpClient _adressClient = new())
            {
                HttpResponseMessage response = await _adressClient.GetAsync(_consumerGetPassenger + cpf);
                //response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return new JavaScriptSerializer().Deserialize<Passengers>(json);
            }
        }
    }
}
