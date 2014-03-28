using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;

namespace AgentCommon
{
    public class Doer : BackgroundThread
    {
        private Communicator communicator;
      
        public Doer(Communicator communicator)
        {
            this.communicator = communicator;
        }

        public override string ThreadName()
        {
            return "Doer";
        }

        protected override void Process()
        {
            MessageQueue requestMessageQueue = RequestMessageQueue.getQueue();

            while (keepGoing)
            {
                while (keepGoing && !suspended)
                {
                    if(requestMessageQueue.hasItems())
                    {
                        Envelope envelope = requestMessageQueue.pop();

                        ExecutionStrategy.StartConversation(envelope);
                    }
                }
            }
        }
    }
}
