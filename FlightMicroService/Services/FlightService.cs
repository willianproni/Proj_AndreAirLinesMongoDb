using System.Collections.Generic;
using FlightMicroService.Util;
using Model;
using MongoDB.Driver;

namespace FlightMicroService.Services
{
    public class FlightService
    {
        private readonly IMongoCollection<Flight> _flight;

        public FlightService(IFlightDatabase settings)
        {
            var flight = new MongoClient(settings.ConnectionString);
            var database = flight.GetDatabase(settings.DatabaseName);
            _flight = database.GetCollection<Flight>(settings.FlightCollectionName);
        }

        public List<Flight> Get() =>
            _flight.Find(flight => true).ToList();

        public Flight Get(string id) =>
            _flight.Find<Flight>(flight => flight.Id == id).FirstOrDefault();

        public Flight Create(Flight newFlight)
        {
            _flight.InsertOne(newFlight);
            return newFlight;
        }

        public void Update(string id,Flight upFlight) =>
            _flight.ReplaceOne(flight => flight.Id == id, upFlight);


        public void Remove(string id) =>
            _flight.DeleteOne(flight => flight.Id == id);

    }
}
