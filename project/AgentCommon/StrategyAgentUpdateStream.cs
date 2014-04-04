using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;
using Common;

namespace AgentCommon
{
  public class StrategyAgentUpdateStream : ExecutionStrategy
  {
    Agent agent;
    bool StreamStarted = false;

    public StrategyAgentUpdateStream(int conversationId, Agent agent)
      : base(conversationId)
    {
      this.agent = agent;
    }

    protected override void Execute()
    {
      if (messageQueue.hasItems())
      {
        Envelope envelope = messageQueue.pop();
        if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AckNak) // change this to StartUpdateStream
        {
          agent.Communicator.Send(envelope);
          StatusMonitor.get().post("Sent Start Update Stream");

          while (!messageQueue.hasItems())
            System.Threading.Thread.Sleep(10);

          Envelope recieved = messageQueue.pop();
          if (recieved.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AckNak)
          {
            AckNak ackNak = (AckNak)recieved.message;
            if (ackNak.Status == Reply.PossibleStatus.Success)
            {
              StatusMonitor.get().post("Update Stream Started");
              StreamStarted = true;
            }
            else
            {
              StatusMonitor.get().post("Update Stream Failed to start");
            }
          }
        }
        else if (StreamStarted && envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AgentListReply)
        {
          AgentListReply agentListReply = (AgentListReply)envelope.message;
          StatusMonitor.get().post("Recieved update from Update Stream");
          // use the agent list
        }
        else if (StreamStarted && envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AckNak) // change this to StopUpdateStream
        {
          agent.Communicator.Send(envelope);
          StatusMonitor.get().post("Sent Stop Update Stream");

          Stop();
        }        
      }
    }
  }
}
