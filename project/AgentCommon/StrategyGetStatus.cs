using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;
using Common;

namespace AgentCommon
{
  public class StrategyGetStatus : ExecutionStrategy
  {
    public StrategyGetStatus(Agent agent)
      : base(agent) { }

    public override void Execute(Object startEnvelope)
    {
      Envelope recieved = (Envelope)startEnvelope;
      if (recieved.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.GetStatus)
      {
        GetStatus getStatus = (GetStatus)recieved.message;
        StatusMonitor.get().postDebug("Recieved GetStatus message.");

        StatusReply statusReply = new StatusReply(Reply.PossibleStatus.Success, agent.State.AgentInfo);
        Envelope response = new Envelope(statusReply, recieved.endPoint);
        agent.Communicator.Send(response);
      }
    }
  }
}
