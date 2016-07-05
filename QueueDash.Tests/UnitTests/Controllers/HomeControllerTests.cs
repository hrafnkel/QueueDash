using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using QueueDash.Controllers;
using QueueDash.Models;
using QueueDash.Presenters;

namespace QueueDash.Tests.UnitTests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private Mock<IQueueDashPresenter> _presenterMock;

        [SetUp]
        public void SetUp()
        {
            _presenterMock = new Mock<IQueueDashPresenter>();
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
