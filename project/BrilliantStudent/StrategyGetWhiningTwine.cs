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
  public class StrategyGetWhiningTwine : ExecutionStrategy
  {
    private Agent agent;

    public StrategyGetWhiningTwine(int conversationId, Agent agent)
      : base(conversationId)
    {
      this.agent = agent;
    }

    private void sendMessage(Envelope envelope)
    {
      GetResource getResource = (GetResource)envelope.message;
      getResource.GameId = agent.State.AgentInfo.Id;
      getResource.EnablingTick = agent.getTickFromStash();

      StatusMonitor.get().post("Sent Get " + getResource.GetResourceType.ToString() + " Message.");
      agent.Communicator.Send(envelope);
    }

    private void handleResponse(Envelope envelope)
    {
      if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.ResourceReply)
      {
        ResourceReply reply = (ResourceReply)envelope.message;
        if (reply.Status == Reply.PossibleStatus.Success)
        {
          StatusMonitor.get().post("Recieved resource reply");
          WhiningTwine twine = (WhiningTwine)reply.Resource;

          //TODO: save info on agent
        }
        else
        {
          StatusMonitor.get().post("Failed to get resource");
        }
      }
    }
    protected override void Execute()
    {
      if (messageQueue.hasItems())
      {
        Envelope envelope = messageQueue.pop();
        if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.GetResource)
        {
          sendMessage(envelope);

          while (!messageQueue.hasItems())
            System.Threading.Thread.Sleep(10);

          handleResponse(messageQueue.pop());
        }
        Stop();
      }
    }
  }
}
