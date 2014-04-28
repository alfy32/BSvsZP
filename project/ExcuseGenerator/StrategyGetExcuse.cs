using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentCommon;
using Messages;
using Common;

namespace ExcuseGenerator
{
  public class StrategyGetExcuse : ExecutionStrategy
  {
    public StrategyGetExcuse(Agent agent)
      : base(agent) { }

    public override void Execute(Object startEnvelope)
    {
      Envelope envelope = (Envelope)startEnvelope;
      if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.GetResource)
      {
        GetResource getResource = (GetResource)envelope.message;
        if (getResource.GetResourceType != GetResource.PossibleResourceType.Excuse)
        {
          StatusMonitor.get().postStatus("Someone asked for a resource that is not an excuse.");
          ResourceReply failedResourceReply = new ResourceReply(Reply.PossibleStatus.Failure, null, "I only have excuses!");
          failedResourceReply.ConversationId = envelope.message.ConversationId;
          agent.Communicator.Send(new Envelope(failedResourceReply, envelope.endPoint));
          return;
        }

        ExcuseGenerator generator = (ExcuseGenerator)agent;
        ResourceReply resourceReply = null;

        if (getResource.EnablingTick != null && generator.ExcuseAvailable())
        {
          Excuse excuse = generator.getExcuse();
          excuse.RequestTick = getResource.EnablingTick;
          resourceReply = new ResourceReply(Reply.PossibleStatus.Success, excuse);
          StatusMonitor.get().postStatus("Sending excuse to " + envelope.endPoint);
        }
        else if (getResource.EnablingTick == null)
        {
          StatusMonitor.get().postStatus("Agent at " + envelope.endPoint + " didn't give me a tick!");
          resourceReply = new ResourceReply(Reply.PossibleStatus.Failure, null, "Your enabling tick was null. Bad Agent!");
        }
        else
        {
          StatusMonitor.get().postStatus("Agent at " + envelope.endPoint + " asked for an excuse but I don't have one");
          resourceReply = new ResourceReply(Reply.PossibleStatus.Failure, null, "No excuses available.");
        }

        resourceReply.ConversationId = envelope.message.ConversationId;
        Envelope response = new Envelope(resourceReply, envelope.endPoint);
        agent.Communicator.Send(response);
      }
    }
  }
}
