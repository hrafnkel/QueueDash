using System.Collections.Generic;
using System.Messaging;
using Moq;
using NUnit.Framework;
using QueueDash.Builders;
using QueueDash.Models;
using QueueDash.Repositories;

namespace QueueDash.Tests.UnitTests.Builders
{
    [TestFixture]
    public class QueueBuilderTests
    {
        private MessageQueue _mq;
        private List<MessageQueue> _queueList;

        private QueueBuilder _queueBuilder;
        private Mock<IQueueRepository> _queueRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _queueRepositoryMock = new Mock<IQueueRepository>(MockBehavior.Strict);
            _queueBuilder = new QueueBuilder(_queueRepositoryMock.Object);
        }

        [Test]
        public void QueueBuilder_Is_Created()
        {
            Assert.IsNotNull(_queueBuilder);
        }

        [Test]
        public void Builder_Is_As_Expected()
        {
            Assert.That(_queueBuilder, Is.TypeOf<QueueBuilder>());
        }

        [Test]
        public void GetLocalQueues_Is_Called_From_Builder()
        {
            _queueRepositoryMock.Setup(x => x.GetLocalQueues()).Returns(_queueList);

            _queueBuilder.GetLocalQueues();

            _queueRepositoryMock.Verify(x => x.GetLocalQueues());
        }

        [Test]
        public void GetLocalQueues_Returns_List_Of_MessageQueues()
        {
            _queueRepositoryMock.Setup(x => x.GetLocalQueues()).Returns(_queueList);

            List<MessageQueue> result = _queueBuilder.GetLocalQueues();

            Assert.That(result,Is.TypeOf<List<MessageQueue>>());
        }

        [Test]
        public void GetLocalQueueNames_Returns_Queue_Names()
        {
            const string expected = "DIRECT=OS:bur5-9slsv42\\private$\\test";
            GivenAListOfQueues();
            _queueRepositoryMock.Setup(x => x.GetLocalQueues()).Returns(_queueList);

            List<string> queueNames = _queueBuilder.GetLocalQueueNames();
            var name = queueNames[0];

            Assert.That(name, Is.EqualTo(expected));
        }

        [Test]
        public void GetLocalQueueDetails_Returns_Queue_Details()
        {
            const string expectedLongName = "DIRECT=OS:bur5-9slsv42\\private$\\test";
            const string expectedShortName = "private$\\test";
            GivenAListOfQueues();
            _queueRepositoryMock.Setup(x => x.GetLocalQueues()).Returns(_queueList);

            List<QueueDetails> queueDetails = _queueBuilder.GetLocalQueueDetails();
            QueueDetails detail = queueDetails[0];
            string longName = detail.LongName;
            string name = detail.Name;

            Assert.That(longName, Is.EqualTo(expectedLongName));
            Assert.That(name, Is.EqualTo(expectedShortName));
        }

        private void GivenAListOfQueues()
        {
            const string testQueue = @".\private$\test";
            _mq = !MessageQueue.Exists(testQueue) ? MessageQueue.Create(testQueue, false) : new MessageQueue(testQueue);
            _queueList = new List<MessageQueue> { _mq };
        }

        [TearDown]
        public void Teardown()
        {
            const string testQueue = @".\private$\test";
            if (MessageQueue.Exists(testQueue)) MessageQueue.Delete(testQueue);
        }
    }
}
