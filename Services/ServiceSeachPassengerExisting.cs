using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace Services
{
    public class ServiceSeachPassengerExisting
    {
        static readonly HttpClient client = new HttpClient();

        public static async Task<Passenger> SeachPassengerInApi(string cpf)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44384/api/Passenger/" + cpf);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var passengerJson = JsonConvert.DeserializeObject<Passenger>(responseBody);
                return passengerJson;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
