﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User : Person
    {
        public string Password { get; set; }
        public string Login { get; set; }
        public string Sector { get; set; }
        public Function Function { get; set; }
    }
}
