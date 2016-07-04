﻿using System.Collections.Generic;
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
    }
}