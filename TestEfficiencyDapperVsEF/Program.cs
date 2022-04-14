using System;
using System.Threading;
using Services;

namespace TestEfficiencyDapperVsEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
                Console.Clear();
                Console.WriteLine("\nTest efficiency");
                Console.WriteLine("\nDapper Vs EF");

                Thread.Sleep(2000);

                Console.WriteLine("\nFirt Test --> Dapper");

                Console.WriteLine("\nStart...");

                Thread.Sleep(3000);

                var beginDapper = DateTime.Now;

                Console.WriteLine($"\n Start --> {beginDapper.ToString()}");

                for (int i = 1; i < 101; i++)
                {
                    Console.WriteLine($"consultation {i} -> {DateTime.Now}");
                    ServiceSeachApiExisting.SeachAirportDataSqlIdApiForId(i).Wait();
                }

                var endDapper = DateTime.Now;
                var diferenceDapper = endDapper - beginDapper;
                Console.WriteLine($"Finish --> {endDapper.ToString()}");
                Console.WriteLine($"Different--> {diferenceDapper.ToString()}");
                Console.WriteLine("\n\n\tPress Enter to Next");

                Console.ReadKey();

                Console.Clear();

                Console.Clear();
                Console.WriteLine("\nTest efficiency");
                Console.WriteLine("\nDapper Vs EF");

                Thread.Sleep(2000);

                Console.WriteLine("\nSecond Test --> EF");

                Console.WriteLine("\nStart...");

                Thread.Sleep(3000);

                var beginEF = DateTime.Now;

                Console.WriteLine($"\nStart --> {beginEF.ToString()}");

                for (int i = 1; i < 101; i++)
                {
                    Console.WriteLine($"Consult {i} --> {DateTime.Now}");
                    ServiceSeachApiExisting.SeachAirportDataSqlEntityFrameworkIdApiForId(i).Wait();
                }

                var endEF = DateTime.Now;
                var differenceEF = endEF - beginEF;
                Console.WriteLine("\nFinish  -> " + endEF.ToString());
                Console.WriteLine(" Different -> " + differenceEF);
                Console.WriteLine("\n Press Enter to Result...");
                Console.ReadKey();

                Console.Clear();

                Console.WriteLine("\nTest efficiency");
                Console.WriteLine("\nDapper Vs EF");

                Thread.Sleep(2000);

                Console.WriteLine($"\n Dapper -> {diferenceDapper}");
                Console.WriteLine($"\n EF -> {differenceEF}");

                if (diferenceDapper > differenceEF)
                {
                    Console.WriteLine($"\nFaster EF for --> {diferenceDapper - differenceEF}");
                }
                else
                {
                    Console.WriteLine($"\nFaster Dapper for --> {differenceEF - diferenceDapper}");

                }

                Console.WriteLine("Finish Test");
          

        }
    }
}
