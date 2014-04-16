using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentCommon;

namespace AgentCommon
{
  public abstract class AgentBrain : BackgroundThread
  {
    protected Agent agent;

    public AgentBrain(Agent agent)
    {
      this.agent = agent;
    }

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
  }
}
