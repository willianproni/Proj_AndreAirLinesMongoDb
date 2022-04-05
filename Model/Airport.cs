﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    public class Airport
    {
        [BsonId]
        public string Id { get; set; }
        public string Acronym { get; set; }
        public int Name { get; set; }
        public Address Address { get; set; }
    }
}
