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
        if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.StartUpdateStream)
        {
          agent.Communicator.Send(envelope);
          StatusMonitor.get().postDebug("Sent Start Update Stream");

          while (!messageQueue.hasItems())
            System.Threading.Thread.Sleep(10);

          Envelope recieved = messageQueue.pop();
          if (recieved.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AckNak)
          {
            AckNak ackNak = (AckNak)recieved.message;
            if (ackNak.Status == Reply.PossibleStatus.Success)
            {
              StatusMonitor.get().postDebug("Update Stream Started");
              StreamStarted = true;
            }
            else
            {
              StatusMonitor.get().postDebug("Update Stream Failed to start");
            }
          }
        }
        else if (StreamStarted && envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AgentListReply)
        {
          AgentListReply agentListReply = (AgentListReply)envelope.message;
          StatusMonitor.get().postDebug("Recieved update from Update Stream");
          agent.State.AgentList.Update(agentListReply.Agents);
          agent.State.AgentList = agent.State.AgentList;
        }
        else if (StreamStarted && envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AckNak) // change this to StopUpdateStream
        {
          agent.Communicator.Send(envelope);
          StatusMonitor.get().postDebug("Sent Stop Update Stream");

          Stop();
        }        
      }
    }
  }
}
