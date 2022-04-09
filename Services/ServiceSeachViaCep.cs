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
    public class ServiceSeachViaCep
    {
        static readonly HttpClient client = new HttpClient();

        public static async Task<Address> ServiceSeachCepInApiViaCep(string cep)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://viacep.com.br/ws/" + cep + "/json/");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var addressJson = JsonConvert.DeserializeObject<Address>(responseBody);
                return addressJson;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
