using System.Collections.Generic;
using FunctionMicroService.Util;
using Model;
using MongoDB.Driver;

namespace FunctionMicroService.Services
{
    public class FunctionService
    {
        private readonly IMongoCollection<Function> _fuction;

        public FunctionService(IFunctionDatabase service)
        {
            var function = new MongoClient(service.ConnectionString);
            var database = function.GetDatabase(service.DatabaseName);
            _fuction = database.GetCollection<Function>(service.FunctionCollectionName);
        }

        public List<Function> Get() =>
            _fuction.Find(function => true).ToList();

        public Function Get(string id) =>
            _fuction.Find<Function>(function => function.Id == id).FirstOrDefault();

        public Function Create(Function newFunction)
        {
            _fuction.InsertOne(newFunction);
            return newFunction;
        }

        public void Update(string id, Function upFunction) =>
            _fuction.ReplaceOne(function => function.Id == id, upFunction);

        public void Delete(string id) =>
            _fuction.DeleteOne(function => function.Id == id);
    }
}
