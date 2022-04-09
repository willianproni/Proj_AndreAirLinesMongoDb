using System.Collections.Generic;
using Model;
using MongoDB.Driver;
using PassengerMicroService.Util;


namespace PassengerMicroService.Services
{
    public class PassengerService
    {
        private readonly IMongoCollection<Passenger> _passenger;

        public PassengerService(IPassengerDatabase settings)
        {
            var passenger = new MongoClient(settings.ConnectionString);
            var database = passenger.GetDatabase(settings.DatabaseName);
            _passenger = database.GetCollection<Passenger>(settings.PassengerCollectionName);
        }

        public List<Passenger> Get() =>
            _passenger.Find(passenger => true).ToList();

        public Passenger Get(string id) =>
            _passenger.Find<Passenger>(passenger => passenger.Id == id).FirstOrDefault();

        #region Checkers

        public bool VerifyPassengerExist(string cpfPassenger) =>
            _passenger.Find<Passenger>(passenger => passenger.Cpf == cpfPassenger).Any();

        public Passenger VerifyCpfPassenger(string cpf) =>
            _passenger.Find<Passenger>(passenger => passenger.Cpf == cpf).FirstOrDefault();
        #endregion

        public Passenger Create(Passenger newPassenger)
        {
            _passenger.InsertOne(newPassenger);
            return newPassenger;
        }

        public void Update(string cpf, Passenger updatePassenger) =>        
            _passenger.ReplaceOne(passenger => passenger.Cpf == cpf, updatePassenger);
        

        public void Remove(string cpf) =>
            _passenger.DeleteOne(passenger => passenger.Cpf == cpf);
        
    }
}
