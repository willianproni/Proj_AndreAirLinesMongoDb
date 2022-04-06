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
    public class ServiceSeachApiExisting
    {
        static readonly HttpClient client = new HttpClient();
        
        public static async Task<Classes> SeachClasseIdInApi(string id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44346/api/Classe/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var classeJson = JsonConvert.DeserializeObject<Classes>(responseBody);
                return classeJson;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<BasePrice> SeachBasepriceIdInApi(string id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44374/api/Baseprice/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var basepriceJson = JsonConvert.DeserializeObject<BasePrice>(responseBody);
                return basepriceJson;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
