using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AgentGUI
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      if (args.Length == 0)
      {        
        Application.Run(new StartupForm());
      }
      else
      {
        string whichAgent = args[0];
        int port = int.Parse(args[1]);
        string whichGame = args[2];

        AgentCommon.Agent agent = null;

        if (whichAgent == "BS")
          agent = new BrilliantStudent.BrilliantStudent(port);
        else if (whichAgent == "EG")
          agent = new ExcuseGenerator.ExcuseGenerator(port);
        else if (whichAgent == "WS")
          agent = new WhiningSpinner.WhiningSpinner(port);

        agent.pickGameByLabel(whichGame);
        Application.Run(new AgentForm(agent, whichGame));
      }
    }
  }
}
