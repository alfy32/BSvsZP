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

      StatusMonitor.get().postDebug("Creating BrilliantStudent...");
      StatusMonitor.get().postDebug("");
      BrilliantStudent brilliantStudent = new BrilliantStudent(port);

      StatusMonitor.get().postDebug("Communicating on port: " + brilliantStudent.Communicator.Port);
      StatusMonitor.get().postDebug("");
      brilliantStudent.autoPickGame();

      while (Console.ReadKey(false).Key != System.ConsoleKey.Escape) ;
      StatusMonitor.get().postDebug("");
      StatusMonitor.get().postDebug("Killing Brilliant Student...");
    }
  }
}
