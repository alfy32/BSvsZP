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

    private AgentList agentList = new AgentList();
    #endregion

    #region Delegates and Events
    public delegate void AgentInfoMethod(AgentInfo param);
    public delegate void AgentListMethod(AgentList param);
    public delegate void IntMethod(int param);
    public delegate void StringMethod(string param);

    public event AgentInfoMethod updateAgentInfoEvent;
    public event AgentListMethod updateAgentListEvent;
    #endregion

    #region Constructors
    public AgentState(AgentInfo.PossibleAgentType agentType)
    {
      agentInfo = new AgentInfo();
      agentInfo.AgentType = agentType;
      agentInfo.ANumber = "A01072246";
      agentInfo.FirstName = "Alan";
      agentInfo.LastName = "Christensen";
      agentInfo.Id = MessageNumber.LocalProcessId;

      GameConfiguration = null;
      PlayingFieldLayout = null;
      this.agentList = new AgentList();
    }
    #endregion

    #region Public Members
    public EndPoint GameEndPoint { get; set; }
    public GameConfiguration GameConfiguration { get; set; }
    public PlayingFieldLayout PlayingFieldLayout { get; set; }
    public AgentList AgentList 
    { 
      get 
      { 
        return this.agentList; 
      } 
      set 
      { 
        this.agentList = value; 
        if (updateAgentListEvent != null) updateAgentListEvent(value); 
      } 
    }
    public AgentInfo AgentInfo
    {
      get { return agentInfo; }
      set
      {
        agentInfo = value;
        if (updateAgentInfoEvent != null) updateAgentInfoEvent(agentInfo);
      }
    }
    #endregion

    #region Public Functions

    #endregion
  }
}
