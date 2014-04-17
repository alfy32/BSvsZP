using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentCommon;

namespace ExcuseGenerator
{
  class ExcuseBrain : AgentBrain
  {
    public ExcuseBrain(Agent agent) : base(agent) { }
    protected override void Think()
    {
      // do some excuse stuff
      StatusMonitor statusMonitor = StatusMonitor.get();

      statusMonitor.postDebug("I'm thinking...");

      System.Threading.Thread.Sleep(2000);
    }
  }
}
