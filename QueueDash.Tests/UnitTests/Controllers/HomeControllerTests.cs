using System.Web.Mvc;
using NUnit.Framework;
using QueueDash.Controllers;
using QueueDash.Models;

namespace QueueDash.Tests.UnitTests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void CreateDashboardViewModel()
        {
            HomeController controller = new HomeController();

            ViewResult result = (ViewResult) controller.Index();

            DashboardViewModel resultViewModel = (DashboardViewModel) result.Model;
            
            Assert.That(resultViewModel, Is.EqualTo(null));
        }
    }
}
