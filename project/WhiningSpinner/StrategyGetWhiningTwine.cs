using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentCommon;
using Messages;
using Common;

namespace WhiningSpinner
{
  public class StrategyGetWhiningTwine : ExecutionStrategy
  {
    private Agent agent;

    public StrategyGetWhiningTwine(int conversationId, Agent agent)
      : base(conversationId)
    {
      this.agent = agent;
    }

    protected override void Execute()
    {
      if (messageQueue.hasItems())
      {
        Envelope envelope = messageQueue.pop();
        if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.GetResource)
        {
          GetResource getResource = (GetResource)envelope.message;
          if (getResource.GetResourceType != GetResource.PossibleResourceType.WhiningTwine)
          {
            StatusMonitor.get().postDebug("Someone asked for a resource that is not whiningTwine.");
            ResourceReply failedResourceReply = new ResourceReply(Reply.PossibleStatus.Failure, null, "I only have whining twine!");
            failedResourceReply.ConversationId = envelope.message.ConversationId;
            agent.Communicator.Send(new Envelope(failedResourceReply, envelope.endPoint));
            Stop();
          }

          WhiningSpinner spinner = (WhiningSpinner)agent;
          ResourceReply resourceReply = null;

          if (getResource.EnablingTick != null && spinner.TwineAvailable())
          {
            WhiningTwine twine = spinner.getTwine();
            twine.RequestTick = getResource.EnablingTick;
            resourceReply = new ResourceReply(Reply.PossibleStatus.Success, twine);

          }
          else if (getResource.EnablingTick == null)
          {
            StatusMonitor.get().postDebug("Agent at " + envelope.endPoint + " didn't give me a tick!");
            resourceReply = new ResourceReply(Reply.PossibleStatus.Failure, null, "Your enabling tick was null. Bad Agent!");
          }
          else
          {
            StatusMonitor.get().postDebug("Agent at " + envelope.endPoint + " asked for twine but I don't have any");
            resourceReply = new ResourceReply(Reply.PossibleStatus.Failure, null, "No twine available.");
          }

          resourceReply.ConversationId = envelope.message.ConversationId;
          Envelope response = new Envelope(resourceReply, envelope.endPoint);
          agent.Communicator.Send(response);
        }
      }
      Stop();
    }
  }
}
