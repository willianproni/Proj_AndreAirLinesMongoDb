using System.Collections.Generic;
using AirportDataDapper.Reposity;
using Model.DataModel;

namespace AirportDataDapper.Services
{
    public class AirportDateServices //Classe responsável pelos serviços
    {
  
        private IAirpotyDateReposity _airpotyDateReposity; //Criando um campo com referencia a Interface IAirpotyDateReposity

        public AirportDateServices(IAirpotyDateReposity airpotyDateReposity)
        {
            _airpotyDateReposity = airpotyDateReposity;
        }

        public bool Add(AirportData newAirport) =>
            _airpotyDateReposity.Add(newAirport);
        
        public List<AirportData> GetAll() =>
            _airpotyDateReposity.GeAll();

        public AirportData GetId(int id) =>
            _airpotyDateReposity.GetId(id);

        public AirportData Get(string code) =>
            _airpotyDateReposity.Get(code);

        public void Update(AirportData airport) =>
            _airpotyDateReposity.Update(airport);

        public void Remove(string id) =>
            _airpotyDateReposity.Delete(id);
    }
}
