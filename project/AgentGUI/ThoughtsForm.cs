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
  public partial class ThoughtsForm : Form
  {
    private Agent agent;
    public ThoughtsForm(Agent agent)
    {
      this.agent = agent;

      InitializeComponent();
    }

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

    private void getConfiguration_Click(object sender, EventArgs e)
    {
      agent.Brain.getResource(GetResource.PossibleResourceType.GameConfiguration);
    }

    private void move_Click(object sender, EventArgs e)
    {
      // check that x and y are numbers
      if (moveToX.Text == "" && moveToY.Text == "")
      {
        StatusMonitor.get().postDebug("Enter the location to move to.");
        return;
      }

      short x = short.Parse(moveToX.Text);
      short y = short.Parse(moveToY.Text);

      agent.Brain.move(new FieldLocation(x, y));
    }
  }
}
