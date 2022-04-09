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

        public static async Task<AddressDTO> ServiceSeachCepInApiViaCep(string cep)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://viacep.com.br/ws/" + cep + "/json/");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var addressJson = JsonConvert.DeserializeObject<AddressDTO>(responseBody);
                return addressJson;
            }
            catch (HttpRequestException)
            {
                new HttpRequestException("Serviço do Viacep Idisponivel");
                return null;
            }
        }
    }
}
