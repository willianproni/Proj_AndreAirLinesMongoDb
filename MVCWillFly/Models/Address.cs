using Microsoft.EntityFrameworkCore;

namespace MVCWillFly.Models
{
    [Keyless]
    public class Address
    {
        public string Id { get; set; }
        public string Cep { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Complement { get; set; }
        public string Country { get; set; }
        public string Continent { get; set; }
    }
}
