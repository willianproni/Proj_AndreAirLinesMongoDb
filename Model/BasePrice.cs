using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    public class BasePrice
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public Airport Origin { get; set; }
        public Airport Destiny { get; set; }
        public decimal Value { get; set; }
        public DateTime InclusionDate { get; set; }
        public string LoginUser { get; set; }
    }
}
