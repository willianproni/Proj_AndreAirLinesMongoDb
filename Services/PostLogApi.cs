using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Services
{
    public class PostLogApi
    {
        public static void PostLogInApi(Log newLog)
        {
            HttpClient client = new HttpClient();
            client.PostAsJsonAsync("https://localhost:44366/api/Log", newLog);
        }
    }
}
