using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentCommon;

namespace WhiningSpinner
{
  class Program
  {
    static void Main(string[] args)
    {
      int port = Communicator.nextAvailablePort();

      if (args.Length >= 1) port = int.Parse(args[0]);

      StatusMonitor.get().postDebug("Creating WhiningSpinner...");
      StatusMonitor.get().postDebug("");
      WhiningSpinner whiningSpinner = new WhiningSpinner(port);

      StatusMonitor.get().postDebug("Communicating on port: " + whiningSpinner.Communicator.Port);
      StatusMonitor.get().postDebug("");
      whiningSpinner.autoPickGame();

      while (Console.ReadKey(false).Key != System.ConsoleKey.Escape) ;
      StatusMonitor.get().postDebug("");
      StatusMonitor.get().postDebug("Killing WhiningSpinner...");
    }
  }
}
