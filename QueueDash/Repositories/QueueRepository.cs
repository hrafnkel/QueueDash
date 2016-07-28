using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using QueueDash.Models;

namespace QueueDash.Repositories
{
    public class QueueRepository : IQueueRepository
    {
        public List<MessageQueue> GetLocalQueues()
        {
            string machineName = GetMachineName();
            return GetPrivateQueuesByMachine(machineName);
        }

        public List<QueueData> GetDashboardData()
        {
            List<QueueDetails> queueDetails = GetLocalQueueDetails();
            return MapDetailsToData(queueDetails);
        }

        private List<QueueDetails> GetLocalQueueDetails()
        {
            List<MessageQueue> queues = GetLocalQueues();

            return queues.Select(messageQueue => new QueueDetails
            {
                Depth = GetQueueDepth(messageQueue),
                LongName = messageQueue.FormatName,
                Name = messageQueue.QueueName
            }).ToList();
        }

        private List<QueueData> MapDetailsToData(List<QueueDetails> queueDetails)
        {
            List<QueueData> queuesData = new List<QueueData>();
            foreach (QueueDetails details in queueDetails)
            {
                QueueData data = new QueueData
                {
                    Depth = details.Depth,
                    Name = TrimQueueName(details.Name)
                };

                queuesData.Add(data);
            }
            return queuesData;
        }

        private string TrimQueueName(string name)
        {
            string subString = name.Substring(9);
            return subString;
        }

        private int GetQueueDepth(MessageQueue mq)
        {
            int count = 0;
            MessageEnumerator enumerator = mq.GetMessageEnumerator2();
            while (enumerator.MoveNext()) count++;

            return count;
        }

        private string GetMachineName()
        {
            return Environment.MachineName;
        }

        private List<MessageQueue> GetPrivateQueuesByMachine(string machineName)
        {
            return MessageQueue.GetPrivateQueuesByMachine(machineName).ToList();
        }
    }
}