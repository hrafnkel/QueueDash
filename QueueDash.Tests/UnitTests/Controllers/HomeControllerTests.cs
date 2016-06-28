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
            QueueData queueData = new QueueData {Depth = 1, Name = "Name"};
            List<QueueData> queues = new List<QueueData> {queueData};
            DashboardViewModel vm = new DashboardViewModel {Queues = queues};
            _presenterMock.Setup(x => x.GetDashboardData()).Returns(vm);
            HomeController controller = new HomeController(_presenterMock.Object);

            ViewResult result = (ViewResult) controller.Index();

            DashboardViewModel resultViewModel = (DashboardViewModel) result.Model;
            int count = resultViewModel.Queues.Count;
            string name = resultViewModel.Queues[0].Name;
            Assert.That(count, Is.EqualTo(1));
            Assert.That(name,Is.EqualTo("Name"));
        }
    }
}
