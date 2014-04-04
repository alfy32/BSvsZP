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

          StatusMonitor.get().post("Creating ExcuseGenerator...");
          StatusMonitor.get().post("");
          ExcuseGenerator excuseGenerator = new ExcuseGenerator(port);

          StatusMonitor.get().post("Communicating on port: " + excuseGenerator.Communicator.Port);
          StatusMonitor.get().post("");
          excuseGenerator.autoPickGame();

          Console.ReadKey(false);
          StatusMonitor.get().post("");
          StatusMonitor.get().post("Killing ExcuseGenerator...");
        }
    }
}
