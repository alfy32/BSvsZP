using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using AgentCommon;

namespace AgentCommonTester
{
  [TestClass]
  public class MessageQueueTester
  {
    [TestMethod]
    public void MessageQueue_push()
    {
      MessageQueue messageQueue = new MessageQueue();

      Envelope envelope = new Envelope();

      messageQueue.push(envelope);

      Assert.AreEqual(true, messageQueue.hasItems());
    }

    [TestMethod]
    public void MessageQueue_pop()
    {
      MessageQueue messageQueue = new MessageQueue();

      Envelope envelope = new Envelope();

      messageQueue.push(envelope);

      Assert.AreEqual(true, messageQueue.hasItems());

      Assert.AreEqual(envelope, messageQueue.pop());

      Assert.AreEqual(false, messageQueue.hasItems());
    }
  }
}
