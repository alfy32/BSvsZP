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
  public class BrilliantBrain : AgentBrain
  {
    public BrilliantBrain(Agent agent) : base(agent) { }
    protected override void Think()
    {
      //Check for stuff and update
      StatusMonitor.get().postDebug("I'm thinking...");

      
      System.Threading.Thread.Sleep(5000);
    }

    #region Thoughts
    public void getExcuse(EndPoint endPoint)
    {
      Tick tick = agent.getTickFromStash();
      if (tick == null)
      {
        StatusMonitor.get().postDebug("I don't have any ticks :(");
        return;
      }
      GetResource getResource = new GetResource(agent.State.AgentInfo.Id, GetResource.PossibleResourceType.Excuse);
      getResource.EnablingTick = tick;
      Envelope envelope = new Envelope(getResource, endPoint);
      ExecutionStrategy.StartConversation(envelope);
    }

    public void getWhine(EndPoint endPoint)
    {
      Tick tick = agent.getTickFromStash();
      if (tick == null)
      {
        StatusMonitor.get().postDebug("I don't have any ticks :(");
        return;
      }
      GetResource getResource = new GetResource(agent.State.AgentInfo.Id, GetResource.PossibleResourceType.WhiningTwine);
      getResource.EnablingTick = tick;
      Envelope envelope = new Envelope(getResource, endPoint);
      ExecutionStrategy.StartConversation(envelope);
    }
    public void gotExcuse(Excuse excuse)
    {
      StatusMonitor.get().postDebug("I got an excuse. I don't know what to do with it.");
      agent.addExcuse(excuse);
    }
    public void gotTwine(WhiningTwine twine)
    {
      StatusMonitor.get().postDebug("I got whining twine. I don't know what to do with it.");
      agent.addTwine(twine);
    }
    #endregion
  }
}
