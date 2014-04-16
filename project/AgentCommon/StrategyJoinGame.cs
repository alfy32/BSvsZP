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
        Envelope envelope = messageQueue.pop();

        JoinGame joinGame = (JoinGame)envelope.message;

        agent.Communicator.Send(envelope);

        while (!messageQueue.hasItems())
        {
          System.Threading.Thread.Sleep(1);
        }

        envelope = messageQueue.pop();
        AckNak ackNak = (AckNak)envelope.message;

        if (ackNak.Status == Reply.PossibleStatus.Success)
        {
          StatusMonitor statusMonitor = StatusMonitor.get();

          statusMonitor.post("Successfully joined a game...");

          AgentInfo resultAgentInfo = (AgentInfo)ackNak.ObjResult;
          statusMonitor.post(" Status: " + resultAgentInfo.AgentStatus);
          statusMonitor.post(" Location: " + resultAgentInfo.Location);
          statusMonitor.post(" Strength: " + resultAgentInfo.Strength);
          statusMonitor.post("");

          agent.State.updateAgentInfo(resultAgentInfo);

          AckNak ack = new AckNak(Reply.PossibleStatus.Success);
          ack.ConversationId.SeqNumber = envelope.message.ConversationId.SeqNumber;
          Envelope toSend = new Envelope(ack, envelope.endPoint);

          statusMonitor.post("Sending join game ack...");
          agent.Communicator.Send(toSend);
        }
        else
        {
          StatusMonitor statusMonitor = StatusMonitor.get();

          statusMonitor.post("Failed to join the game...");
          statusMonitor.post(ackNak.Message);
        }

        Stop();
      }
    }
  }
}
