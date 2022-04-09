using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.DataModel;
using Newtonsoft.Json;

namespace Services
{
    public class ServiceSeachApiExisting
    {
        static readonly HttpClient client = new HttpClient();

        public static async Task<Aircraft> SeachAircraftNameInApi(string nameAircraft)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44366/api/Aircraft/" + nameAircraft);
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

        public static async Task<Airport> SeachAiportInApi(string iata)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44327/api/Airport/" + iata);
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

        public static async Task<AirportData> SeachAirportDataSqlIdApi(string iata)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44376/api/Dapper/JNB?iata=" + iata);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var airportData = JsonConvert.DeserializeObject<AirportData>(responseBody);
                return airportData;
            }
            catch (Exception)
            {
                throw;
            }
        }

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

        public static async Task<List<Access>> SeachAccessIdInApi(string id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44373/api/Access/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var accessJson = JsonConvert.DeserializeObject<List<Access>>(responseBody);
                return accessJson;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Function> SeachFunctionIdInApi(string id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:44372/api/Function/" + id);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var functionJson = JsonConvert.DeserializeObject<Function>(responseBody);
                return functionJson;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
