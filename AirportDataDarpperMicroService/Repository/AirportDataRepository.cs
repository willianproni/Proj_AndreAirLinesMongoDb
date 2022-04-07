using System.Collections.Generic;
using System.Data.SqlClient;
using AirportDataDarpperMicroService.Config;
using Dapper;
using Model.DataModel;

namespace AirportDataDarpperMicroService.Repository
{
    public class AirportDataRepository : IAiportDataRepository
    {
        private string _conection;

        public AirportDataRepository()
        {
            _conection = DataBaseConfiguration.Get();
        }
        public bool Add(AirportData airport)
        {
            bool status = false;

            using(var dataBase = new SqlConnection(_conection))
            {
                dataBase.Open();
                dataBase.Execute(AirportData.INSERT, airport);
                status = true;
            }
            return status;
        }

        public AirportData Get(string id)
        {
            using(var dataBase = new SqlConnection(_conection))
            {
                dataBase.Open();
                var airport = dataBase.QueryFirstOrDefault<AirportData>(AirportData.GETID + id);
                return (AirportData)airport;
            }
        }

        public List<AirportData> GetAll()
        {
            using(var dataBase = new SqlConnection(_conection))
            {
                dataBase.Open();
                var airport = dataBase.Query<AirportData>(AirportData.GETALL);
                return (List<AirportData>)airport;
            }
        }

        public void Update(AirportData id)
        {
            using(var dataBase =  new SqlConnection(_conection))
            {
                dataBase.Open();
                dataBase.Execute(AirportData.UPDATE);
            }
        }

        public void Remove(string id)
        {
            using (var dataBase = new SqlConnection(_conection))
            {
                dataBase.Open();
                dataBase.Execute(AirportData.DELETE);
            }
        }
    }
}
