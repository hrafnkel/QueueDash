using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;

namespace QueueDash.Repositories
{
    public class QueueRepository : IQueueRepository
    {
        public List<MessageQueue> GetLocalQueues()
        {
            string machineName = GetMachineName();
            return GetPrivateQueuesByMachine(machineName);
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