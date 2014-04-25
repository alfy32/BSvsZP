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

      Agent agent = null;

      if (agentType.Text == "Brilliant Student")
        agent = new BrilliantStudent.BrilliantStudent(port);
      else if (agentType.Text == "Excuse Generator")
        agent = new ExcuseGenerator.ExcuseGenerator(port);
      else if (agentType.Text == "Whining Spinner")
        agent = new WhiningSpinner.WhiningSpinner(port);

      agent.State.AgentInfo.ANumber = aNumber.Text;
      agent.State.AgentInfo.FirstName = firstName.Text;
      agent.State.AgentInfo.LastName = lastName.Text;

      string gameLabel = availableGames.SelectedItem.ToString();
      if (gameLabel == null) Environment.Exit(1);

      AgentForm agentForm = new AgentForm(agent, gameLabel);
      Hide();
      agentForm.Show();
      agentForm.FormClosed += new FormClosedEventHandler(this.quit_Click);
    }



    private void quit_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
