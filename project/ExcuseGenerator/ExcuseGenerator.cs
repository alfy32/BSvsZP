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
  public class ExcuseGenerator : Agent
  {
    public ExcuseGenerator(int port = -1)
      : base(port)
    {
      brain = new ExcuseBrain(this);
      state = new AgentState(AgentInfo.PossibleAgentType.ExcuseGenerator);
    }
  }
}
