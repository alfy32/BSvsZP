using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Messages;
using Common;

namespace AgentCommon
{
  public class StrategyGetResource : ExecutionStrategy
  {
    public StrategyGetResource(Agent agent)
      : base(agent) { }

    public override void Execute(Object startEnvelope)
    {
      Envelope envelope = (Envelope)startEnvelope;
      if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.GetResource)
      {
        GetResource getResource = (GetResource)envelope.message;

        int messageId = (int)envelope.message.MessageTypeId() + 1000 * (int)getResource.GetResourceType;

        if (StrategyPool.ContainsKey(messageId))
        {
          ExecutionStrategy executionStrategy = StrategyPool[messageId];
          ThreadPool.QueueUserWorkItem(executionStrategy.Execute, envelope);
        }
      }

    }
  }
}
