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
    public class ServiceSeachAirportExisting
    {
        static readonly HttpClient client = new HttpClient();

        public static async Task<Airport> SeachAiportInApi(string iata)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44327/api/Airport/iata/" + iata);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var airportJson = JsonConvert.DeserializeObject<Airport>(responseBody);
                return airportJson;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
