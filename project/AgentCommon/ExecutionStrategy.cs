using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Messages;

namespace AgentCommon
{
  public abstract class ExecutionStrategy
  {
    #region Static Members
    protected static Dictionary<int, ExecutionStrategy> StrategyPool = new Dictionary<int, ExecutionStrategy>();

    public static void addStrategy(int messageId, ExecutionStrategy strategy)
    {
      StrategyPool.Add(messageId, strategy);
    }
    public static void StartConversation(Envelope envelope)
    {
      int messageId = (int)envelope.message.MessageTypeId();
      int conversationId = envelope.message.ConversationId.SeqNumber;

      //If the strategyPool doesn't contain the executionStrategy then 
      // we have not yet implemented it or don't know what to do so 
      // we ignore it.
      if (StrategyPool.ContainsKey(messageId))
      {
        ExecutionStrategy executionStrategy = StrategyPool[messageId];
        ThreadPool.QueueUserWorkItem(executionStrategy.Execute, envelope);
      }

    }
    #endregion

    protected Agent agent;
    public ExecutionStrategy(Agent agent) { this.agent = agent; }
    public abstract void Execute(Object envelope);
  }
}