using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentCommon;
using Messages;
using Common;

namespace BrilliantStudent
{
  class Program
  {
    static void Main(string[] args)
    {
      int port = Communicator.nextAvailablePort();

      if (args.Length >= 1) port = int.Parse(args[0]);

      StatusMonitor.get().post("Creating BrilliantStudent...");
      StatusMonitor.get().post("");
      BrilliantStudent brilliantStudent = new BrilliantStudent(port);

      StatusMonitor.get().post("Communicating on port: " + brilliantStudent.Communicator.Port);
      StatusMonitor.get().post("");
      brilliantStudent.autoPickGame();

      Console.ReadKey(false);
      StatusMonitor.get().post("");
      StatusMonitor.get().post("Killing Brilliant Student...");
    }
  }
}
