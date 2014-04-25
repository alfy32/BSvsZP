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

    TreeNode agentInfoNode = new TreeNode("Agent Info");
    TreeNode brilliantNode = new TreeNode("Brilliant Students");
    TreeNode excuseNode = new TreeNode("Excuse Generators");
    TreeNode whineNode = new TreeNode("Whining Spinners");
    TreeNode zombieNode = new TreeNode("Zombie Professors");
    #endregion

    #region Constructors
    public AgentForm(Agent agent, string gameLabel)
    {
      this.agent = agent;

      InitializeComponent();

      //StatusMonitor.get().debugMessageEvent += new StatusMonitor.StringMethod(updateMessages);
      StatusMonitor.get().statusMessageEvent += new StatusMonitor.StringMethod(updateMessages);
      agent.State.updateAgentInfoEvent += new AgentState.AgentInfoMethod(updateAgentInfo);
      agent.State.updateAgentListEvent += new AgentState.AgentListMethod(updateAgentList);
      agent.tickCountEvent += new Agent.IntMethod(updateTicks);
      agent.excuseCountEvent += new Agent.IntMethod(updateExcuses);
      agent.whineCountEvent += new Agent.IntMethod(updateWhines);
      agent.closestZombieEvent += new Agent.IntMethod(updateClosestZombie);

      createAgentTreeView();
      GameInfo gameInfo = gameRegistry.getGameByLabel(gameLabel);

      int address = gameInfo.CommunicationEndPoint.Address;
      int port = gameInfo.CommunicationEndPoint.Port;

      agent.startJoinGameConversation(gameInfo.Id, new EndPoint(address, port));

      ThoughtsForm thoughtsForm = new ThoughtsForm(agent);
      thoughtsForm.Show();
    }
    #endregion

    #region Private Delegates
    private delegate void StringDelegate(string param);
    private delegate void AgentInfoDelegate(AgentInfo param);
    private delegate void AgentListDelegate(AgentList param);
    #endregion

    #region Thread Callbacks
    public void updateAgentInfo(AgentInfo agentInfo) { if(this.Visible) this.Invoke(new AgentInfoDelegate(updateAgentTreeView), agentInfo); }
    public void updateTicks(int count) { if (this.Visible) this.Invoke(new StringDelegate(updateTickCount), count.ToString()); }
    public void updateMessages(string message) { if (messageBox.InvokeRequired) this.Invoke(new StringDelegate(displayMessage), message); }
    public void updateExcuses(int count) { if (this.Visible) this.Invoke(new StringDelegate(updateExcuseCount), count.ToString()); }
    public void updateWhines(int count) { if (this.Visible) this.Invoke(new StringDelegate(updateWhineCount), count.ToString()); }
    public void updateClosestZombie(int count) { if (this.Visible) this.Invoke(new StringDelegate(updateClosestZombie), count.ToString()); }
    #endregion
    
    #region Update Callbacks
    private void updateTickCount(string count) {tickCount.Text = count;}
    private void updateExcuseCount(string count) { excuseCount.Text = count; }
    private void updateWhineCount(string count) { whineCount.Text = count; }
    private void updateClosestZombie(string count) { closestZombie.Text = count; }

    private void updateAgentList(AgentList agentList)
    {
      if (agentList.Count > 0)
      {
        foreach (AgentInfo agentInfo in agentList) {
          this.Invoke(new AgentInfoDelegate(updateAgentInfoNode), agentInfo);
        }
      }
    }

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

    private void updateAgentInfoNode(AgentInfo agentInfo)
    {
      agentTreeView.BeginUpdate();

      switch (agentInfo.AgentType)
      {
        case AgentInfo.PossibleAgentType.BrilliantStudent:
          updateAgentNode(brilliantNode, agentInfo);         
          break;
        case AgentInfo.PossibleAgentType.ExcuseGenerator:
          updateAgentNode(excuseNode, agentInfo);
          break;
        case AgentInfo.PossibleAgentType.WhiningSpinner:
          updateAgentNode(whineNode, agentInfo);
          break;
        case AgentInfo.PossibleAgentType.ZombieProfessor:
          updateAgentNode(zombieNode, agentInfo);
          break;
      }

      agentTreeView.EndUpdate();
    }

    private void updateAgentNode(TreeNode node, AgentInfo agentInfo)
    {
      if (node.Nodes.ContainsKey(agentInfo.Id.ToString()))
      {
        TreeNodeCollection nodes = node.Nodes[agentInfo.Id.ToString()].Nodes;

        if (nodes["Type"] != null) nodes["Type"].Text = "Type: " + agentInfo.AgentType.ToString();
        if (nodes["EndPoint"] != null && agentInfo.CommunicationEndPoint != null) nodes["EndPoint"].Text = "EndPoint: " + agentInfo.CommunicationEndPoint.ToString();
        if (nodes["ANumber"] != null && agentInfo.ANumber != null) nodes["ANumber"].Text = "ANumber: " + agentInfo.ANumber;
        if (nodes["Name"] != null && agentInfo.FirstName != null && agentInfo.LastName != null) nodes["Name"].Text = "Name: " + agentInfo.FirstName + " " + agentInfo.LastName;
        if (nodes["AgentId"] != null) nodes["AgentId"].Text = "AgentId: " + agentInfo.Id.ToString();
        if (nodes["Status"] != null) nodes["Status"].Text = "Status: " + agentInfo.AgentStatus.ToString();
        if (nodes["Location"] != null && agentInfo.Location != null) nodes["Location"].Text = "Location: " + agentInfo.Location.ToString();
        if (nodes["Points"] != null) nodes["Points"].Text = "Points: " + agentInfo.Points.ToString();
        if (nodes["Strength"] != null) nodes["Strength"].Text = "Strength: " + agentInfo.Strength.ToString();
        if (nodes["Speed"] != null) nodes["Speed"].Text = "Speed: " + agentInfo.Speed.ToString();
      }
      else
      {
        node.Nodes.Add(agentInfo.Id.ToString(), agentInfo.Id.ToString() + " " + agentInfo.FirstName + " " + agentInfo.CommunicationEndPoint.ToString());

        TreeNodeCollection nodes = node.Nodes[agentInfo.Id.ToString()].Nodes;

        nodes.Add("Type: " + agentInfo.AgentType.ToString());
        if (agentInfo.CommunicationEndPoint != null) nodes.Add("EndPoint: " + agentInfo.CommunicationEndPoint.ToString());
        if (agentInfo.ANumber != null) nodes.Add("ANumber: " + agentInfo.ANumber);
        if (agentInfo.FirstName != null && agentInfo.LastName != null) nodes.Add("Name: " + agentInfo.FirstName + " " + agentInfo.LastName);
        nodes.Add("AgentId: " + agentInfo.Id.ToString());
        nodes.Add("Status: " + agentInfo.AgentStatus.ToString());
        if (agentInfo.Location != null) nodes.Add("Location: " + agentInfo.Location.ToString());
        nodes.Add("Points: " + agentInfo.Points.ToString());
        nodes.Add("Strength: " + agentInfo.Strength.ToString());
        nodes.Add("Speed: " + agentInfo.Speed.ToString());
      }
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
    //private void updateBrilliantStudentTreeView(AgentInfo agentInfo)
    //{
    //  addAgentListToTreeNode(brilliantNode, agentInfo);
    //}

    //private void updateExcuseGeneratorTreeView(AgentList agentList)
    //{
    //  addAgentListToTreeNode(excuseNode, agentList);
    //}

    //private void updateWhiningSpinnerTreeView(AgentList agentList)
    //{
    //  addAgentListToTreeNode(whineNode, agentList);
    //}

    //private void updateZombieProfessorTreeView(AgentList agentList)
    //{
    //  addAgentListToTreeNode(zombieNode, agentList);
    //}

    //private void addAgentListToTreeNode(TreeNode treeNode, AgentList agentList)
    //{
    //  agentTreeView.BeginUpdate();

    //  treeNode.Nodes.Clear();

    //  foreach (AgentInfo agentInfo in agentList)
    //  {
    //    TreeNode agentTreeNode = new TreeNode(agentInfo.AgentType + " ID: " + agentInfo.Id);

    //    agentTreeNode.Nodes.Add("Type", "Type: " + agentInfo.AgentType);
    //    if (agentInfo.CommunicationEndPoint != null) agentTreeNode.Nodes.Add("EndPoint", "EndPoint: " + agentInfo.CommunicationEndPoint);
    //    if (agentInfo.ANumber != null) agentTreeNode.Nodes.Add("ANumber", "ANumber: " + agentInfo.ANumber);
    //    if (agentInfo.FirstName != null && agentInfo.LastName != null) agentTreeNode.Nodes.Add("Name", "Name: " + agentInfo.FirstName + " " + agentInfo.LastName);
    //    agentTreeNode.Nodes.Add("AgentId", "AgentId: " + agentInfo.Id);
    //    agentTreeNode.Nodes.Add("Status", "Status: " + agentInfo.AgentStatus);
    //    if (agentInfo.Location != null) agentTreeNode.Nodes.Add("Location", "Location: " + agentInfo.Location);
    //    agentTreeNode.Nodes.Add("Points", "Points: " + agentInfo.Points);
    //    agentTreeNode.Nodes.Add("Strength", "Strength: " + agentInfo.Strength);
    //    agentTreeNode.Nodes.Add("Speed", "Speed: " + agentInfo.Speed);

    //    treeNode.Nodes.Add(agentTreeNode);
    //  }

    //  agentTreeView.EndUpdate();
    //}

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

    #region Callback Functions
    private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      System.Environment.Exit(0);
    }
    #endregion

    private void AgentForm_VisibleChanged(object sender, EventArgs e)
    {
      agent.State.AgentInfo = agent.State.AgentInfo;
    }

  }
}
