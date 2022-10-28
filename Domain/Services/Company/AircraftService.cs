using Aircraft.Models;
using Company.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Company.Services
{
    public class AircraftService
    {
        public AircraftService()
        {
        }

        public async Task<Aircrafts> GetAddress(string cnpj)
        {
            Aircrafts aircraft;
            using (HttpClient _aircraftClient = new HttpClient())
            {
                HttpResponseMessage response = await _aircraftClient.GetAsync($"https://viacep.com.br/ws/{cnpj}/json/");
                var adressJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return aircraft = JsonConvert.DeserializeObject<Aircrafts>(adressJson);
                else return null;
            }
        }
    }
}
