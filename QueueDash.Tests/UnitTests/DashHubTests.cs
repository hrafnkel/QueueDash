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
        [Test]
        public void HubsAreMockableViaDynamic()
        {
            bool helloCalled = false;
            var hub = new DashHub();
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            var repositoryMoq = new Mock<IQueueRepository>();
            hub.Clients = mockClients.Object;
            dynamic all = new ExpandoObject();
            all.populateQueues = new Action<List<QueueData>>((queues) => { helloCalled = true; });
            repositoryMoq.Setup(x => x.GetLocalQueues());
            mockClients.Setup(m => m.All).Returns((ExpandoObject)all);
            hub.Hello();
            Assert.True(helloCalled);
        }
    }
}
