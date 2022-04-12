using System.Collections.Generic;
using System.Threading.Tasks;
using AircraftMicroService.Util;
using LogMicroService.Util;
using Model;
using MongoDB.Driver;
using Services;

namespace AircraftMicroService.Services
{
    public class AircraftService
    {
        private readonly IMongoCollection<Aircraft> _aircraft;

        public AircraftService(IAircraftDatabase settings)
        {
            var aircraft = new MongoClient(settings.ConnectionString);
            var database = aircraft.GetDatabase(settings.DatabaseName);
            _aircraft = database.GetCollection<Aircraft>(settings.AircraftCollectionName);
        }

        public List<Aircraft> Get() =>
            _aircraft.Find(aircraft => true).ToList();

        public Aircraft Get(string id) =>
            _aircraft.Find<Aircraft>(aircraft => aircraft.Id == id).FirstOrDefault();

        public Aircraft GetNameAircraft(string name) =>
            _aircraft.Find<Aircraft>(aircraft => aircraft.Name == name).FirstOrDefault();

        public bool VerifyAircraftExist(string nameAircraft) =>
            _aircraft.Find<Aircraft>(aircraft => aircraft.Name == nameAircraft).Any();

        public Aircraft Create(Aircraft newAircraft)
        {
            _aircraft.InsertOne(newAircraft);
            return newAircraft;
        }
        public void Update(string nameAircraft, Aircraft upAircraft) =>
            _aircraft.ReplaceOne(aircraft => aircraft.Name == nameAircraft, upAircraft);

        public void Remove(string nameArcraft) =>
            _aircraft.DeleteOne(aircraft => aircraft.Name == nameArcraft);
    }
}
