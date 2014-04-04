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
  public class StrategyThrowBomb : ExecutionStrategy
  {
    Agent agent;

    public StrategyThrowBomb(int conversationId, Agent agent)
      : base(conversationId)
    {
      this.agent = agent;
    }

    protected override void Execute()
    {
      if (messageQueue.hasItems())
      {
        Envelope envelope = messageQueue.pop();
        if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.ThrowBomb)
        {
          agent.Communicator.Send(envelope);
          StatusMonitor.get().post("Sent throw bomb message.");

          while (!messageQueue.hasItems())
            System.Threading.Thread.Sleep(10);

          Envelope response = messageQueue.pop();
          if (response.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AckNak)
          {
            AckNak ackNak = (AckNak)response.message;
            StatusMonitor.get().post("Recieved throw bomb response Message.");

            if (ackNak.Status == Reply.PossibleStatus.Success)
            {
              StatusMonitor.get().post("Agent Successfully Threw Bomb.");   
           
              // update 
            }
            else
            {
              StatusMonitor.get().post("Agent Couldn't Throw Bomb. Error: " + ackNak.Message); 
            }
          }
        }
        Stop();
      }
    }
  }
}
