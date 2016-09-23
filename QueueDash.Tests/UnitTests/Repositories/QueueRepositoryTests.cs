using System.Collections.Generic;
using System.Messaging;
using NUnit.Framework;
using QueueDash.Repositories;

namespace QueueDash.Tests.UnitTests.Repositories
{
    [TestFixture]
    public class QueueRepositoryTests
    {
        private QueueRepository _queueRepository;

        [SetUp]
        public void SetUp()
        {
            _queueRepository = new QueueRepository();
        }

        [Test]
        public void QueueRepository_Is_Created()
        {
            Assert.IsNotNull(_queueRepository);
        }

        [Test]
        public void Repository_Is_As_Expected()
        {
            Assert.That(_queueRepository, Is.TypeOf<QueueRepository>());
        }

        [Test]
        public void Repository_Returns_List_Of_Local_Queues()
        {
            List<MessageQueue> queues = _queueRepository.GetLocalQueues();
            int count = queues.Count;
            Assert.That(count,Is.GreaterThan(0));
        }
    }
}
