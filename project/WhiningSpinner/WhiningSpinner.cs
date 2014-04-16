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
  public class WhiningSpinner : Agent
  {
    public WhiningSpinner(int port = -1)
      : base(port)
    {
      brain = new WhiningBrain(this);
      state = new AgentState(AgentInfo.PossibleAgentType.WhiningSpinner);
    }
  }
}
