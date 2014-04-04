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
    Agent agent;

    public StrategyCollaborate(int conversationId, Agent agent)
      : base(conversationId)
    {
      this.agent = agent;
    }

    protected override void Execute()
    {
      if (messageQueue.hasItems())
      {
        Envelope envelope = messageQueue.pop();
        if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.Collaborate)
        {
          Collaborate collaborate = (Collaborate)envelope.message;
         
          if(true) // is being sent
          {
            agent.Communicator.Send(envelope);
            StatusMonitor.get().post("Sent Collaboration message.");

            while (!messageQueue.hasItems())
              System.Threading.Thread.Sleep(10);

            Envelope response = messageQueue.pop();
            if (response.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AckNak)
            {
              AckNak ackNack = (AckNak)response.message;
              if (ackNack.Status == Reply.PossibleStatus.Success)
              {
                ComponentInfo info = (ComponentInfo)ackNack.ObjResult;
                StatusMonitor.get().post("Recieved Collaboration ackNak message.");
                // do something with this
              }
            }
          }
          else // is being recieved
          {
            // do the collaboration
            Reply.PossibleStatus status =Reply.PossibleStatus.Success;
            ComponentInfo info = new ComponentInfo();

            AckNak ackNack = new AckNak(status, info);
            Envelope response = new Envelope(ackNack, envelope.endPoint);
            StatusMonitor.get().post("Did Collaboration sending ackNak message.");
            agent.Communicator.Send(response);
          }
        }
        Stop();
      }
    }
  }
}
