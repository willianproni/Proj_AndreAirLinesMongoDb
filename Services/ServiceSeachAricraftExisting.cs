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
    public class ServiceSeachAricraftExisting
    {
        static readonly HttpClient client = new HttpClient();

        public static async Task<Aircraft> SeachAircraftNameInApi(string nameAircraft)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44366/api/Aircraft/name/" + nameAircraft);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var aircraftJson = JsonConvert.DeserializeObject<Aircraft>(responseBody);
                return aircraftJson;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<bool> CheckAircraftService()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44366/api/StatusAircraft");
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
