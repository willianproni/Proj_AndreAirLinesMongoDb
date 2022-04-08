﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    public abstract class Person
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public Log LoginUser { get; set; }
    }
}
