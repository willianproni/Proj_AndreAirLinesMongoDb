using System.Collections.Generic;
using AirportMicroServices.Util;
using Model;
using MongoDB.Driver;

namespace AirportMicroServices.Service
{
    public class AirportService
    {
        private readonly IMongoCollection<Airport> _airport;

        public AirportService(IAirportDatabase settings)
        {
            var airport = new MongoClient(settings.ConnectionString);
            var database = airport.GetDatabase(settings.DatabaseName);
            _airport = database.GetCollection<Airport>(settings.AirportCollectionName);
        }

        public List<Airport> Get() =>
            _airport.Find(airport => true).ToList();

        public Airport Get(string id) =>
            _airport.Find<Airport>(aircraft => aircraft.Id == id).FirstOrDefault();

        public Airport GetAirport(string iata) =>
            _airport.Find<Airport>(airport => airport.CodeIATA == iata).FirstOrDefault();

        public bool VerifyCodeIata(string CodIata) =>
            _airport.Find<Airport>(airport => airport.CodeIATA == CodIata).Any();

        public Airport Create(Airport newAirport)
        {
            _airport.InsertOne(newAirport);
            return newAirport;
        }

        public void Uptade(string iata, Airport updateAirport)
        {
            _airport.ReplaceOne(airport => airport.CodeIATA == iata, updateAirport);
        }

        public void Remove(string iata) =>
            _airport.DeleteOne(airport => airport.CodeIATA == iata);
    }
}
