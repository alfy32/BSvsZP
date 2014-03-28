using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentCommon
{
    public class RequestMessageQueue
    {
        private static MessageQueue messageQueue = null;

        public static MessageQueue getQueue() {
            if (messageQueue == null)
            {
                messageQueue = new MessageQueue();
            }

            return messageQueue;
        }
    }
}
