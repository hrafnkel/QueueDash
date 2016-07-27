using System.Collections.Generic;
using System.Diagnostics;
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

        public void Hello()
        {
            List<QueueData> queues = _repository.GetDashboardData();
            Clients.All.populateQueues(queues);
        }

        public void Refresh(bool calledFromTest)
        {
            int timeout = 50000;
            Stopwatch sw = Stopwatch.StartNew();
            while (true)
            {
                List<QueueData> queues = _repository.GetDashboardData();
                Clients.All.populateQueues(queues);
                System.Threading.Thread.Sleep(1000);
                if (calledFromTest && (sw.ElapsedMilliseconds > timeout)) break;
            }
            return;
        }
    }
}