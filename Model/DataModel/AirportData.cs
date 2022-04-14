using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataModel
{
    [Table("Airport")]
    public class AirportData
    {
        #region Constant

        public readonly static string INSERT = "INSERT INTO Airport (City, Country, Code, Continent) VALUES (@City, @Country, @Code, @Continent)";
        public readonly static string GETALL = "SELECT Id, City, Country, Code, Continent FROM Airport";
        public readonly static string GETIATA = "SELECT Id, City, Country, Code, Continent FROM Airport WHERE Code = @code";
        public readonly static string GETID = "SELECT Id, City, Country, Code, Continent FROM Airport WHERE Id = @id";
        public readonly static string DELETE = "DELETE FROM Airport WHERE ID = @id";
        public readonly static string UPDATE = "UPDATE Airport SET City = @City, Country = @Country, Code = @Code, Continent = @Continent WHERE ID = @id";

        #endregion

        #region Properties
        [Key]
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Code { get; set; }
        public string Continent { get; set; }

        #endregion

        #region Method

        public override string ToString()
        {
            return $"Id: {Id}\n" +
                   $"City: {City}\n" +
                   $"Country: {Country}\n" +
                   $"Code: {Code}\n" +
                   $"Continent: {Continent}";
        }

        #endregion
    }
}
