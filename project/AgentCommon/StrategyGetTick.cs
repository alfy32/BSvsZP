using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;
using Common;

namespace AgentCommon
{
  public class StrategyGetTick : ExecutionStrategy
  {
    Agent agent;

    public StrategyGetTick(int conversationId, Agent agent)
      : base(conversationId)
    {
      this.agent = agent;
    }

    protected override void Execute()
    {
      if (messageQueue.hasItems())
      {
        Envelope envelope = messageQueue.pop();
        if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.TickDelivery)
        {
          TickDelivery tickMessage = (TickDelivery)envelope.message;
          agent.stashTick(tickMessage.CurrentTick);
        }
        Suspend();
      }
    }
  }
}
