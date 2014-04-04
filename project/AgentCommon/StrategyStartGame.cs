using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;
using Common;

namespace AgentCommon
{
  public class StrategyStartGame : ExecutionStrategy
  {
    Agent agent;

    public StrategyStartGame(int conversationId, Agent agent)
      : base(conversationId)
    {
      this.agent = agent;
    }

    protected override void Execute()
    {
      if (messageQueue.hasItems())
      {
        Envelope recieved = messageQueue.pop();
        if (recieved.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.StartGame)
        {
          StartGame startGame = (StartGame)recieved.message;
          StatusMonitor.get().post("Recieved StartGame message.");

          ReadyReply ready = new ReadyReply(Reply.PossibleStatus.Success);
          agent.Communicator.Send(new Envelope(ready, recieved.endPoint));
          StatusMonitor.get().post("Sent Ready message.");

          while (!messageQueue.hasItems())
            System.Threading.Thread.Sleep(10);

          Envelope response = messageQueue.pop();
          //if (response.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.Proceed)
          {
            //  Proceed proceed = (Proceed)response.message;
            StatusMonitor.get().post("Recieved Proceed Message.");

            agent.Brain.Start();
          }
        }
        Stop();
      }
    }
  }
}
