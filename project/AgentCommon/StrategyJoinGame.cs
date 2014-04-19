using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;
using Common;

namespace AgentCommon
{
  public class StrategyJoinGame : ExecutionStrategy
  {
    Agent agent;

    public StrategyJoinGame(int conversationId, Agent agent)
      : base(conversationId) 
    {
      this.agent = agent;
    }

    protected override void Execute()
    {
      if(messageQueue.hasItems())
      {
        StatusMonitor statusMonitor = StatusMonitor.get();

        Envelope envelope = messageQueue.pop();
        JoinGame joinGame = (JoinGame)envelope.message;

        statusMonitor.postDebug("Sending JoinGame message");
        agent.Communicator.Send(envelope);

        while (!messageQueue.hasItems())
        {
          System.Threading.Thread.Sleep(1);
        }

        envelope = messageQueue.pop();
        AckNak ackNak = (AckNak)envelope.message;
        statusMonitor.postDebug("Recieved AckNack message");

        if (ackNak.Status == Reply.PossibleStatus.Success)
        {
          statusMonitor.postDebug("AckNack status success.");

          AgentInfo resultAgentInfo = (AgentInfo)ackNak.ObjResult;
          statusMonitor.postDebug("Agent Status: " + resultAgentInfo.AgentStatus);
          agent.State.AgentInfo = resultAgentInfo;

          statusMonitor.postDebug("Sending join game ack...");
          AckNak ack = new AckNak(Reply.PossibleStatus.Success);
          ack.ConversationId = envelope.message.ConversationId;
          agent.Communicator.Send(new Envelope(ack, envelope.endPoint));
        }
        else
        {
          statusMonitor.postDebug("AckNack status: " + ackNak.Status.ToString());
          statusMonitor.postDebug(ackNak.Message);
        }

        Stop();
      }
    }
  }
}
