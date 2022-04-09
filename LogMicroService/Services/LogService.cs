using System.Collections.Generic;
using LogMicroService.Util;
using Model;
using MongoDB.Driver;

namespace LogMicroService.Services
{
    public class LogService
    {
        private readonly IMongoCollection<Log> _log;

        public LogService(ILogDatabase settings)
        {
            var log = new MongoClient(settings.ConnectionString);
            var database = log.GetDatabase(settings.DatabaseName);
            _log = database.GetCollection<Log>(settings.LogCollectionName);
        }

        public List<Log> Get() =>
            _log.Find(log => true).ToList();

        public Log Get(string id) =>
            _log.Find<Log>(log => log.Id == id).FirstOrDefault();

        public Log Create(Log newLog)
        {
            _log.InsertOne(newLog);
            return newLog;
        }

        public void Update(string id, Log uptadeLog) =>
            _log.ReplaceOne(log => log.Id == id, uptadeLog);

        public void Remove(string id) =>
            _log.DeleteOne(log => log.Id == id);
    }
}
