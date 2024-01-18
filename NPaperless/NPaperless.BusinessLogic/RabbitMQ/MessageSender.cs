using log4net;
using log4net.Core;
using Microsoft.Extensions.Configuration;
using NPaperless.BusinessLogic.Interfaces;
using NPaperless.BusinessLogic.Services;
using RabbitMQ.Client;
using System.Text;

namespace NPaperless.BusinessLogic.RabbitMQ
{
    public class MessageSender : IMessageSender
    {
        private readonly IConfiguration _configuration;
        public MessageSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendMessage(string message)
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

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "npaperless-queue",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
