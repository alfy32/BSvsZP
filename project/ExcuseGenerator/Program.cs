using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentCommon;
using Messages;
using Common;

namespace ExcuseGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
          int port = Communicator.nextAvailablePort();

          if (args.Length >= 1) port = int.Parse(args[0]);

          StatusMonitor.get().postDebug("Creating ExcuseGenerator...");
          StatusMonitor.get().postDebug("");
          ExcuseGenerator excuseGenerator = new ExcuseGenerator(port);

          StatusMonitor.get().postDebug("Communicating on port: " + excuseGenerator.Communicator.Port);
          StatusMonitor.get().postDebug("");
          if (args.Length >= 2) excuseGenerator.pickGameByLabel(args[1]);
          else excuseGenerator.askUserForGame();

          while (Console.ReadKey(false).Key != System.ConsoleKey.Escape) ;
          StatusMonitor.get().postDebug("");
          StatusMonitor.get().postDebug("Killing ExcuseGenerator...");
        }
    }
}
