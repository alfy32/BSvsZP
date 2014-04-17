using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;
using Common;

namespace AgentCommon
{
  public class StrategyChangeStrength : ExecutionStrategy
  {
    Agent agent;

    public StrategyChangeStrength(int conversationId, Agent agent)
      : base(conversationId)
    {
      this.agent = agent;
    }

    protected override void Execute()
    {
      if (messageQueue.hasItems())
      {
        Envelope recieved = messageQueue.pop();
        if (recieved.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.ChangeStrength)
        {
          ChangeStrength changeStrength = (ChangeStrength)recieved.message;
          StatusMonitor.get().postDebug("Recieved ChangeStrength message.");

          //TODO update agent strength
          Reply.PossibleStatus status = Reply.PossibleStatus.Success;
          ComponentInfo info = new ComponentInfo();

          if (status == Reply.PossibleStatus.Success)
          {
            AckNak ackNak = new AckNak(status, info);
            agent.Communicator.Send(new Envelope(ackNak, recieved.endPoint));
            StatusMonitor.get().postDebug("Sent Acknowledged ChangeStrength message.");
          }
          else
          {
            AckNak ackNak = new AckNak(status, null);
            ackNak.Message = "Failed to change strength";
            agent.Communicator.Send(new Envelope(ackNak, recieved.endPoint));
            StatusMonitor.get().postDebug("Didn't want to ChangeStrength.");
          }
        }
        Stop();
      }
    }
  }
}
