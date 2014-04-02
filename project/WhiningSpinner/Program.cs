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

      Console.WriteLine("Creating WhiningSpinner...");
      Console.WriteLine();
      WhiningSpinner whiningSpinner = new WhiningSpinner(port);

      Console.WriteLine("Communicating on port: " + whiningSpinner.Communicator.Port);
      Console.WriteLine();
      whiningSpinner.autoPickGame();

      Console.ReadKey(false);
      Console.WriteLine();
      Console.WriteLine("Killing WhiningSpinner...");
    }
  }
}
