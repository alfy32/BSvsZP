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
    public StrategyJoinGame(Agent agent)
      : base(agent) { }

    public override void Execute(Object startEnvelope)
    {
      Envelope envelope = (Envelope)startEnvelope;
      JoinGame joinGame = (JoinGame)envelope.message;

      StatusMonitor statusMonitor = StatusMonitor.get();
      MessageQueue messageQueue = ConversationMessageQueues.getQueue(envelope.message.ConversationId);

      agent.Communicator.Send(envelope);
      statusMonitor.postDebug("Sent JoinGame message");

      while (!messageQueue.hasItems())
        System.Threading.Thread.Sleep(1);

      Envelope response = messageQueue.pop();
      AckNak ackNak = (AckNak)response.message;
      statusMonitor.postDebug("Recieved JoinGame ack. Status: " + ackNak.Status.ToString());
      agent.State.GameEndPoint = response.endPoint;

      if (ackNak.Status == Reply.PossibleStatus.Success)
      {
        AgentInfo resultAgentInfo = (AgentInfo)ackNak.ObjResult;
        statusMonitor.postDebug("Agent Status: " + resultAgentInfo.AgentStatus);
        agent.State.AgentInfo = resultAgentInfo;

        AckNak ack = new AckNak(Reply.PossibleStatus.Success);
        ack.ConversationId = envelope.message.ConversationId;
        agent.Communicator.Send(new Envelope(ack, envelope.endPoint));
        statusMonitor.postDebug("Sent JoinGame ack...");

        agent.Brain.getResource(GetResource.PossibleResourceType.GameConfiguration);
      }
      else
      {
        statusMonitor.postDebug("JoinGame failed: " + ackNak.Message);
      }
    }
  }
}
