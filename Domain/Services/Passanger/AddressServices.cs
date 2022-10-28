using Newtonsoft.Json;
using Passenger.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Passenger.Services {
    public class AddressServices {

        private Address _address;

        public AddressServices() {

        }

        public async Task<Address> MainAsync(string zipCode) {

            using (HttpClient client = new HttpClient()) {
                HttpResponseMessage response = await client.GetAsync("https://viacep.com.br/ws/" + zipCode + "/json/");

                var adressJson = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    return _address = JsonConvert.DeserializeObject<Address>(adressJson);

                else
                    return null;
            }


        }

    }
}
