using System;
using RabbitMQ.Client;

namespace ProjRabbitMQLogs
{
    internal class Program
    {
        private const string QUEUE_NAME = "messagelogs";
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                }
            }
        }
    }
}
