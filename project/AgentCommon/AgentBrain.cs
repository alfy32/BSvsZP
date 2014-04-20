using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentCommon;
using Messages;
using Common;

namespace AgentCommon
{
  public abstract class AgentBrain : BackgroundThread
  {
    #region BackgroundThread Stuff
    public override string ThreadName()
    {
      return "AgentBrain";
    }

    protected override void Process()
    {
      while (keepGoing)
      {
        while (keepGoing && !suspended)
        {
          //This is the AI portion of the brilliantStudent
          Think();

          // This is to slow down the work a little. 
          // The while loops just keep going like crazy without this.
          System.Threading.Thread.Sleep(1);
        }
      }
    }

    protected abstract void Think();
    #endregion

    protected Agent agent;

    public AgentBrain(Agent agent)
    {
      this.agent = agent;
    }

    #region Thoughts
    public void getResource(GetResource.PossibleResourceType resourceType)
    {
      StatusMonitor.get().postDebug("I'm going to get a resource type: " + resourceType.ToString());
      GetResource getResource = new GetResource(agent.State.AgentInfo.Id, resourceType);

      Envelope envelope = new Envelope(getResource, agent.State.GameEndPoint);
      ExecutionStrategy.StartConversation(envelope, agent);
    }

    public void move(FieldLocation toLocation)
    {
      StatusMonitor.get().postDebug("Moving to: " + toLocation.ToString());

      Tick tick = agent.getTickFromStash();
      StatusMonitor.get().postDebug("Is my tick valid: " + tick.IsValid);
      bool valid = tick.IsValid;

      Move move = new Move(tick.ForAgentId, toLocation, tick);
      Envelope envelope = new Envelope(move, agent.State.GameEndPoint);
      ExecutionStrategy.StartConversation(envelope, agent);
    }
    #endregion
  }
}
