using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using QueueDash.Models;
using QueueDash.Presenters;

namespace QueueDash.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQueueDashPresenter _presenter;

        public HomeController(IQueueDashPresenter presenter)
        {
            _presenter = presenter;
        }

        // GET: Home
        public ActionResult Index()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<DashHub>();
            DashboardViewModel vm = _presenter.GetDashboardData();
            context.Clients.All.Hello(vm);
            return View();
        }
    }
}