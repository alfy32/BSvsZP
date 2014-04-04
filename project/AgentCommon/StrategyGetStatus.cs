using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;
using Common;

namespace AgentCommon
{
  public class StrategyGetStatus : ExecutionStrategy
  {
    Agent agent;

    public StrategyGetStatus(int conversationId, Agent agent)
      : base(conversationId)
    {
      this.agent = agent;
    }

    protected override void Execute()
    {
      if (messageQueue.hasItems())
      {
        Envelope recieved = messageQueue.pop();
        if (recieved.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.GetStatus)
        {
          GetStatus getStatus = (GetStatus)recieved.message;
          StatusMonitor.get().post("Recieved GetStatus message.");

          StatusReply statusReply = new StatusReply(Reply.PossibleStatus.Success, agent.State.getAgentInfo());
          Envelope response = new Envelope(statusReply, recieved.endPoint);
        }
        Stop();
      }
    }
  }
}
