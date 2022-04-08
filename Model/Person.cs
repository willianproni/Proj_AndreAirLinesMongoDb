using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public abstract class Person
    {
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public string LoginUser { get; set; }
    }
}
