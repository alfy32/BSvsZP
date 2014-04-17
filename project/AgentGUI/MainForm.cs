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

     TreeNode agentInfoNode = new TreeNode("Agent Info");

    private void createAgentTreeView()
    {
      agentTreeView.BeginUpdate();

      TreeNodeCollection nodes = agentTreeView.Nodes;

      nodes.Clear();
     
      // Agent Info
      agentInfoNode.Nodes.Add("Type", "Type: ");
      agentInfoNode.Nodes.Add("EndPoint", "EndPoint: ");
      agentInfoNode.Nodes.Add("ANumber", "ANumber: ");
      agentInfoNode.Nodes.Add("FirstName", "ANumber: ");
      agentInfoNode.Nodes.Add("LastName", "ANumber: ");
      agentInfoNode.Nodes.Add("AgentId", "ANumber: ");
      agentInfoNode.Nodes.Add("Status", "ANumber: ");
      agentInfoNode.Nodes.Add("Location", "ANumber: ");
      agentInfoNode.Nodes.Add("Points", "ANumber: ");
      agentInfoNode.Nodes.Add("Strenght", "ANumber: ");
      agentInfoNode.Nodes.Add("Speed", "ANumber: ");
      nodes.Add(agentInfoNode);

      agentTreeView.EndUpdate();
    }

    private void updateAgentTreeView(AgentInfo agentInfo)
    {
      displayAgentInfo(agentInfo);

      agentTreeView.BeginUpdate();

      TreeNodeCollection items = agentInfoNode.Nodes;

      //items["type"].Name = "Type: " +agentInfo.AgentType.ToString();        
      //if (agentInfo.CommunicationEndPoint != null) items.Add("EndPoint: " +agentInfo.CommunicationEndPoint.ToString());
      //if (agentInfo.ANumber != null) items.Add("A Number: " +agentInfo.ANumber);
      //if (agentInfo.FirstName != null && agentInfo.LastName != null)
      //  items.Add(agentInfo.FirstName + " " + agentInfo.LastName;

      //agentId.Text = agentInfo.Id.ToString();
      //agentStatus.Text = agentInfo.AgentStatus.ToString();

      //if (agentInfo.Location != null)
      //  location.Text = agentInfo.Location.ToString();
      //points.Text = agentInfo.Points.ToString();

      //strength.Text = agentInfo.Strength.ToString();
      //speed.Text = agentInfo.Speed.ToString();

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
