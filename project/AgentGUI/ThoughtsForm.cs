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

      if (agent.State.AgentInfo.AgentType != AgentInfo.PossibleAgentType.BrilliantStudent)
      {
        getWhine.Enabled = false;
        spinnerId.Enabled = false;
        getExcuse.Enabled = false;
        genteratorId.Enabled = false;
      }
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

    private void getExcuse_Click(object sender, EventArgs e)
    {
      StatusMonitor statusMonitor = StatusMonitor.get();

      if(genteratorId.Text == "") {
        statusMonitor.postDebug("You need a generator Id.");
        return;
      }
      if (agent.State.ExcuseGeneratorList == null || agent.State.ExcuseGeneratorList.Count == 0)
      {
        statusMonitor.postDebug("There are no generators to get an excuse from.");
        return;
      }

      short genId = short.Parse(genteratorId.Text);

      EndPoint endPoint = null;
      foreach (AgentInfo agentInfo in agent.State.ExcuseGeneratorList)
      {
        if (agentInfo.Id == genId)
        {
          endPoint = agentInfo.CommunicationEndPoint;
        }
      }
      if (endPoint == null) endPoint = agent.State.ExcuseGeneratorList[0].CommunicationEndPoint;

      statusMonitor.postDebug("Asking for whine...");
      GetResource getResource = new GetResource(agent.State.AgentInfo.Id, GetResource.PossibleResourceType.Excuse);
      ((BrilliantStudent.BrilliantBrain)agent.Brain).getExcuse(endPoint);
    }

    private void getWhine_Click(object sender, EventArgs e)
    {
      StatusMonitor statusMonitor = StatusMonitor.get();

      if (spinnerId.Text == "")
      {
        StatusMonitor.get().postDebug("You need a spinner Id.");
        return;
      }
      if (agent.State.WhiningSpinnerList == null || agent.State.WhiningSpinnerList.Count == 0)
      {
        statusMonitor.postDebug("There are no spinners to get a whine from.");
        return;
      }

      short genId = short.Parse(genteratorId.Text);

      EndPoint endPoint = null;
      foreach (AgentInfo agentInfo in agent.State.WhiningSpinnerList)
      {
        if (agentInfo.Id == genId)
        {
          endPoint = agentInfo.CommunicationEndPoint;
        }
      }
      if (endPoint == null) endPoint = agent.State.WhiningSpinnerList[0].CommunicationEndPoint;

      statusMonitor.postDebug("Asking for whine...");
      GetResource getResource = new GetResource(agent.State.AgentInfo.Id, GetResource.PossibleResourceType.WhiningTwine);
      ((BrilliantStudent.BrilliantBrain)agent.Brain).getWhine(endPoint);
    }

    private void moveUp_Click(object sender, EventArgs e)
    {
      short speed = 1;

      // check that x and y are numbers
      if(agent.State.GameConfiguration != null) {
        if (moveSpeed.Text == "") speed = (short)agent.State.GameConfiguration.BrilliantStudentBaseSpeed;
        else
        {
          speed = short.Parse(moveSpeed.Text);
          if (speed > agent.State.GameConfiguration.BrilliantStudentBaseSpeed)
            speed = (short)agent.State.GameConfiguration.BrilliantStudentBaseSpeed;
        }
      }

      short x = agent.State.AgentInfo.Location.X;
      short y = (short)(agent.State.AgentInfo.Location.Y - speed);

      agent.Brain.move(new FieldLocation(x, y));
    }

    private void moveDown_Click(object sender, EventArgs e)
    {
      short speed = 1;

      // check that x and y are numbers
      if (agent.State.GameConfiguration != null)
      {
        if (moveSpeed.Text == "") speed = (short)agent.State.GameConfiguration.BrilliantStudentBaseSpeed;
        else
        {
          speed = short.Parse(moveSpeed.Text);
          if (speed > agent.State.GameConfiguration.BrilliantStudentBaseSpeed)
            speed = (short)agent.State.GameConfiguration.BrilliantStudentBaseSpeed;
        }
      }

      short x = agent.State.AgentInfo.Location.X;
      short y = (short)(agent.State.AgentInfo.Location.Y + speed);

      agent.Brain.move(new FieldLocation(x, y));
    }

    private void moveLeft_Click(object sender, EventArgs e)
    {
      short speed = 1;

      // check that x and y are numbers
      if (agent.State.GameConfiguration != null)
      {
        if (moveSpeed.Text == "") speed = (short)agent.State.GameConfiguration.BrilliantStudentBaseSpeed;
        else
        {
          speed = short.Parse(moveSpeed.Text);
          if (speed > agent.State.GameConfiguration.BrilliantStudentBaseSpeed)
            speed = (short)agent.State.GameConfiguration.BrilliantStudentBaseSpeed;
        }
      }

      short x = (short)(agent.State.AgentInfo.Location.X - speed);
      short y = agent.State.AgentInfo.Location.Y;

      agent.Brain.move(new FieldLocation(x, y));
    }

    private void moveRight_Click(object sender, EventArgs e)
    {
      short speed = 1;

      // check that x and y are numbers
      if (agent.State.GameConfiguration != null)
      {
        if (moveSpeed.Text == "") speed = (short)agent.State.GameConfiguration.BrilliantStudentBaseSpeed;
        else
        {
          speed = short.Parse(moveSpeed.Text);
          if (speed > agent.State.GameConfiguration.BrilliantStudentBaseSpeed)
            speed = (short)agent.State.GameConfiguration.BrilliantStudentBaseSpeed;
        }
      }

      short x = (short)(agent.State.AgentInfo.Location.X + speed);
      short y = agent.State.AgentInfo.Location.Y;

      agent.Brain.move(new FieldLocation(x, y));
    }
  }
}
