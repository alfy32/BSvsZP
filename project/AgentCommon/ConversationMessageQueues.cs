using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace AgentCommon
{
  public class ConversationMessageQueues
  {
    private static Dictionary<string, MessageQueue> messageQueues = new Dictionary<string, MessageQueue>();

    public static MessageQueue getQueue(MessageNumber conversationId)
    {
      if (messageQueues.ContainsKey(conversationId.ToString()))
      {
        return messageQueues[conversationId.ToString()];
      }
      else
      {
        MessageQueue messageQueue = new MessageQueue();
        messageQueues.Add(conversationId.ToString(), messageQueue);
        return messageQueue;
      }
    }

    public static bool hasQueue(MessageNumber conversationId)
    {
      return messageQueues.ContainsKey(conversationId.ToString());
    }
  }
}
