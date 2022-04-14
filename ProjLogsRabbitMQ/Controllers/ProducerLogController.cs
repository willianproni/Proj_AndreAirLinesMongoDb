using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace ProjLogsRabbitMQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerLogController : ControllerBase
    {
        private readonly ConnectionFactory _factory;
        private const string QUEUE_NAME = "messagelogs";

        public ProducerLogController()
        {
            _factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
        }

        [HttpPost]
        public IActionResult PostMessage(Log newLog)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                            queue: QUEUE_NAME,
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                     );

                    var stringfieldMessage = JsonConvert.SerializeObject(newLog);
                    var bytesMessage = Encoding.UTF8.GetBytes(stringfieldMessage);

                    channel.BasicPublish(
                            exchange: "",
                            routingKey: QUEUE_NAME,
                            basicProperties: null,
                            body: bytesMessage
                    );
                }
            }
            return Accepted();
        }
    }
}
