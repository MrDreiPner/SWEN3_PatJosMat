using NPaperless.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPaperless.BusinessLogic.RabbitMQ
{
    public class QueueReceivedEvent : IQueueReceivedEvent
    {
        public QueueReceivedEvent(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
