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
  public partial class MainForm : Form
  {
    #region Private Memebers
    private GameRegistry gameRegistry = new GameRegistry();
    private Agent agent;
    #endregion

    #region Constructors
    public MainForm(Agent agent, string gameLabel)
    {
      this.agent = agent;

      InitializeComponent();

      StatusMonitor.get().debugMessageEvent += new StatusMonitor.StringMethod(updateMessages);
      agent.State.updateAgentInfoEvent += new AgentState.AgentInfoMethod(updateAgentInfo);
      agent.State.updateAgentListEvent += new AgentState.AgentListMethod(updateBSList);

      createAgentTreeView();
      GameInfo gameInfo = gameRegistry.getGameByLabel(gameLabel);

      int address = gameInfo.CommunicationEndPoint.Address;
      int port = gameInfo.CommunicationEndPoint.Port;

      agent.startJoinGameConversation(gameInfo.Id, new EndPoint(address, port));
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

    private void updateBSList(AgentList agentList)
    {
      this.Invoke(new updateBSListCallBack(updateBrilliantStudentTreeView), agentList);
    }

    TreeNode agentInfoNode = new TreeNode("Agent Info");
    TreeNode brilliantNode = new TreeNode("Brilliant Students");

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
      agentInfoNode.Nodes.Add(brilliantNode);

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
      agentTreeView.BeginUpdate();

      TreeNodeCollection brilliantNodes = brilliantNode.Nodes;

      foreach(AgentInfo agentInfo in agentList) {
        TreeNode treeNode = new TreeNode("Brilliant Student ID: " + agentInfo.Id.ToString());

        treeNode.Nodes.Add("Type", "Type: " + agentInfo.AgentType.ToString());
        if ( agentInfo.CommunicationEndPoint != null) treeNode.Nodes.Add("EndPoint", "EndPoint: " + agentInfo.CommunicationEndPoint.ToString());
        if ( agentInfo.ANumber != null) treeNode.Nodes.Add("ANumber", "ANumber: " + agentInfo.ANumber);
        if (agentInfo.FirstName != null && agentInfo.LastName != null) treeNode.Nodes.Add("Name", "Name: " + agentInfo.FirstName + " " + agentInfo.LastName);
        treeNode.Nodes.Add("AgentId", "AgentId: " + agentInfo.Id.ToString());
        treeNode.Nodes.Add("Status", "Status: " + agentInfo.AgentStatus.ToString());
        if (agentInfo.Location != null) treeNode.Nodes.Add("Location", "Location: " + agentInfo.Location.ToString());
        treeNode.Nodes.Add("Points", "Points: " + agentInfo.Points.ToString());
        treeNode.Nodes.Add("Strength", "Strength: " + agentInfo.Strength.ToString());
        treeNode.Nodes.Add("Speed", "Speed: " + agentInfo.Speed.ToString());

        brilliantNode.Nodes.Add(treeNode);
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
    public delegate void updateBSListCallBack(AgentList agentList);
    public delegate void updateAgentInfoCallback(AgentInfo param);
    public delegate void updateMessagesCallback(string message);
    #endregion

    #region Callback Functions
    private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      System.Environment.Exit(0);
    }
    #endregion

    private void getStudents_Click(object sender, EventArgs e)
    {
      agent.Brain.getResource(GetResource.PossibleResourceType.BrillianStudentList);
    }

    private void getExcuses_Click(object sender, EventArgs e)
    {
      agent.Brain.getResource(GetResource.PossibleResourceType.ExcuseGeneratorList);
    }

    private void getZombies_Click(object sender, EventArgs e)
    {
      agent.Brain.getResource(GetResource.PossibleResourceType.ZombieProfessorList);
    }

    private void getWhiners_Click(object sender, EventArgs e)
    {
      agent.Brain.getResource(GetResource.PossibleResourceType.WhiningSpinnerList);
    }

    private void getField_Click(object sender, EventArgs e)
    {
      agent.Brain.getResource(GetResource.PossibleResourceType.PlayingFieldLayout);
    }

    private void move_Click(object sender, EventArgs e)
    {
      // check that x and y are numbers
      if(moveToX.Text != "" && moveToY.Text != "") return;

      short x = short.Parse(moveToX.Text);
      short y = short.Parse(moveToY.Text);

      agent.Brain.move(new FieldLocation(x,y));
    }
  }
}
