using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;
using Common;

namespace AgentCommon
{
  public class StrategyGetResource : ExecutionStrategy
  {
    private Agent agent;

    public StrategyGetResource(int conversationId, Agent agent)
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
          messageQueue.push(envelope);

          GetResource getResource = (GetResource)envelope.message;

          int messageId = (int)envelope.message.MessageTypeId() + 1000* (int)getResource.GetResourceType;
          int conversationId = envelope.message.ConversationId.SeqNumber;

          ExecutionStrategy executionStrategy = (ExecutionStrategy)Activator.CreateInstance(StrategyPool[messageId], conversationId, agent);
          if (!executionStrategy.IsRunning)
          {
            executionStrategy.Start();
          }
          else
          {
            executionStrategy.Resume();
          }
        }
        Stop();
      }
    }
  }
}
