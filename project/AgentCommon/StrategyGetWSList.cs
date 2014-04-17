using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;
using Common;

namespace AgentCommon
{
  public class StrategyGetWSList : ExecutionStrategy
  {
    private Agent agent;

    public StrategyGetWSList(int conversationId, Agent agent)
      : base(conversationId)
    {
      this.agent = agent;
    }

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
      if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AgentListReply)
      {
        AgentListReply reply = (AgentListReply)envelope.message;
        if (reply.Status == Reply.PossibleStatus.Success)
        {
          StatusMonitor.get().postDebug("Recieved WhiningSpinnerList");
          agent.State.WhiningSpinnerList = reply.Agents;
        }
        else
        {
          StatusMonitor.get().postDebug("Failed to get agentlist");
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
