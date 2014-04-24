using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;
using Common;

namespace AgentCommon
{
  public class StrategyGetLayout : StrategyGetResource
  {
    public StrategyGetLayout(Agent agent)
      : base(agent) { }

    private void sendMessage(Envelope envelope)
    {
      GetResource getResource = (GetResource)envelope.message;
      getResource.GameId = agent.State.AgentInfo.Id;
      getResource.EnablingTick = agent.getTickFromStash();

      StatusMonitor.get().postDebug("Sent Get " + getResource.GetResourceType.ToString() + " Message.");
      agent.Communicator.Send(envelope);
    }

    private void handleResponse(Envelope envelope)
    {
      if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.PlayingFieldReply)
      {
        PlayingFieldReply reply = (PlayingFieldReply)envelope.message;
        if (reply.Status == Reply.PossibleStatus.Success)
        {
          StatusMonitor.get().postDebug("Recieved playing field reply");
          agent.State.PlayingFieldLayout = reply.Layout;
        }
        else
        {
          StatusMonitor.get().postDebug("Failed to get Playing Field Layout");
        }
      }
    }
    public override void Execute(Object startEnvelope)
    {
      Envelope envelope = (Envelope)startEnvelope;
      MessageQueue messageQueue = ConversationMessageQueues.getQueue(envelope.message.ConversationId);
      if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.GetResource)
      {
        sendMessage(envelope);

        while (!messageQueue.hasItems())
          System.Threading.Thread.Sleep(10);

        handleResponse(messageQueue.pop());
      }
    }
  }
}
