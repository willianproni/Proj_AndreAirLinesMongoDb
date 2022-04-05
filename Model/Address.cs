﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    public class Address
    {
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string Cep { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Complement { get; set; }
        public string Country { get; set; }
    }
}
