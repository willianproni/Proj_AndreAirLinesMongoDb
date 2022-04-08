﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Model
{
    public class Access
    {
        [BsonId]
        public string Id { get; set; }
        public string Description { get; set; }
    }
}
