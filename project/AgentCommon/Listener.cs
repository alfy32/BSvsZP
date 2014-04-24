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
            MessageNumber conversationId = envelope.message.ConversationId;

            StatusMonitor statusMonitor = StatusMonitor.get();
            statusMonitor.postDebug("Listener recieved a message: " + envelope.message.GetType().ToString());

            if (messageNr == conversationId.SeqNumber)
            {
              //place on request message queue
              RequestMessageQueue.getQueue().push(envelope);
            }
            else if (ConversationMessageQueues.hasQueue(conversationId))
            {
              //place on conversation message queue
              ConversationMessageQueues.getQueue(conversationId).push(envelope);
            }
            else
            {
              //ignore if ther is no conversation queue
              statusMonitor.postDebug("Listener ignored a message: " + envelope.message.GetType().ToString());
            }
          }
          System.Threading.Thread.Sleep(1);
        }
      }
    }
  }
}
