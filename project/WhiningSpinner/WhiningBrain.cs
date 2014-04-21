using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentCommon;

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

      System.Threading.Thread.Sleep(2000);
    }
  }
}
