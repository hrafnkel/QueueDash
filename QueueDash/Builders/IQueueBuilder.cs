using System.Collections.Generic;
using System.Messaging;
using QueueDash.Models;

namespace QueueDash.Builders
{
    public interface IQueueBuilder
    {
        List<MessageQueue> GetLocalQueues();
        List<string> GetLocalQueueNames();
        List<QueueDetails> GetLocalQueueDetails();
    }
}