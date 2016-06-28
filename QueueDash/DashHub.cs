using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using QueueDash.Models;
using QueueDash.Presenters;

namespace QueueDash
{
    public class DashHub : Hub
    {
        private readonly IQueueDashPresenter _presenter;

        public DashHub(IQueueDashPresenter presenter)
        {
            _presenter = presenter;
        }

        public void Hello()
        {
            DashboardViewModel vm = _presenter.GetDashboardData();
            List<QueueData> queues = vm.Queues;

            dynamic caller = Clients.Caller;
            caller.populateQueues(queues.ToArray());
        }
    }
}