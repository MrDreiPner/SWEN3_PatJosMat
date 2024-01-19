using EasyNetQ;
using EasyNetQ.Management.Client;
using EasyNetQ.Management.Client.Model;
using Microsoft.Extensions.Configuration; 


using NPaperless.BusinessLogic.Interfaces;


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
            string host = _configuration["RabbitMQ:Host"];
            string userName = _configuration["RabbitMQ:UserName"];
            string password = _configuration["RabbitMQ:Password"];
            string queueName = _configuration["RabbitMQ:Queue"];

            string connectionString = $"host={host};username={userName};password={password}";

            using (var bus = RabbitHutch.CreateBus(connectionString))
            {
                bus.SendReceive.Send(queueName, message);
            } 
        }
    }
}
