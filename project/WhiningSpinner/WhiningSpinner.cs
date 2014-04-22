using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
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
    #region Private
    public ConcurrentQueue<WhiningTwine> twine = new ConcurrentQueue<WhiningTwine>();
    #endregion

    #region Public
    public bool TwineAvailable()
    {
      return twine.Count != 0;
    }
    public WhiningTwine getTwine()
    {
      if (TwineAvailable())
      {
        WhiningTwine twine = null;
        while (TwineAvailable() && !this.twine.TryDequeue(out twine)) ;
        return twine;
      }
      else
      {
        return null;
      }
    }
    public void addTwine(WhiningTwine twine)
    {
      this.twine.Enqueue(twine);
      //if (this.resourceCountEvent != null) resourceCountEvent(getTwineCount());
    }
    public int getTwineCount()
    {
      return this.twine.Count;
    }
    #endregion

    public WhiningSpinner(int port = -1)
      : base(port)
    {
      brain = new WhiningBrain(this);
      state = new AgentState(AgentInfo.PossibleAgentType.WhiningSpinner);

      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.GetResource + 1000 * (int)GetResource.PossibleResourceType.WhiningTwine, typeof(StrategyGetWhiningTwine));
    }
  }
}
