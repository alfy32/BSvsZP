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

      StatusMonitor.get().post("Creating WhiningSpinner...");
      StatusMonitor.get().post("");
      WhiningSpinner whiningSpinner = new WhiningSpinner(port);

      StatusMonitor.get().post("Communicating on port: " + whiningSpinner.Communicator.Port);
      StatusMonitor.get().post("");
      whiningSpinner.autoPickGame();

      Console.ReadKey(false);
      StatusMonitor.get().post("");
      StatusMonitor.get().post("Killing WhiningSpinner...");
    }
  }
}
