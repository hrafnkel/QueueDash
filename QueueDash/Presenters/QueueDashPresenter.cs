using QueueDash.Builders;
using QueueDash.Models;
using System.Collections.Generic;

namespace QueueDash.Presenters
{

    public class QueueDashPresenter : IQueueDashPresenter
    {
        private readonly IQueueBuilder _queueBuilder;

        public QueueDashPresenter(IQueueBuilder queueBuilder)
        {
            _queueBuilder = queueBuilder;
        }

        public DashboardViewModel GetDashboardData()
        {
            List<QueueDetails> queueDetails = _queueBuilder.GetLocalQueueDetails();
            return MapDetailsToModel(queueDetails);
        }

        private DashboardViewModel MapDetailsToModel(List<QueueDetails> queueDetails)
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

            return new DashboardViewModel
            {
                Queues = queuesData
            };
        }

        private string TrimQueueName(string name)
        {
            return name.Substring(9);
        }
    }
}