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
    protected override void Execute()
    {
      if (messageQueue.hasItems())
      {
        Envelope envelope = messageQueue.pop();
        if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.GetResource)
        {
          StatusMonitor.get().postDebug("Sent Get WhiningSpinnerList Message.");
          agent.Communicator.Send(envelope);

          while (!messageQueue.hasItems())
            System.Threading.Thread.Sleep(10);

          Envelope response = messageQueue.pop();

          if (response.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AgentListReply)
          {
            AgentListReply reply = (AgentListReply)response.message;
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
        Stop();
      }
    }
  }
}
