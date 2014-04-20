﻿using System;
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

    private AgentList brilliantStudentList = new AgentList();
    private AgentList excuseGeneratorList = new AgentList();
    private AgentList whiningSpinnerList = new AgentList();
    private AgentList zombieProfessorList = new AgentList();
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
      BrilliantStudentList = null;
      ExcuseGeneratorList = null;
      WhiningSpinnerList = null;
      ZombieProfessorList = null;
    }
    #endregion

    #region Public Members
    public EndPoint GameEndPoint { get; set; }
    public GameConfiguration GameConfiguration { get; set; }
    public PlayingFieldLayout PlayingFieldLayout { get; set; }
    public AgentList BrilliantStudentList { get { return brilliantStudentList; } set { brilliantStudentList = value; if (updateAgentListEvent != null) updateAgentListEvent(value); } }
    public AgentList ExcuseGeneratorList { get { return excuseGeneratorList; } set { excuseGeneratorList = value; if (updateAgentListEvent != null) updateAgentListEvent(value); } }
    public AgentList WhiningSpinnerList { get { return whiningSpinnerList; } set { whiningSpinnerList = value; if (updateAgentListEvent != null) updateAgentListEvent(value); } }
    public AgentList ZombieProfessorList { get { return zombieProfessorList; } set { zombieProfessorList = value; if (updateAgentListEvent != null) updateAgentListEvent(value); } }
    public AgentInfo AgentInfo { 
      get { return agentInfo; } 
      set { 
        agentInfo = value;
        if (updateAgentInfoEvent != null) updateAgentInfoEvent(agentInfo);
      } 
    }
    #endregion

    #region Public Functions    
   
    #endregion
  }
}
