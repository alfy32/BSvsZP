using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using AgentCommon;
using Messages;
using Common;
using System.Net;

namespace AgentCommonTester
{
  [TestClass]
  public class DoerTester
  {
    [TestMethod]
    public void Doer_Test()
    {
      Communicator communicator = new Communicator(Communicator.nextAvailablePort());

      Doer doer = new Doer(communicator);

      doer.Start();

      MessageQueue requestMessageQueue = RequestMessageQueue.getQueue();

      //create message
      AgentInfo agentInfo = new AgentInfo();
      agentInfo.AgentType = AgentInfo.PossibleAgentType.BrilliantStudent;
      Message message = new JoinGame(10, agentInfo);
      IPEndPoint localEP = new IPEndPoint(IPAddress.Loopback, Communicator.nextAvailablePort());
      Common.EndPoint endPoint = new Common.EndPoint(localEP);
      Envelope envelope = new Envelope(message, endPoint);

      requestMessageQueue.push(envelope);

      doer.Suspend();

      doer.Resume();

      doer.Stop();

      //Assert the conversation was executed
    }
  }
}
