using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using AgentCommon;
using Messages;
using Common;

namespace AgentCommonTester
{
    [TestClass]
    public class EnvelopeTester
    {
        [TestMethod]
        public void Envelope_CheckConstructors()
        {
            Envelope envelopeEmpty = new Envelope();

            Assert.AreEqual(null, envelopeEmpty.message);
            Assert.AreEqual(null, envelopeEmpty.endPoint);

            Message message = new EndGame();
            EndPoint endPoint = new EndPoint();

            Envelope envelope = new Envelope(message, endPoint);

            Assert.AreEqual(message, envelope.message);
            Assert.AreEqual(endPoint, envelope.endPoint);
        }
    }
}
