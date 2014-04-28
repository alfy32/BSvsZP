using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;
using Common;

namespace AgentCommon
{
  public class StrategyGetZPList : ExecutionStrategy
  {
    public StrategyGetZPList(Agent agent)
      : base(agent) { }

    public override void Execute(Object startEnvelope)
    {
      Envelope envelope = (Envelope)startEnvelope;
      MessageQueue messageQueue = ConversationMessageQueues.getQueue(envelope.message.ConversationId);
      if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.GetResource)
      {
        agent.Communicator.Send(envelope);
        StatusMonitor.get().postDebug("Sent Get ZombieProfessor List Message.");

        while (!messageQueue.hasItems())
          System.Threading.Thread.Sleep(10);

        Envelope response = messageQueue.pop();
        if (response.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AgentListReply)
        {
          AgentListReply reply = (AgentListReply)response.message;
          if (reply.Status == Reply.PossibleStatus.Success)
          {
            StatusMonitor.get().postDebug("Recieved ZombieProfessor");
            agent.State.AgentList.Update(reply.Agents);
            agent.State.AgentList = agent.State.AgentList;

            foreach (AgentInfo zombie in reply.Agents)
            {
              int index = agent.State.AgentList.FindIndex(zombie.Id);
              agent.State.AgentList[index].Location = zombie.Location;
            }
          }
          else
          {
            StatusMonitor.get().postDebug("Failed to get agentlist");
          }
        }
      }
    }
  }
}
