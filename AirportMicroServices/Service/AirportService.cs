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

        public Airport VerifyCodeIATA(string codIata) =>
            _airport.Find<Airport>(airport => airport.CodeIATA == codIata).FirstOrDefault();

        public Airport Create(Airport newAirport)
        {
            _airport.InsertOne(newAirport);
            return newAirport;
        }

        public void Uptade(string id, Airport upAirport)
        {
            _airport.ReplaceOne(airport => airport.Id == id, upAirport);
        }

        public void Remove(string id) =>
            _airport.DeleteOne(airport => airport.Id == id);
    }
}
