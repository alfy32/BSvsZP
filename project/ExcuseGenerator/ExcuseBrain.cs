using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentCommon;
using Messages;
using Common;

namespace ExcuseGenerator
{
  public class ExcuseBrain : AgentBrain
  {
    public ExcuseBrain(Agent agent) : base(agent) { }
    protected override void Think()
    {
      // do some excuse stuff
      StatusMonitor statusMonitor = StatusMonitor.get();

      statusMonitor.postDebug("I'm thinking...");

      if (agent.State != null && agent.State.GameConfiguration != null)
      {
        if (agent.getTickCount() < agent.State.GameConfiguration.NumberOfTicksRequiredToBuildAnExcuse)
        {
          statusMonitor.postDebug("I don't have enough ticks to build an excuse. Count:" + agent.getTickCount());
        }
        else
        {
          statusMonitor.postDebug("Building an excuse...");

          List<Tick> ticks = new List<Tick>();
          for (int i = 0; i < agent.State.GameConfiguration.NumberOfTicksRequiredToBuildAnExcuse; ++i)
          {
            ticks.Add(agent.getTickFromStash());
          }
          Excuse excuse = new Excuse(agent.State.AgentInfo.Id, ticks, null);
          ((ExcuseGenerator)agent).addExcuse(excuse);

          statusMonitor.postDebug("Number of excuses: " + ((ExcuseGenerator)agent).getExcuseCount());
        }

        System.Threading.Thread.Sleep(agent.State.GameConfiguration.TickInterval * 10);
      }
      else
      {
        statusMonitor.postDebug("no configuration...");
        System.Threading.Thread.Sleep(2000);
      }
    }
  }
}
