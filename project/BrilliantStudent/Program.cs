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

      Console.WriteLine("Creating BrilliantStudent...");
      Console.WriteLine();
      BrilliantStudent brilliantStudent = new BrilliantStudent(port);

      Console.WriteLine("Communicating on port: " + brilliantStudent.Communicator.Port);
      Console.WriteLine();
      brilliantStudent.askUserForGame();

      Console.ReadKey(false);
      Console.WriteLine();
      Console.WriteLine("Killing Brilliant Student...");
    }
  }
}
