using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AgentCommon;
using Messages;
using Common;

namespace AgentGUI
{
  public partial class AgentForm : Form
  {
    #region Private Memebers
    private GameRegistry gameRegistry = new GameRegistry();
    private Agent agent;
    #endregion

    #region Constructors
    public AgentForm(Agent agent, string gameLabel)
    {
      this.agent = agent;

      InitializeComponent();

      StatusMonitor.get().debugMessageEvent += new StatusMonitor.StringMethod(updateMessages);
      agent.State.updateAgentInfoEvent += new AgentState.AgentInfoMethod(updateAgentInfo);
      agent.State.updateAgentListEvent += new AgentState.AgentListMethod(updateAgentList);

      createAgentTreeView();
      GameInfo gameInfo = gameRegistry.getGameByLabel(gameLabel);

      int address = gameInfo.CommunicationEndPoint.Address;
      int port = gameInfo.CommunicationEndPoint.Port;

      agent.startJoinGameConversation(gameInfo.Id, new EndPoint(address, port));

      ThoughtsForm thoughtsForm = new ThoughtsForm(agent);
      thoughtsForm.Show();
    }
    #endregion

    #region Update Callbacks
    private void updateAgentInfo(AgentInfo agentInfo)
    {
      this.Invoke(new updateAgentInfoCallback(updateAgentTreeView), agentInfo);
    }
    private void updateMessages(string message)
    {
      if (messageBox.InvokeRequired)
        this.Invoke(new updateMessagesCallback(displayMessage), message);
    }

    private void updateAgentList(AgentList agentList)
    {
      if (agentList.Count > 0)
      {
        AgentInfo agentInfo = agentList.First();
        switch (agentInfo.AgentType)
        {
          case AgentInfo.PossibleAgentType.BrilliantStudent:
            this.Invoke(new updateAgentListCallBack(updateBrilliantStudentTreeView), agentList);
            break;
          case AgentInfo.PossibleAgentType.ExcuseGenerator:
            this.Invoke(new updateAgentListCallBack(updateExcuseGeneratorTreeView), agentList);
            break;
          case AgentInfo.PossibleAgentType.WhiningSpinner:
            this.Invoke(new updateAgentListCallBack(updateWhiningSpinnerTreeView), agentList);
            break;
          case AgentInfo.PossibleAgentType.ZombieProfessor:
            this.Invoke(new updateAgentListCallBack(updateZombieProfessorTreeView), agentList);
            break;
        }
      }
      else
      {
        StatusMonitor.get().postDebug("Can't Update agents. The list is empty");
      }
    }


    TreeNode agentInfoNode = new TreeNode("Agent Info");
    TreeNode brilliantNode = new TreeNode("Brilliant Students");
    TreeNode excuseNode = new TreeNode("Excuse Generators");
    TreeNode whineNode = new TreeNode("Whining Spinners");
    TreeNode zombieNode = new TreeNode("Zombie Professors");


    private void createAgentTreeView()
    {
      agentTreeView.BeginUpdate();

      TreeNodeCollection nodes = agentTreeView.Nodes;

      nodes.Clear();

      // Agent Info
      agentInfoNode.Nodes.Add("Type", "Type: ");
      agentInfoNode.Nodes.Add("EndPoint", "EndPoint: ");
      agentInfoNode.Nodes.Add("ANumber", "ANumber: ");
      agentInfoNode.Nodes.Add("Name", "Name: ");
      agentInfoNode.Nodes.Add("AgentId", "AgentId: ");
      agentInfoNode.Nodes.Add("Status", "Status: ");
      agentInfoNode.Nodes.Add("Location", "Location: ");
      agentInfoNode.Nodes.Add("Points", "Points: ");
      agentInfoNode.Nodes.Add("Strength", "Strength: ");
      agentInfoNode.Nodes.Add("Speed", "Speed: ");
      nodes.Add(agentInfoNode);

      // BS list
      nodes.Add(brilliantNode);
      nodes.Add(excuseNode);
      nodes.Add(whineNode);
      nodes.Add(zombieNode);

      agentTreeView.EndUpdate();
    }

