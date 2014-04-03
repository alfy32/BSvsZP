using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentCommon;

namespace BrilliantStudent
{
  class StartGameStrategy : ExecutionStrategy
  {
    Agent agent;

    public StartGameStrategy(int conversationId, Agent agent)
      : base(conversationId) 
    {
      this.agent = agent;
    }

    protected override void Execute()
    {
      if (messageQueue.hasItems())
      {
        // do converstation
      }
    }
  }
}
