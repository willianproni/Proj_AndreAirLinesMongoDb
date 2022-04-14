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
        static readonly HttpClient client = new HttpClient();

        public static async Task Add(Log log)
        {
            try
            {

                if (client.BaseAddress == null)
                    await client.PostAsJsonAsync("https://localhost:44396/api/ProducerLog", log);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
