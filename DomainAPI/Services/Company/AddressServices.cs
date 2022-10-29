using DomainAPI.Models.Company;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DomainAPI.Services.Company
{
    public class AddressServices
    {
        public AddressServices()
        {
        }

        public async Task<CompanyAddress> GetAddress(string cep)
        {
            CompanyAddress address;
            using (HttpClient _adressClient = new HttpClient())
            {
                HttpResponseMessage response = await _adressClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
                var adressJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode) return address = JsonConvert.DeserializeObject<CompanyAddress>(adressJson);
                else return null;
            }
        }
    }
}
