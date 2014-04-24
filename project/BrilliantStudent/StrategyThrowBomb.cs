using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentCommon;
using Messages;
using Common;

namespace BrilliantStudent
{
  public class StrategyThrowBomb : ExecutionStrategy
  {
    public StrategyThrowBomb(Agent agent)
      : base(agent) { }

    public override void Execute(Object startEnvelope)
    {
      Envelope envelope = (Envelope)startEnvelope;
      MessageQueue messageQueue = ConversationMessageQueues.getQueue(envelope.message.ConversationId);
      if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.ThrowBomb)
      {
        agent.Communicator.Send(envelope);
        StatusMonitor.get().postDebug("Sent ThrowBomb message.");

        while (!messageQueue.hasItems())
          System.Threading.Thread.Sleep(10);

        Envelope response = messageQueue.pop();
        if (response.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AckNak)
        {
          AckNak ackNak = (AckNak)response.message;
          if (ackNak.Status == Reply.PossibleStatus.Success)
          {
            StatusMonitor.get().postDebug("Successfully Threw Bomb.");

            // TODO: update something
          }
          else
          {
            StatusMonitor.get().postDebug("Couldn't Throw Bomb. Error: " + ackNak.Message);
          }
        }
      }
    }
  }
}
