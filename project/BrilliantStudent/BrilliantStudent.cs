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
  public class BrilliantStudent : Agent
  {
    public BrilliantStudent(int port = -1)
      : base(port)
    {
      brain = new BrilliantBrain(this);
      state = new AgentState(AgentInfo.PossibleAgentType.BrilliantStudent);

      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.GetResource + 1000 * (int)GetResource.PossibleResourceType.Excuse, typeof(StrategyGetExcuse));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.GetResource + 1000 * (int)GetResource.PossibleResourceType.WhiningTwine, typeof(StrategyGetWhiningTwine));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.Move, typeof(StrategyMove));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.ThrowBomb, typeof(StrategyThrowBomb));
    }
  }
}
