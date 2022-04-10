using System.Collections.Generic;
using Model;
using MongoDB.Driver;
using UserMicroService.Util;

namespace UserMicroServices.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _user;

        public UserService(IUserDatabase settings)
        {
            var user = new MongoClient(settings.ConnectionString);
            var database = user.GetDatabase(settings.DatabaseName);
            _user = database.GetCollection<User>(settings.UserCollectionName);
        }

        public List<User> Get() =>
            _user.Find(user => true).ToList();

        public User Get(string cpf) =>
            _user.Find<User>(user => user.Cpf == cpf).FirstOrDefault();

        public User GetLogin(string loginUser) =>
            _user.Find<User>(login => login.Login == loginUser).FirstOrDefault();

        public User Create(User newUser)
        {
            _user.InsertOne(newUser);
            return newUser;
        }

        public void Update(string cpf, User upUser) =>
            _user.ReplaceOne(user => user.Cpf == cpf, upUser);


        public void Remove(string cpf) =>
            _user.DeleteOne(user => user.Cpf == cpf);

    }
}
