using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;
using Common;

namespace AgentCommon
{
  public class StrategyEndGame : ExecutionStrategy
  {
    Agent agent;

    public StrategyEndGame(int conversationId, Agent agent)
      : base(conversationId) 
    {
      this.agent = agent;
    }

    protected override void Execute()
    {
      if(messageQueue.hasItems())
      {
        Envelope envelope = messageQueue.pop();
        if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.EndGame)
        {
          StatusMonitor.get().post("The game has ended");
        }
        Stop();
      }
    }
  }
}
