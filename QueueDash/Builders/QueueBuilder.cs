using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using QueueDash.Models;
using QueueDash.Repositories;

namespace QueueDash.Builders
{
    public class QueueBuilder : IQueueBuilder
    {
        private readonly IQueueRepository _queueRepository;

        public QueueBuilder(IQueueRepository queueRepository)
        {
            _queueRepository = queueRepository;
        }

        public List<MessageQueue> GetLocalQueues()
        {
            return _queueRepository.GetLocalQueues();
        }

        public List<string> GetLocalQueueNames()
        {
            List<MessageQueue> queues = GetLocalQueues();
            return queues.Select(queue => queue.FormatName).ToList();
        }

        public List<QueueDetails> GetLocalQueueDetails()
        {
            List<MessageQueue> queues = GetLocalQueues();

            return queues.Select(messageQueue => new QueueDetails
            {
                Depth = GetQueueDepth(messageQueue), LongName = messageQueue.FormatName, Name = messageQueue.QueueName
            }).ToList();
        }

        private int GetQueueDepth(MessageQueue mq)
        {
            int count = 0;
            var enumerator = mq.GetMessageEnumerator2();
            while (enumerator.MoveNext())
                count++;

            return count;
        }
    }
}