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

    public StrategyEndGame(Agent agent)
      : base(agent) { }

    public override void Execute(Object startEnvelope)
    {
      Envelope envelope = (Envelope)startEnvelope;
      StatusMonitor statusMonitor = StatusMonitor.get();
      if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.EndGame)
      {
        EndGame endGame = (EndGame)envelope.message;
        statusMonitor.postStatus("The game has ended");
        statusMonitor.postStatus("Winners:");

        foreach (AgentInfo agentInfo in endGame.Winners)
        {
          statusMonitor.postStatus(agentInfo.Id + " " + agentInfo.FirstName + " " + agentInfo.LastName);
        }

        StatusMonitor.get().postDebug("Shutting Down...");
        Environment.Exit(0);
      }
    }
  }
}
