using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;

namespace AgentCommon
{
  public abstract class ExecutionStrategy : BackgroundThread
  {
    #region Static Members
    protected static Dictionary<int, Type> StrategyPool = new Dictionary<int, Type>();

    public static void addStrategy(int messageId, Type strategy)
    {
      StrategyPool.Add(messageId, strategy);
    }
    public static void StartConversation(Envelope envelope, Agent agent)
    {
      //TODO: Make this work better.
      int messageId = (int)envelope.message.MessageTypeId();
      int conversationId = envelope.message.ConversationId.SeqNumber;

      //If the strategyPool doesn't contain the executionStrategy then 
      // we have not yet implemented it or don't know what to do so 
      // we ignore it.
      if (StrategyPool.ContainsKey(messageId))
      {
        ExecutionStrategy executionStrategy = (ExecutionStrategy)Activator.CreateInstance(StrategyPool[messageId], conversationId, agent);
        executionStrategy.Start();
        executionStrategy.messageQueue.push(envelope);
      }

    }
    #endregion

    protected MessageQueue messageQueue;

    public ExecutionStrategy(int conversationId)
    {
      messageQueue = ConversationMessageQueues.getQueue(conversationId);
    }

    public override String ThreadName()
    {
      return "ExecutionStrategy";
    }

    protected override void Process()
    {
      while (keepGoing)
      {
        while (keepGoing && !suspended)
        {
          Execute();
        }
      }
    }

    protected abstract void Execute();
  }
}

// Dictionary of classes to instantiate example

//Dictionary<string, Type> d = new Dictionary<string, Type>();

//d.Add("Bomb", typeof(JoinGameStrategy));

//ExecutionStrategy b = (ExecutionStrategy)Activator.CreateInstance(d["Bomb"], 2, new Agent(AgentInfo.PossibleAgentType.BrilliantStudent));