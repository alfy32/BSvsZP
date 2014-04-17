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
  public partial class StartupForm : Form
  {
    GameRegistry gameRegistry = new GameRegistry();
    public StartupForm()
    {
      InitializeComponent();

      availableGames.DataSource = gameRegistry.getAvailableGameList();
    }

    private void refresh_Click(object sender, EventArgs e)
    {
      availableGames.DataSource = gameRegistry.getAvailableGameList();
    }

    private void startAgent_Click(object sender, EventArgs e)
    {
      int port = (int)this.port.Value;

      AgentInfo agentInfo = new AgentInfo();
      agentInfo.ANumber = aNumber.Text;
      agentInfo.FirstName = firstName.Text;
      agentInfo.LastName = lastName.Text;

      Agent agent = null;

      if (agentType.Text == "Brilliant Student")
      {
        agent = new BrilliantStudent.BrilliantStudent(port);
        agentInfo.AgentType = AgentInfo.PossibleAgentType.BrilliantStudent;
      }
      else if (agentType.Text == "Excuse Generator")
      {
        agent = new ExcuseGenerator.ExcuseGenerator(port);
        agentInfo.AgentType = AgentInfo.PossibleAgentType.ExcuseGenerator;
      }
      else if (agentType.Text == "Whining Spinner")
      {
        agent = new WhiningSpinner.WhiningSpinner(port);
        agentInfo.AgentType = AgentInfo.PossibleAgentType.WhiningSpinner;
      }

      MainForm mainForm = new MainForm(agent, availableGames.SelectedItem.ToString());
      Hide();
      mainForm.Show();
      mainForm.FormClosed += new FormClosedEventHandler(this.quit_Click);

      agent.State.AgentInfo = agentInfo;
    }



    private void quit_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
