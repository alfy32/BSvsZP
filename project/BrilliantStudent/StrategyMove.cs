using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentCommon;
using Messages;
using Common;

namespace BrilliantStudent
{
  public class StrategyMove : ExecutionStrategy
  {
    Agent agent;

    public StrategyMove(int conversationId, Agent agent)
      : base(conversationId)
    {
      this.agent = agent;
    }

    protected override void Execute()
    {
      if (messageQueue.hasItems())
      {
        Envelope envelope = messageQueue.pop();
        if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.Move)
        {
          agent.Communicator.Send(envelope);
          StatusMonitor.get().postDebug("Sent Move message.");

          while (!messageQueue.hasItems())
            System.Threading.Thread.Sleep(10);

          Envelope response = messageQueue.pop();
          if (response.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AckNak)
          {
            AckNak ackNak = (AckNak)response.message;
            StatusMonitor.get().postDebug("Recieved move response Message.");

            if (ackNak.Status == Reply.PossibleStatus.Success)
            {
              StatusMonitor.get().postDebug("Agent Moved Successfully.");
              agent.State.AgentInfo = (AgentInfo)ackNak.ObjResult;
            }
            else
            {
              StatusMonitor.get().postDebug("Agent Couldn't Move. Error: " + ackNak.Message);
            }
          }
        }
        Stop();
      }
    }
  }
}
