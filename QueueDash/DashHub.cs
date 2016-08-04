using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using QueueDash.Models;
using QueueDash.Repositories;

namespace QueueDash
{
    [HubName("dashHub")]
    public class DashHub : Hub
    {
        readonly QueueRepository _repository = new QueueRepository();
        private List<QueueData> _oldQueueData = new List<QueueData>();

        public void Hello()
        {
            List<QueueData> queues = _repository.GetDashboardData();
            Clients.All.populateQueues(queues);
        }

        public void Refresh(bool calledFromTest)
        {
            double timeout = TimeSpan.FromMilliseconds(50000).TotalMilliseconds;
            Stopwatch sw = Stopwatch.StartNew();
            while (true)
            {
                List<QueueData> queues = _repository.GetDashboardData();
                if (QueueDataHasChanged(queues)) Clients.All.populateQueues(queues);
                System.Threading.Thread.Sleep(2000);
                if (calledFromTest && (sw.ElapsedMilliseconds > timeout)) break;
            }
        }

        private bool QueueDataHasChanged(List<QueueData> queues)
        {
            if (_oldQueueData.Count != queues.Count)
            {
                _oldQueueData = queues;
                return true;
            }

            if ((from data in queues
                 let queueData = _oldQueueData.FirstOrDefault(x => x.Name == data.Name)
                 where (queueData == null)||(queueData.Name != data.Name)||(queueData.Depth != data.Depth)
                 select data).Any())
            {
                _oldQueueData = queues;
                return true;
            }

            return false;
        }
    }
}