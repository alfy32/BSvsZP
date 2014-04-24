using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;
using Common;

namespace AgentCommon
{
  public class StrategyCollaborate : ExecutionStrategy
  {
    public StrategyCollaborate(Agent agent)
      : base(agent) { }

    public override void Execute(Object startEnvelope)
    {
      Envelope envelope = (Envelope)startEnvelope;
      if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.Collaborate)
      {
        Collaborate collaborate = (Collaborate)envelope.message;

        // sending message
        if (envelope.endPoint != agent.State.AgentInfo.CommunicationEndPoint)
        {
          MessageQueue messageQueue = ConversationMessageQueues.getQueue(envelope.message.ConversationId);
          agent.Communicator.Send(envelope);
          StatusMonitor.get().postDebug("Sent Collaboration message.");

          while (!messageQueue.hasItems())
            System.Threading.Thread.Sleep(10);

          Envelope response = messageQueue.pop();
          if (response.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AckNak)
          {
            AckNak ackNack = (AckNak)response.message;
            if (ackNack.Status == Reply.PossibleStatus.Success)
            {
              AgentInfo info = (AgentInfo)ackNack.ObjResult;
              StatusMonitor.get().postDebug("Recieved Collaboration ackNak message.");
              // do something with this
            }
          }
        }
        else // recieved collaboration message
        {
          // do the collaboration
          AgentInfo agentInfo = agent.State.AgentList.FindClosestToLocation(agent.State.AgentInfo.Location, AgentInfo.PossibleAgentType.ZombieProfessor);

          AckNak ackNack = new AckNak(Reply.PossibleStatus.Success, agentInfo);
          ackNack.ConversationId = envelope.message.ConversationId;
          Envelope response = new Envelope(ackNack, envelope.endPoint);
          agent.Communicator.Send(response);
          StatusMonitor.get().postDebug("Did Collaboration sending ackNak message.");
        }
      }
    }
  }
}
