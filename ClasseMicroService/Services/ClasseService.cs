using System.Collections.Generic;
using Model;
using MongoDB.Driver;

namespace ClasseMicroService.Services
{
    public class ClasseService
    {
        private readonly IMongoCollection<Classes> _classe;

        public ClasseService(IClasseDatabase settings)
        {
            var classe = new MongoClient(settings.ConnectionString);
            var datebase = classe.GetDatabase(settings.DatabaseName);
            _classe = datebase.GetCollection<Classes>(settings.ClasseCollectionName);
        }

        public List<Classes> Get() =>
            _classe.Find(classe => true).ToList();

        public Classes Get(string id) =>
            _classe.Find(classe => classe.Id == id).FirstOrDefault();

        public Classes Create(Classes newClasse)
        {
            _classe.InsertOne(newClasse);
            return newClasse;
        }

        public void Update(string id, Classes upClasse) =>
            _classe.ReplaceOne(classe => classe.Id == id, upClasse);

        public void Remove(string id) =>
            _classe.DeleteOne(classe => classe.Id == id);
    }
}
