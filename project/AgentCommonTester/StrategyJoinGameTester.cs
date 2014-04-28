using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using AgentCommon;
using Messages;
using Common;
using BrilliantStudent;

namespace AgentCommonTester
{
  [TestClass]
  public class StrategyJoinGameTester
  {
    [TestMethod]
    public void StrategyJoinGame_Tester()
    {
      EndPoint endPoint = new EndPoint("localhost", 51300);
      Communicator communicator = new Communicator(endPoint.Port);

      BrilliantStudent.BrilliantStudent agent = new BrilliantStudent.BrilliantStudent();
      agent.startJoinGameConversation(10, endPoint);

      while (communicator.GetAvailable() == 0)
        System.Threading.Thread.Sleep(1);

      Envelope envelope = communicator.Recieve();
      
      Assert.IsTrue(envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.JoinGame);

      AgentInfo agentInfo = agent.State.AgentInfo;
      agentInfo.AgentStatus = AgentInfo.PossibleAgentStatus.InGame;
      AckNak ack = new AckNak(Reply.PossibleStatus.Success, agentInfo);
      ack.ConversationId = envelope.message.ConversationId;
      communicator.Send(new Envelope(ack, envelope.endPoint));

      while (communicator.GetAvailable() == 0)
        System.Threading.Thread.Sleep(1);

      envelope = communicator.Recieve();

      Assert.IsTrue(envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AckNak);

      AckNak ackNack = (AckNak)envelope.message;

      Assert.IsTrue(ackNack.Status == Reply.PossibleStatus.Success);
    }
  }
}
