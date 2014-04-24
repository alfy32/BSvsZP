using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;
using Common;

namespace AgentCommon
{
  public class StrategyExitGame : ExecutionStrategy
  {

    public StrategyExitGame(Agent agent)
      : base(agent) { }

    public override void Execute(Object startEnvelope)
    {
      Envelope envelope = (Envelope)startEnvelope;
      MessageQueue messageQueue = ConversationMessageQueues.getQueue(envelope.message.ConversationId);
      if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.ExitGame)
      {
        agent.Communicator.Send(envelope);
        StatusMonitor.get().postStatus("Sent ExitGame.");

        while (!messageQueue.hasItems())
          System.Threading.Thread.Sleep(1);

        Envelope response = messageQueue.pop();
        if (response.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AckNak)
        {
          AckNak ackNack = (AckNak)response.message;
          StatusMonitor.get().postStatus("ExitGame Response: " + ackNack.Status);
          if (ackNack.Status == Reply.PossibleStatus.Failure)
            StatusMonitor.get().postStatus(ackNack.Message);
        }
      }
    }
  }
}
