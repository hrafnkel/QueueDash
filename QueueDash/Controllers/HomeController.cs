using System.Web.Mvc;
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
            DashboardViewModel vm = _presenter.GetDashboardData();
            return View(vm);
        }
    }
}