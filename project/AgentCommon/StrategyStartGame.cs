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
          StatusMonitor.get().postDebug("Recieved StartGame message.");

          ReadyReply ready = new ReadyReply(Reply.PossibleStatus.Success);
          ready.ConversationId = startGame.ConversationId;
          agent.Communicator.Send(new Envelope(ready, recieved.endPoint));
          StatusMonitor.get().postDebug("Sent Ready message.");

          while (!messageQueue.hasItems())
            System.Threading.Thread.Sleep(1);

          Envelope response = messageQueue.pop();
          if (response.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AckNak)
          {
            StatusMonitor.get().postDebug("Recieved Proceed Message.");
            agent.State.GameEndPoint = response.endPoint;
            
            AgentInfo agentInfo = agent.State.AgentInfo;
            agentInfo.AgentStatus = AgentInfo.PossibleAgentStatus.InGame;
            
            agent.State.AgentInfo = agentInfo;

            agent.Brain.Start();
          }
        }
        Stop();
      }
    }
  }
}
