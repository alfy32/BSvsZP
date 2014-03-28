using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace AgentCommon
{
    public class Listener : BackgroundThread
    {
        #region Private Data Members
        private Communicator communicator;
        #endregion

        #region Constructors
        public Listener(Communicator communicator)
        {
            this.communicator = communicator;
        }
        #endregion

        public override string ThreadName()
        {
            return "Lstener";
        }

        protected override void Process()
        {
            while (keepGoing)
            {
                while (keepGoing && !suspended)
                {
                    if (communicator.GetAvailable() > 0)
                    {
                        Envelope envelope = communicator.Recieve();
                        int messageNr = envelope.message.MessageNr.SeqNumber;
                        int conversationId = envelope.message.ConversationId.SeqNumber;

                        if (messageNr == conversationId)
                        {
                            //place on request message queue
                            MessageQueue messageQueue = RequestMessageQueue.getQueue();
                            messageQueue.push(envelope);
                        }
                        else if(ConversationMessageQueues.hasQueue(conversationId))
                        {
                            //place on conversation message queue
                            MessageQueue messageQueue = ConversationMessageQueues.getQueue(conversationId);
                            messageQueue.push(envelope);
                        }
                        //ignore if ther is no conversation queue
                    }
                }
            }
        }
    }
}
