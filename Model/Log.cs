using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    public class Log
    {
        [bsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public User User { get; set; }
        public string EntityBefore { get; set; }
        public string EntityAfter { get; set; } 
        public string Operation { get; set; }
        public DateTime DataOperation { get; set; }
    }
}
