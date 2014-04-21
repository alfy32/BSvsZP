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
      GetResource getResource = new GetResource(agent.State.AgentInfo.Id, GetResource.PossibleResourceType.Excuse);
      Envelope envelope = new Envelope(getResource, endPoint);
      ExecutionStrategy.StartConversation(envelope, agent);
    }

    public void getWhine(EndPoint endPoint)
    {
      GetResource getResource = new GetResource(agent.State.AgentInfo.Id, GetResource.PossibleResourceType.WhiningTwine);
      Envelope envelope = new Envelope(getResource, endPoint);
      ExecutionStrategy.StartConversation(envelope, agent);
    }
    public void gotExcuse(Excuse excuse)
    {
      StatusMonitor.get().postDebug("I got an excuse. I don't know what to do with it.");
    }
    public void gotTwine(WhiningTwine twine)
    {
      StatusMonitor.get().postDebug("I got whining twine. I don't know what to do with it.");
    }
    #endregion
  }
}
