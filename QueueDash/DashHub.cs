using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using QueueDash.Models;
using QueueDash.Presenters;

namespace QueueDash
{
    [HubName("dashHub")]
    public class DashHub : Hub
    {
        //private readonly IQueueDashPresenter _presenter;

        //public DashHub(IQueueDashPresenter presenter)
        //{
        //    _presenter = presenter;
        //}

        public void Hello(DashboardViewModel vm)
        {
            //DashboardViewModel vm = _presenter.GetDashboardData();
            List<QueueData> queues = vm.Queues;
            Clients.All.populateQueues(queues.ToArray());
        }
    }
}