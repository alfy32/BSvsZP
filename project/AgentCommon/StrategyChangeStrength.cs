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
    public StrategyChangeStrength(Agent agent)
      : base(agent) { }

    public override void Execute(Object startEnvelope)
    {
      Envelope envelope = (Envelope)startEnvelope;

      if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.ChangeStrength)
      {
        ChangeStrength changeStrength = (ChangeStrength)envelope.message;
        StatusMonitor.get().postDebug("Recieved ChangeStrength message.");

        agent.State.AgentInfo.Strength += changeStrength.DeltaValue;
        agent.State.AgentInfo = agent.State.AgentInfo;

        AckNak ackNak = new AckNak(Reply.PossibleStatus.Success, agent.State.AgentInfo);
        agent.Communicator.Send(new Envelope(ackNak, envelope.endPoint));
        StatusMonitor.get().postDebug("Sent ChangeStrength Ack."); 
      }
    }
  }
}
