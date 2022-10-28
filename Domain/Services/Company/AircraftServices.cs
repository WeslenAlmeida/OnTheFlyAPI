using Company.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Company.Services
{
    public class AircraftServices
    {
        public AircraftServices()
        {
        }

        //public async Task<Aircrafts> GetAddress(string cnpj)
        //{
        //    Address address;
        //    using (HttpClient _adressClient = new HttpClient())
        //    {
        //        HttpResponseMessage response = await _adressClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
        //        var adressJson = await response.Content.ReadAsStringAsync();
        //        if (response.IsSuccessStatusCode) return address = JsonConvert.DeserializeObject<Address>(adressJson);
        //        else return null;
        //    }
        //}
    }
}
