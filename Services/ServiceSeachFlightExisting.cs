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
    public class ServiceSeachFlightExisting
    {
        static readonly HttpClient client = new HttpClient();

        public static async Task<Flight> SeachFlightApi(string id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44333/api/Flight/" + id);
                response.EnsureSuccessStatusCode();
                string reponseBody = await response.Content.ReadAsStringAsync();
                var flightJson = JsonConvert.DeserializeObject<Flight>(reponseBody);
                return flightJson;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
