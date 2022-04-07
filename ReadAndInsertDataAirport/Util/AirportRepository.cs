using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model.DataModel;
using ReadAndInsertDataAirport.Config;

namespace ReadAndInsertDataAirport.Util
{
    public class AirportRepository : IAirportRepository
    {
        private string _connection;

        public AirportRepository()
        {
            _connection = DataBaseConfiguration.Get();
        }
        public bool Add(AirportData airport)
        {
            bool status = false;

            using (var dataBase = new SqlConnection(_connection))
            {
                dataBase.Open();
                dataBase.Execute(AirportData.INSERT, airport);
                status = true;
            }
            return status;
        }

        public List<AirportData> GetAll()
        {
            using (var dataBase = new SqlConnection(_connection))
            {
                dataBase.Open();
                var airport = dataBase.Query<AirportData>(AirportData.GETALL);
                return (List<AirportData>)airport;
            }
        }
    }
}
