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
        protected static Dictionary<Message.MESSAGE_CLASS_IDS, ExecutionStrategy> StrategyPool = new Dictionary<Message.MESSAGE_CLASS_IDS, ExecutionStrategy>();

        public static void StartConversation(Envelope envelope)
        {
            //TODO: Make this work better.
            Message.MESSAGE_CLASS_IDS messageId = envelope.message.MessageTypeId();

            //If the strategyPool doesn't contain the executionStrategy then 
            // we have not yet implemented it or don't know what to do so 
            // we ignore it.
            if(StrategyPool.ContainsKey(messageId)) 
            {
                int conversationId = envelope.message.ConversationId.SeqNumber;
                MessageQueue messageQueue = ConversationMessageQueues.getQueue(conversationId);
                messageQueue.push(envelope);

                ExecutionStrategy executionStrategy = StrategyPool[messageId];
                executionStrategy.Resume();
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
