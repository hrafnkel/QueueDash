using System.Collections.Generic;
using System.Messaging;

namespace QueueDash.Repositories
{
    public interface IQueueRepository
    {
        List<MessageQueue> GetLocalQueues();
    }
}