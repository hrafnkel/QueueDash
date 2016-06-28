﻿using System;
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
        const string TestQueue = @".\private$\test";
        private MessageQueue _mq;
        private List<MessageQueue> _queueList;

        private QueueBuilder _queueBuilder;
        private Mock<IQueueRepository> _queueRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _queueRepositoryMock = new Mock<IQueueRepository>();
            _queueBuilder = new QueueBuilder(_queueRepositoryMock.Object);

            _mq = !MessageQueue.Exists(TestQueue) ? MessageQueue.Create(TestQueue, false) : new MessageQueue(TestQueue);
            _queueList = new List<MessageQueue> { _mq };
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
            _queueRepositoryMock.Setup(x => x.GetLocalQueues());

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
            _queueRepositoryMock.Setup(x => x.GetLocalQueues()).Returns(_queueList);

            List<string> queueNames = _queueBuilder.GetLocalQueueNames();
            var name = queueNames[0];

            Assert.That(name, Is.EqualTo("DIRECT=OS:bur5-9slsv42\\private$\\test"));
        }

        [Test]
        public void GetLocalQueueDetails_Returns_Queue_Details()
        {
            _queueRepositoryMock.Setup(x => x.GetLocalQueues()).Returns(_queueList);

            List<QueueDetails> queueDetails = _queueBuilder.GetLocalQueueDetails();
            QueueDetails detail = queueDetails[0];
            string longName = detail.LongName;
            string name = detail.Name;

            Assert.That(longName, Is.EqualTo("DIRECT=OS:bur5-9slsv42\\private$\\test"));
            Assert.That(name, Is.EqualTo("private$\\test"));
        }

        [TearDown]
        public void Teardown()
        {
            if (MessageQueue.Exists(TestQueue)) MessageQueue.Delete(TestQueue);
        }

        private void Write_HelloWorld_On_Queue()
        {
            string message = "Hello World";
            _mq.Formatter = new XmlMessageFormatter();
            _mq.Send(message);
        }
    }
}
