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
    bool StreamStarted = false;

    public StrategyAgentUpdateStream(Agent agent)
      : base(agent) { }

    public void recieveUpdates(MessageQueue messageQueue) {
      while (!messageQueue.hasItems())
        System.Threading.Thread.Sleep(1);

      Envelope response = messageQueue.pop();
      if (response.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AgentListReply)
      {
        AgentListReply agentListReply = (AgentListReply)response.message;
        StatusMonitor.get().postDebug("Recieved update from Update Stream");
        AgentList agentList = agentListReply.Agents;
        agentList.Update(agent.State.AgentList);
        agent.State.AgentList = agentList;
      }
    }
    public override void Execute(Object startEnvelope)
    {
      Envelope envelope = (Envelope)startEnvelope;
      MessageQueue messageQueue = ConversationMessageQueues.getQueue(envelope.message.ConversationId);

      if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.StartUpdateStream)
      {
        agent.Communicator.Send(envelope);
        StatusMonitor.get().postDebug("Sent Start Update Stream");

        while (!messageQueue.hasItems())
          System.Threading.Thread.Sleep(1);

        Envelope ackEnvelope = messageQueue.pop();
        if (ackEnvelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AckNak)
        {
          AckNak ackNak = (AckNak)ackEnvelope.message;
          if (ackNak.Status == Reply.PossibleStatus.Success)
          {
            StatusMonitor.get().postDebug("Update Stream Started");
            StreamStarted = true;

            while (StreamStarted) recieveUpdates(messageQueue);
          }
          else
          {
            StatusMonitor.get().postDebug("Update Stream Failed to start");
          }
        }
      }
      else if (StreamStarted && envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.EndUpdateStream)
      {
        agent.Communicator.Send(envelope);
        StatusMonitor.get().postDebug("Sent Stop Update Stream");
      }
    }
  }
}

