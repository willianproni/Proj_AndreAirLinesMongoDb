using System.Collections.Generic;
using AirportDataDarpperMicroService.Repository;
using Model.DataModel;

namespace AirportDataDarpperMicroService.Services
{
    public class AirportDataService
    {
        private AirportDataRepository _airportDataRepository;

        public AirportDataService()
        {
            _airportDataRepository = new AirportDataRepository();
        }

        public bool Add(AirportData airport)
        {
            return _airportDataRepository.Add(airport);
        }

        public List<AirportData> Get()
        {
            return _airportDataRepository.GetAll();
        }

        public AirportData Get(string id)
        {
            return _airportDataRepository.Get(id);
        }

        public void Update(AirportData airport)
        {
            _airportDataRepository.Update(airport);
        }

        public void Remove(string id)
        {
           _airportDataRepository.Remove(id);
        }


    }
}
