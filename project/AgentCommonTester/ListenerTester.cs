using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using AgentCommon;
using Common;
using Messages;
using System.Net;

namespace AgentCommonTester
{
  [TestClass]
  public class ListenerTester
  {
    [TestMethod]
    public void Listener_Listen()
    {
      int senderPort = Communicator.nextAvailablePort();
      int recieverPort = Communicator.nextAvailablePort();

      Communicator sender = new Communicator(senderPort);
      Communicator reciever = new Communicator(recieverPort);

      MessageQueue messageQueue = RequestMessageQueue.getQueue();

      Listener listener = new Listener(reciever);
      listener.Start();

      AgentInfo agentInfo = new AgentInfo();
      agentInfo.ANumber = "A00123";
      agentInfo.FirstName = "John";
      agentInfo.LastName = "Jones";

      Message message = new JoinGame(10, agentInfo);
      IPEndPoint localEP = new IPEndPoint(IPAddress.Loopback, recieverPort);
      Common.EndPoint endPoint = new Common.EndPoint(localEP);
      Envelope envelope = new Envelope(message, endPoint);

      sender.Send(envelope);

      listener.Stop();

      Assert.AreEqual(true, messageQueue.hasItems());

      JoinGame jg = (JoinGame)messageQueue.pop().message;

      Assert.AreEqual(10, jg.GameId);
      Assert.AreEqual("A00123", jg.AgentInfo.ANumber);
      Assert.AreEqual("John", jg.AgentInfo.FirstName);
      Assert.AreEqual("Jones", jg.AgentInfo.LastName);
    }
  }
}
