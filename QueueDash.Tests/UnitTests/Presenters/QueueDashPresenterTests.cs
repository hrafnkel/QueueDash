using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using QueueDash.Builders;
using QueueDash.Models;
using QueueDash.Presenters;

namespace QueueDash.Tests.UnitTests.Presenters
{
    [TestFixture]
    public class QueueDashPresenterTests
    {
        private Mock<IQueueBuilder> _queueBuilderMock;
        QueueDashPresenter _presenter;

        [SetUp]
        public void SetUp()
        {
            _queueBuilderMock = new Mock<IQueueBuilder>();
            _presenter = new QueueDashPresenter(_queueBuilderMock.Object);
        }

        [Test]
        public void GetDashboardData_Calls_GetLocalQueueDetails()
        {
            List<QueueDetails> queueDetails = GetQueueDetails();

            _queueBuilderMock.Setup(x => x.GetLocalQueueDetails()).Returns(queueDetails);

            _presenter.GetDashboardData();

            _queueBuilderMock.Verify(x => x.GetLocalQueueDetails());
        }

        [Test]
        public void GetDashboardData_Returns_DashboardViewModel()
        {
            List<QueueDetails> queueDetails = GetQueueDetails();

            _queueBuilderMock.Setup(x => x.GetLocalQueueDetails()).Returns(queueDetails);

            DashboardViewModel viewModel = _presenter.GetDashboardData();
            QueueData data = viewModel.Queues[0];

            Assert.That(data.Name, Is.EqualTo("test"));
            Assert.That(data.Depth, Is.EqualTo(1));
        }

        private List<QueueDetails> GetQueueDetails()
        {
            return new List<QueueDetails>
            {
                new QueueDetails
                {
                    Depth = 1,
                    LongName = "Long name",
                    Name = @".\private$\test"
                }
            };
        }
    }
}
