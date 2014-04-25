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
  public class WhiningBrain : AgentBrain
  {
    public WhiningBrain(Agent agent) : base(agent) { }
    protected override void Think()
    {
      // think about whining
      StatusMonitor statusMonitor = StatusMonitor.get();

      statusMonitor.postDebug("I'm thinking...");

      if (agent.State != null && agent.State.GameConfiguration != null)
      {
        if (agent.getTickCount() < agent.State.GameConfiguration.NumberOfTicksRequiredToBuildAnExcuse)
        {
          statusMonitor.postDebug("I don't have enough ticks to build twine. Count:" + agent.getTickCount());
        }
        else
        {
          statusMonitor.postDebug("Building twine...");

          List<Tick> ticks = new List<Tick>();
          for (int i = 0; i < agent.State.GameConfiguration.NumberOfTicksRequiredToBuildTwine; ++i)
          {
            ticks.Add(agent.getTickFromStash());
          }
          WhiningTwine twine = new WhiningTwine(agent.State.AgentInfo.Id, ticks, null);
          ((WhiningSpinner)agent).addTwine(twine);

          statusMonitor.postDebug("Number twine: " + ((WhiningSpinner)agent).getTwineCount());
        }

        System.Threading.Thread.Sleep(agent.State.GameConfiguration.TickInterval);
      }
      else
      {
        statusMonitor.postDebug("no configuration...");
        System.Threading.Thread.Sleep(2000);
      }
    }
  }
}
