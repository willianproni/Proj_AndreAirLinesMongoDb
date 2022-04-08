using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    public class Ticket
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public Flight Flight { get; set; }
        public Passenger Passenger { get; set; }
        public BasePrice BasePrice { get; set; }
        public Classes Classes { get; set; }
        public DateTime DateRegister { get; set; }
        public decimal Amount { get; set; }
        public double Promotion { get; set; }
        public string LoginUser { get; set; }
    }
}
