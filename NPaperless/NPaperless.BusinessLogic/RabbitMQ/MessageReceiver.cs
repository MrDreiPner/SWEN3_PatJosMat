using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using NPaperless.BusinessLogic.Interfaces;
using log4net;
using NPaperless.BusinessLogic.Services;

namespace NPaperless.BusinessLogic.RabbitMQ
{
    public class MessageReceiver : IMessageReceiver
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(MessageReceiver));
        private readonly IConfiguration _configuration;

        public MessageReceiver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public event EventHandler<IQueueReceivedEvent> OnReceived;

        public void Receive()
        {
            _logger.Info("Start receiving");
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQ:HostName"],
                Port = int.Parse(_configuration["RabbitMQ:Port"]),
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"]
            };

            _logger.Info(factory.HostName + factory.Port.ToString());

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "npaperless-queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                _logger.Info("U are here");
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    HandleMessage(message);
                    _logger.Info("Received message " + message);
                };

                channel.BasicConsume(queue: "npaperless-queue",
                                     autoAck: true,
                                     consumer: consumer);
            }
        }

        private void HandleMessage(string message)
        {
            if (this.OnReceived != null)
            {
                this.OnReceived(this, new QueueReceivedEvent(message));
            }
        }


    }
}
