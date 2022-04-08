using System.Collections.Generic;
using AccessMicroService.Util;
using Model;
using MongoDB.Driver;

namespace AccessMicroService.Services
{
    public class AccessService
    {
        private readonly IMongoCollection<Access> _access;

        public AccessService(IAccessDatabase setting)
        {
            var access = new MongoClient(setting.ConnectionString);
            var database = access.GetDatabase(setting.DatabaseName);
            _access = database.GetCollection<Access>(setting.AccessCollectionName);
        }

        public List<Access> Get() =>
            _access.Find(access => true).ToList();

        public Access Get(string id) =>
            _access.Find<Access>(access => access.Id == id).FirstOrDefault();

        public Access Create(Access newAccess)
        {
            _access.InsertOne(newAccess);
            return newAccess;
        }

        public void Update(string id, Access upAccess) =>
            _access.ReplaceOne(access => access.Id == id, upAccess);

        public void Delete(string id) =>
            _access.DeleteOne(access => access.Id == id);
    }
}
