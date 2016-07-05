using System;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using NUnit.Framework;
using QueueDash.Models;

namespace QueueDash.Tests.UnitTests
{
    [TestFixture]
    public class DashHubTests
    {
        [Test]
        public void HubsHelloIsExecuted()
        {
            bool helloCalled = false;
            DashHub hub = new DashHub();
            Mock<IHubCallerConnectionContext<dynamic>> mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            hub.Clients = mockClients.Object;
            dynamic all = new ExpandoObject();
            all.populateQueues = new Action<List<QueueData>>((queues) => { helloCalled = true; });
            mockClients.Setup(m => m.All).Returns((ExpandoObject)all);
            hub.Hello();
            Assert.True(helloCalled);
        }
    }
}
