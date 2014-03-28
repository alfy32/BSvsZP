using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentCommon
{
    class ConversationMessageQueues
    {
        private static Dictionary<int, MessageQueue> messageQueues = new Dictionary<int, MessageQueue>();

        public static MessageQueue getQueue(int conversationId)
        {
            if (messageQueues.ContainsKey(conversationId))
            {
                return messageQueues[conversationId];
            }
            else
            {
                MessageQueue messageQueue = new MessageQueue();
                messageQueues.Add(conversationId, messageQueue);
                return messageQueue;
            }
        }

        public static bool hasQueue(int conversationId)
        {
            return messageQueues.ContainsKey(conversationId);
        }
    }
}