    private void updateAgentTreeView(AgentInfo agentInfo)
    {
      displayAgentInfo(agentInfo);

      agentTreeView.BeginUpdate();

      TreeNodeCollection items = agentInfoNode.Nodes;

      if (items["Type"] != null) items["Type"].Text = "Type: " + agentInfo.AgentType.ToString();
      if (items["EndPoint"] != null && agentInfo.CommunicationEndPoint != null) items["EndPoint"].Text = "EndPoint: " + agentInfo.CommunicationEndPoint.ToString();
      if (items["ANumber"] != null && agentInfo.ANumber != null) items["ANumber"].Text = "ANumber: " + agentInfo.ANumber;
      if (items["Name"] != null && agentInfo.FirstName != null && agentInfo.LastName != null) items["Name"].Text = "Name: " + agentInfo.FirstName + " " + agentInfo.LastName;
      if (items["AgentId"] != null) items["AgentId"].Text = "AgentId: " + agentInfo.Id.ToString();
      if (items["Status"] != null) items["Status"].Text = "Status: " + agentInfo.AgentStatus.ToString();
      if (items["Location"] != null && agentInfo.Location != null) items["Location"].Text = "Location: " + agentInfo.Location.ToString();
      if (items["Points"] != null) items["Points"].Text = "Points: " + agentInfo.Points.ToString();
      if (items["Strength"] != null) items["Strength"].Text = "Strength: " + agentInfo.Strength.ToString();
      if (items["Speed"] != null) items["Speed"].Text = "Speed: " + agentInfo.Speed.ToString();

      agentTreeView.EndUpdate();
    }
    private void updateBrilliantStudentTreeView(AgentList agentList)
    {
      addAgentListToTreeNode(brilliantNode, agentList);
    }

    private void updateExcuseGeneratorTreeView(AgentList agentList)
    {
      addAgentListToTreeNode(excuseNode, agentList);
    }

    private void updateWhiningSpinnerTreeView(AgentList agentList)
    {
      addAgentListToTreeNode(whineNode, agentList);
    }

    private void updateZombieProfessorTreeView(AgentList agentList)
    {
      addAgentListToTreeNode(zombieNode, agentList);
    }

    private void addAgentListToTreeNode(TreeNode treeNode, AgentList agentList)
    {
      agentTreeView.BeginUpdate();

      treeNode.Nodes.Clear();

      foreach (AgentInfo agentInfo in agentList)
      {
        TreeNode agentTreeNode = new TreeNode(agentInfo.AgentType + " ID: " + agentInfo.Id);

        agentTreeNode.Nodes.Add("Type", "Type: " + agentInfo.AgentType);
        if (agentInfo.CommunicationEndPoint != null) agentTreeNode.Nodes.Add("EndPoint", "EndPoint: " + agentInfo.CommunicationEndPoint);
        if (agentInfo.ANumber != null) agentTreeNode.Nodes.Add("ANumber", "ANumber: " + agentInfo.ANumber);
        if (agentInfo.FirstName != null && agentInfo.LastName != null) agentTreeNode.Nodes.Add("Name", "Name: " + agentInfo.FirstName + " " + agentInfo.LastName);
        agentTreeNode.Nodes.Add("AgentId", "AgentId: " + agentInfo.Id);
        agentTreeNode.Nodes.Add("Status", "Status: " + agentInfo.AgentStatus);
        if (agentInfo.Location != null) agentTreeNode.Nodes.Add("Location", "Location: " + agentInfo.Location);
        agentTreeNode.Nodes.Add("Points", "Points: " + agentInfo.Points);
        agentTreeNode.Nodes.Add("Strength", "Strength: " + agentInfo.Strength);
        agentTreeNode.Nodes.Add("Speed", "Speed: " + agentInfo.Speed);

        treeNode.Nodes.Add(agentTreeNode);
      }

      agentTreeView.EndUpdate();
    }

    private void displayAgentInfo(AgentInfo agentInfo)
    {
      agentType.Text = agentInfo.AgentType.ToString();
      if (agentInfo.CommunicationEndPoint != null)
        publicIP.Text = agentInfo.CommunicationEndPoint.ToString();

      if (agentInfo.ANumber != null) aNumber.Text = agentInfo.ANumber;
      if (agentInfo.FirstName != null && agentInfo.LastName != null)
        name.Text = agentInfo.FirstName + " " + agentInfo.LastName;

      agentId.Text = agentInfo.Id.ToString();
      agentStatus.Text = agentInfo.AgentStatus.ToString();

      if (agentInfo.Location != null)
        location.Text = agentInfo.Location.ToString();
      points.Text = agentInfo.Points.ToString();

      strength.Text = agentInfo.Strength.ToString();
      speed.Text = agentInfo.Speed.ToString();

      //  agentInfo.AgentType;  // agentInfo.CommunicationEndPoint;
      //  agentInfo.ANumber;    // agentInfo.FirstName; agentInfo.LastName;
      //  agentInfo.AgentStatus;// agentInfo.Id;
      //  agentInfo.Location;   // agentInfo.Points;
      //  agentInfo.Strength;   // agentInfo.Speed;
    }
    private void displayMessage(string message)
    {
      messageBox.Items.Add(message);
    }
    #endregion

    #region Delegates
    public delegate void updateAgentListCallBack(AgentList agentList);
    public delegate void updateAgentInfoCallback(AgentInfo param);
    public delegate void updateMessagesCallback(string message);
    #endregion

    #region Callback Functions
    private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      System.Environment.Exit(0);
    }
    #endregion
  }
}
