using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataModel;
using ReadAndInsertDataAirport.Util;

namespace ReadAndInsertDataAirport.Services
{
    public class AirportDataService
    {
        private IAirportRepository _airportRepository;

        public AirportDataService()
        {
            _airportRepository = new AirportRepository();
        }

        public bool Add(AirportData airport)
        {
            return _airportRepository.Add(airport);
        }

        public List<AirportData> GetAll()
        {
            return _airportRepository.GetAll();
        }
    }
}
