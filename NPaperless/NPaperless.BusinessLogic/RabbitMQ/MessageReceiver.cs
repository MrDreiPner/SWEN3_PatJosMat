using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace NPaperless.BusinessLogic.RabbitMQ
{
    public class MessageReceiver
    {
        private readonly IConfiguration _configuration;

        public MessageReceiver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Receive()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQ:HostName"],
                Port = int.Parse(_configuration["RabbitMQ:Port"]),
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"]
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "npaperless-queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                };

                channel.BasicConsume(queue: "npaperless-queue",
                                     autoAck: true,
                                     consumer: consumer);
            }
        }
    }
}
