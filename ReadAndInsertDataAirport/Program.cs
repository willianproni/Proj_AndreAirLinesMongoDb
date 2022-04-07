using System;
using ReadAndInsertDataAirport.Services;
using System.IO;
using Model.DataModel;

namespace ReadAndInsertDataAirport
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AirportDataService airportService = new AirportDataService();

            string fileAiportSvg = @"C:\5by5\WillFly\WillFlyData\Arquivocvg\dados.csv";

            if (File.Exists(fileAiportSvg))
            {
                using (StreamReader read = new StreamReader(fileAiportSvg))
                {
                    string line = read.ReadLine();
                    while (line != null && line != "")
                    {
                        string[] values = line.Split(";");

                        AirportData airport = new AirportData()
                        {
                            City = values[0],
                            Country = values[1],
                            Code = values[2],
                            Continent = values[3]
                        };
                        airportService.Add(airport);

                        line = read.ReadLine();
                    }
                }
            }

            foreach (var airport in airportService.GetAll())
            {
                Console.WriteLine(airport);
            }
        }
    }
}
