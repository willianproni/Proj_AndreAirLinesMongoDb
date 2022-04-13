using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ProjRabbitMQLogs.Service
{
    public class SenderMongoDBservice
    {
        private readonly HttpClient client = new HttpClient();

        public static async Task Add(Log log)
        {
            try
            {
                HttpClient client = new HttpClient();

                if (client.BaseAddress == null)
                    await client.PostAsJsonAsync("https://localhost:44366/api/Log", log);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
