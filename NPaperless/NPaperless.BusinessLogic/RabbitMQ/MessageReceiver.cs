using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using NPaperless.BusinessLogic.Interfaces;
using log4net;
using NPaperless.BusinessLogic.Services;
using EasyNetQ;
using EasyNetQ.Management.Client.Model;

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
            string host = _configuration["RabbitMQ:Host"];
            string userName = _configuration["RabbitMQ:UserName"];
            string password = _configuration["RabbitMQ:Password"];
            string queueName = _configuration["RabbitMQ:Queue"];

            string connectionString = $"host={host};username={userName};password={password}";

            var bus = RabbitHutch.CreateBus(connectionString);
            bus.SendReceive.Receive<string>(queueName, message => this.OnReceived(this, new QueueReceivedEvent(message)));
        }
    }
}
