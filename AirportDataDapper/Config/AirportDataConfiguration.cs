using Microsoft.Extensions.Configuration;
using System.IO;

namespace AirportDataDapper.Config
{
    public class AirportDataConfiguration
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static string Get()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
            string _conection = Configuration["ConnectionStrings:DefaultConnection"];
            return _conection;
        }
    }
}
