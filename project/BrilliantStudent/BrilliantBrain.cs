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
      StatusMonitor.get().post("I'm thinking...");

      if (agent.State.GameConfiguration == null)
      {
        getResource(GetResource.PossibleResourceType.GameConfiguration);
        System.Threading.Thread.Sleep(500);
      }
      if (agent.State.PlayingFieldLayout == null)
      {
        getResource(GetResource.PossibleResourceType.PlayingFieldLayout);
        System.Threading.Thread.Sleep(500);
      }
      if (agent.State.BrilliantStudentList == null)
      {
        getResource(GetResource.PossibleResourceType.BrillianStudentList);
        System.Threading.Thread.Sleep(500);
      }
      if (agent.State.ExcuseGeneratorList == null)
      {
        getResource(GetResource.PossibleResourceType.ExcuseGeneratorList);
        System.Threading.Thread.Sleep(500);
      }
      if (agent.State.WhiningSpinnerList == null)
      {
        getResource(GetResource.PossibleResourceType.WhiningSpinnerList);
        System.Threading.Thread.Sleep(500);
      }
      if (agent.State.ZombieProfessorList == null)
      {
        getResource(GetResource.PossibleResourceType.ZombieProfessorList);
        System.Threading.Thread.Sleep(500);
      }



      System.Threading.Thread.Sleep(5000);
    }
  }
}
