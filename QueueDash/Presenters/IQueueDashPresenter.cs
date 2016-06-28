using QueueDash.Models;

namespace QueueDash.Presenters
{
    public interface IQueueDashPresenter
    {
        DashboardViewModel GetDashboardData();
    }
}