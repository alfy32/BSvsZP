using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentCommon;
using Messages;
using Common;

namespace BrilliantStudent
{
  public class StrategyMove : ExecutionStrategy
  {
    public StrategyMove(Agent agent)
      : base(agent) { }

    public override void Execute(Object startEnvelope)
    {
      Envelope envelope = (Envelope)startEnvelope;
      MessageQueue messageQueue = ConversationMessageQueues.getQueue(envelope.message.ConversationId);
      if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.Move)
      {
        agent.Communicator.Send(envelope);
        StatusMonitor.get().postDebug("Sent Move message.");

        while (!messageQueue.hasItems())
          System.Threading.Thread.Sleep(1);

        Envelope response = messageQueue.pop();
        if (response.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AckNak)
        {
          AckNak ackNak = (AckNak)response.message;
          StatusMonitor.get().postDebug("Recieved move response Message.");

          if (ackNak.Status == Reply.PossibleStatus.Success)
          {
            StatusMonitor.get().postStatus("Agent Moved Successfully.");
            agent.State.AgentInfo = (AgentInfo)ackNak.ObjResult;
          }
          else
          {
            StatusMonitor.get().postDebug("Agent Couldn't Move. Error: " + ackNak.Message);
          }
        }
      }
    }
  }
}
