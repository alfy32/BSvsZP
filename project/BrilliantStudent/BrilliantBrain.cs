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
  class BrilliantBrain : AgentBrain
  {
    public BrilliantBrain(Agent agent) : base(agent) { }
    protected override void Think()
    {
      //Check for stuff and update
      StatusMonitor statusMonitor = StatusMonitor.get();

      statusMonitor.post("I'm thinking...");

      statusMonitor.post("I'm going to get the configuration...");
      GetResource getResource = new GetResource(agent.State.getAgentInfo().Id, GetResource.PossibleResourceType.GameConfiguration);

      Envelope envelope = new Envelope(getResource, agent.State.GameEndPoint);
      ExecutionStrategy.StartConversation(envelope, agent);

      System.Threading.Thread.Sleep(5000);
    }
  }
}
