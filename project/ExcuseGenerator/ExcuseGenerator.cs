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
    #region Private
    private ConcurrentQueue<Excuse> excuses = new ConcurrentQueue<Excuse>();
    #endregion

    #region Public
    public bool ExcuseAvailable()
    {
      return excuses.Count != 0;
    }
    public Excuse getExcuse()
    {
      if (ExcuseAvailable())
      {
        Excuse excuse = null;
        while (ExcuseAvailable() && !excuses.TryDequeue(out excuse)) ;
        return excuse;
      } 
      else
      {
        return null;
      }
    }
    public void addExcuse(Excuse excuse)
    {
      excuses.Enqueue(excuse);
      //if (resourceCountEvent != null) resourceCountEvent(getExcuseCount());
    }
    public int getExcuseCount()
    {
      return excuses.Count;
    }
    #endregion

    #region Constructor
    public ExcuseGenerator(int port = -1)
      : base(port)
    {
      brain = new ExcuseBrain(this);
      state = new AgentState(AgentInfo.PossibleAgentType.ExcuseGenerator);

      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.GetResource + 1000 * (int)GetResource.PossibleResourceType.Excuse, typeof(StrategyGetExcuse));
    }
    #endregion  
  }
}
