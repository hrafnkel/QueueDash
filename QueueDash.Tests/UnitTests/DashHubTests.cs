using System;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using NUnit.Framework;
using QueueDash.Models;
using QueueDash.Repositories;

namespace QueueDash.Tests.UnitTests
{
    [TestFixture]
    public class DashHubTests
    {
        private Mock<QueueRepository> _repositoryMock;
        private DashHub _hub;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<QueueRepository>();
            _hub = new DashHub(_repositoryMock.Object);
        }

        [Test]
        public void HubsHelloIsExecuted()
        {
            bool helloCalled = false;
            Mock<IHubCallerConnectionContext<dynamic>> mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            _hub.Clients = mockClients.Object;
            dynamic all = new ExpandoObject();
            all.populateQueues = new Action<List<QueueData>>((queues) => { helloCalled = true; });
            mockClients.Setup(m => m.All).Returns((ExpandoObject)all);
            _hub.Hello();
            Assert.True(helloCalled);
        }

        [Test]
        public void HubsRefreshIsExecuted()
        {
            bool refreshCalled = true;
            Mock<IHubCallerConnectionContext<dynamic>> mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            _hub.Clients = mockClients.Object;
            dynamic all = new ExpandoObject();
            all.populateQueues = new Action<List<QueueData>>((queues) => { refreshCalled = true; });
            mockClients.Setup(m => m.All).Returns((ExpandoObject)all);
            _hub.Refresh(true);
            Assert.True(refreshCalled);
        }
    }
}
