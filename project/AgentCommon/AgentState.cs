using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace AgentCommon
{
  public class AgentState
  {
    #region Private Members
    private AgentInfo agentInfo;
    #endregion

    #region Delegates and Events
    public delegate void AgentInfoMethod(AgentInfo param);
    public delegate void IntMethod(int param);
    public delegate void StringMethod(string param);

    public event AgentInfoMethod updateAgentInfoEvent;
    #endregion

    #region Constructors
    public AgentState(AgentInfo.PossibleAgentType agentType)
    {
      agentInfo = new AgentInfo();
      agentInfo.AgentType = agentType;
      agentInfo.ANumber = "A01072246";
      agentInfo.FirstName = "Alan";
      agentInfo.LastName = "Christensen";
    }
    #endregion

    #region Public Functions
    public AgentInfo getAgentInfo() { return agentInfo; }
    public void updateAgentInfo(AgentInfo info)
    {
      agentInfo = info;
      if (updateAgentInfoEvent != null) updateAgentInfoEvent(agentInfo);
    }
    #endregion
  }
}
