using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrilliantStudentGUI
{
  public partial class BrilliantStudentForm : Form
  {
    GameChooser gameChooser;

    public BrilliantStudentForm()
    {
      InitializeComponent();
    }

    private void btn_joinGame_Click(object sender, EventArgs e)
    {
      
    }

    private void GameChooser_FormClosed(object sender, FormClosedEventArgs e)
    {
      lbl_currentGame.Text = gameChooser.Game;
    }

    private void btn_selectGame_Click(object sender, EventArgs e)
    {
      gameChooser = new GameChooser();
      gameChooser.Show();
      gameChooser.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameChooser_FormClosed);
    }
  }
}
