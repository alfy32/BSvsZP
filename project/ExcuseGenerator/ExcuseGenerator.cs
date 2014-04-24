using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
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
    #region Constructor
    public ExcuseGenerator(int port = -1)
      : base(port)
    {
      brain = new ExcuseBrain(this);
      state = new AgentState(AgentInfo.PossibleAgentType.ExcuseGenerator);

      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.GetResource + 1000 * (int)GetResource.PossibleResourceType.Excuse, new StrategyGetExcuse(this));
    }
    #endregion
  }
}
