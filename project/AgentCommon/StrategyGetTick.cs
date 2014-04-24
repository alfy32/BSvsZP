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
    public StrategyGetTick(Agent agent)
      : base(agent) { }

    public override void Execute(Object startEnvelope)
    {
      Envelope envelope = (Envelope)startEnvelope;
      if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.TickDelivery)
      {
        TickDelivery tickMessage = (TickDelivery)envelope.message;
        agent.stashTick(tickMessage.CurrentTick);
        StatusMonitor.get().postDebug("I got a tick...");
      }
    }
  }
}
