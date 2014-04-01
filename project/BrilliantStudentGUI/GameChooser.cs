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
using AgentCommon.Registrar;

namespace BrilliantStudentGUI
{
  public partial class GameChooser : Form
  {
    private string game = null;
    private GameRegistry registry = new GameRegistry();

    public string Game { get {return game; } }

    public GameChooser()
    {
      InitializeComponent();

      gameListBox.DataSource = registry.getAvailableGameList();
    }

    private void OK_Click(object sender, EventArgs e)
    {
      game = (string)gameListBox.SelectedItem;
      Close();
    }

    private void btn_cancel_Click(object sender, EventArgs e)
    {
      game = null;
      Close();
    }

    private void gameListBox_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      game = (string)gameListBox.SelectedItem;
      Close();
    }

    private void btn_refresh_Click(object sender, EventArgs e)
    {
      gameListBox.DataSource = registry.getAvailableGameList();
    }
  }
}
