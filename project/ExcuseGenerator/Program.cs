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

          Console.WriteLine("Creating ExcuseGenerator...");
          Console.WriteLine();
          ExcuseGenerator excuseGenerator = new ExcuseGenerator(port);

          Console.WriteLine("Communicating on port: " + excuseGenerator.Communicator.Port);
          Console.WriteLine();
          excuseGenerator.autoPickGame();

          Console.ReadKey(false);
          Console.WriteLine();
          Console.WriteLine("Killing ExcuseGenerator...");
        }
    }
}
