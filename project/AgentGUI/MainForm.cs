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
    GameRegistry gameRegistry = new GameRegistry();
    Agent agent;
    List<string> messages = new List<string>();
    #endregion

    #region Private Functions
    private void startGame(Agent agent, string gameLabel)
    {
      AgentCommon.Registrar.GameInfo gameInfo = gameRegistry.getGameByLabel(gameLabel);

      int address = gameInfo.CommunicationEndPoint.Address;
      int port = gameInfo.CommunicationEndPoint.Port;

      agent.startJoinGameConversation(gameInfo.Id, new EndPoint(address, port));
    }
    private void updateAgentInfo(AgentInfo agentInfo)
    {
      this.Invoke(new updateAgentInfoCallback(displayAgentInfo), agentInfo);
    }
    private void updateMessages(string message)
    {
      messages.Add(message);

      if(messageBox.InvokeRequired)
        this.Invoke(new updateMessagesCallback(displayMessage), message);
    }

    private void displayAgentInfo(AgentInfo agentInfo)
    {
      agentType.Text = agentInfo.AgentType.ToString();
      if (agentInfo.CommunicationEndPoint != null)
        publicIP.Text = agentInfo.CommunicationEndPoint.ToString();

      if (agentInfo.ANumber != null) aNumber.Text = agentInfo.ANumber;
      if (agentInfo.FirstName != null && agentInfo.LastName != null)
        name.Text = agentInfo.FirstName + " " + agentInfo.LastName;

      if(agentInfo.Id != null) agentId.Text = agentInfo.Id.ToString();
      if (agentInfo.AgentStatus != null) agentStatus.Text = agentInfo.AgentStatus.ToString();

      if (agentInfo.Location != null)
        location.Text = agentInfo.Location.ToString();
      points.Text = agentInfo.Points.ToString();

      if (agentInfo.Strength != null) strength.Text = agentInfo.Strength.ToString();
      if (agentInfo.Speed != null) speed.Text = agentInfo.Speed.ToString();

      //  agentInfo.AgentType;  // agentInfo.CommunicationEndPoint;
      //  agentInfo.ANumber;    // agentInfo.FirstName; agentInfo.LastName;
      //  agentInfo.AgentStatus;// agentInfo.Id;
      //  agentInfo.Location;   // agentInfo.Points;
      //  agentInfo.Strength;   // agentInfo.Speed;
    }
    private void displayMessage(string message)
    {
      messageBox.DataSource = messages;
    }
    #endregion

    public delegate void updateAgentInfoCallback(AgentInfo param);
    public delegate void updateMessagesCallback(string message);

    public MainForm(Agent agent, string gameLabel)
    {
      this.agent = agent;
      InitializeComponent();

      agent.State.updateAgentInfoEvent += new AgentState.AgentInfoMethod(updateAgentInfo);
      StatusMonitor.get().postMessageEvent += new StatusMonitor.StringMethod(updateMessages);
      startGame(agent, gameLabel);
    }

    private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      System.Environment.Exit(0);
    }
  }
}
