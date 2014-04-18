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
      StatusMonitor.get().postDebug("I'm thinking...");

      
      System.Threading.Thread.Sleep(5000);
    }
  }
}
