using System.Collections.Generic;
using BasePriceMicroService.Util;
using Model;
using MongoDB.Driver;

namespace BasePriceMicroService.Services
{
    public class BasepriceService
    {
        private readonly IMongoCollection<BasePrice> _baseprice;

        public BasepriceService(IBasepriceDatabase settings)
        {
            var baseprice = new MongoClient(settings.ConnectionString);
            var database = baseprice.GetDatabase(settings.DatabaseName);
            _baseprice = database.GetCollection<BasePrice>(settings.BasepriceCollectionName);
        }

        public List<BasePrice> Get() =>
            _baseprice.Find(baseprice => true).ToList();

        public BasePrice Get(string id) =>
            _baseprice.Find<BasePrice>(baseprice => baseprice.Id == id).FirstOrDefault();

        public BasePrice Create(BasePrice newBaseprice)
        {
            _baseprice.InsertOne(newBaseprice);
            return newBaseprice;
        }

        public void Update(string id, BasePrice upBaseprice) =>
            _baseprice.ReplaceOne(baseprice => baseprice.Id == id, upBaseprice);


        public void Remove(string id) =>
            _baseprice.DeleteOne(baseprice => baseprice.Id == id);

    }
}
