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
  public class StrategyGetExcuse : ExecutionStrategy
  {
    public StrategyGetExcuse(Agent agent)
      : base(agent) { }

    public override void Execute(Object startEnvelope)
    {
      Envelope envelope = (Envelope)startEnvelope;
      MessageQueue messageQueue = ConversationMessageQueues.getQueue(envelope.message.ConversationId);
      if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.GetResource)
      {
        agent.Communicator.Send(envelope);
        StatusMonitor.get().postStatus("Asked " + envelope.endPoint.ToString() + " for and excuse.");

        while (!messageQueue.hasItems())
          System.Threading.Thread.Sleep(1);

        Envelope response = messageQueue.pop();

        if (response.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.ResourceReply)
        {
          ResourceReply reply = (ResourceReply)response.message;
          if (reply.Status == Reply.PossibleStatus.Success)
          {
            StatusMonitor.get().postStatus("Recieved excuse");
            ((BrilliantBrain)agent.Brain).gotExcuse((Excuse)reply.Resource);
          }
          else
          {
            StatusMonitor.get().postStatus("Failed to get excuse: " + reply.Note);
          }
        }
      }
    }
  }
}
