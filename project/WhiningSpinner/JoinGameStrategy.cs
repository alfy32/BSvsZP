using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentCommon;
using Messages;
using Common;

namespace WhiningSpinner
{
  class JoinGameStrategy : ExecutionStrategy
  {
    Agent agent;

    public JoinGameStrategy(int conversationId, Agent agent)
      : base(conversationId) 
    {
      this.agent = agent;
    }

    protected override void Execute()
    {
      if(messageQueue.hasItems())
      {
        Envelope envelope = messageQueue.pop();

        JoinGame joinGame = (JoinGame)envelope.message;

        agent.Communicator.Send(envelope);

        while (!messageQueue.hasItems())
        {
          System.Threading.Thread.Sleep(100);
        }

        envelope = messageQueue.pop();
        AckNak ackNak = (AckNak)envelope.message;

        if (ackNak.Status == Reply.PossibleStatus.Success)
        {
          Console.WriteLine("Successfully joined a game...");

          AgentInfo resultAgentInfo = (AgentInfo)ackNak.ObjResult;
          Console.WriteLine(" Status: " + resultAgentInfo.AgentStatus);
          Console.WriteLine(" Location: " + resultAgentInfo.Location);
          Console.WriteLine(" Strength: " + resultAgentInfo.Strength);
          Console.WriteLine();

          agent.Info = resultAgentInfo;
        }
        else
        {
          Console.WriteLine("Failed to join the game...");
          Console.WriteLine(ackNak.Message);
        }

        Stop();
      }
    }
  }
}
