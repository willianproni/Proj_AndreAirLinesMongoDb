using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Model
{
    public class AddressDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        [JsonProperty("cep")]
        public string Cep { get; set; }
        [JsonProperty("localidade")]
        public string City { get; set; }
        [JsonProperty("uf")]
        public string State { get; set; }
        [JsonProperty("bairro")]
        public string District { get; set; }
        [JsonProperty("logradouro")]
        public string Street { get; set; }
        [JsonProperty("number")]
        public int Number { get; set; }
        [JsonProperty("complemento")]
        public string Complement { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("continent")]
        public string Continent { get; set; }
    }
}
