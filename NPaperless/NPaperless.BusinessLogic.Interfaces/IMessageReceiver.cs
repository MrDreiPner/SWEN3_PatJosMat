using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPaperless.BusinessLogic;

namespace NPaperless.BusinessLogic.Interfaces
{
    public interface IMessageReceiver
    {
        public event EventHandler<IQueueReceivedEvent> OnReceived;
        public void Receive();
    }
}
