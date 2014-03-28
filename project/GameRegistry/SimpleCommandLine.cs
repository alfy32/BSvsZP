using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentCommon;
using Messages;
using Common;

namespace GameRegistry
{
  class SimpleCommandLine
  {
    SimpleCommandLine() {}

    public static void Run()
    {
      Console.Write("Enter communicator port number: ");
      Communicator communicator = new Communicator(int.Parse(Console.ReadLine()));

      Games gameRegistry = new Games();
      gameRegistry.displayAvailableGames();

      Console.Write("Enter the id of a game server: ");
      short gameId = short.Parse(Console.ReadLine());

      GameRegistry.Registrar.GameInfo game = gameRegistry.getGameInfoById(gameId);
      Console.WriteLine("You chose " + game.Label + " with id: " + game.Id);

      EndPoint gameEndPoint = new EndPoint(game.CommunicationEndPoint.Address, game.CommunicationEndPoint.Port);

      AgentInfo agentInfo = new Common.AgentInfo(10, AgentInfo.PossibleAgentType.BrilliantStudent);
      JoinGame joinGame = new JoinGame(game.Id, agentInfo);
      Envelope envelope = new Envelope(joinGame, gameEndPoint);

      Console.WriteLine("Sending JoinGame Message...");
      communicator.Send(envelope);
      System.Threading.Thread.Sleep(500);
      Envelope response = communicator.Recieve();
      AckNak ackNak = (AckNak)response.message;

      if (ackNak.Status == Messages.Reply.PossibleStatus.Success)
      {
        Console.WriteLine("Success!");
        Console.WriteLine();
        Console.WriteLine("Agent info:");

        AgentInfo resultAgentInfo = (AgentInfo)ackNak.ObjResult;
        Console.WriteLine(" Status: " + resultAgentInfo.AgentStatus);
        Console.WriteLine(" Location: " + resultAgentInfo.Location);
        Console.WriteLine(" Strength: " + resultAgentInfo.Strength);
        Console.WriteLine();
      }

      Console.WriteLine();
      Console.WriteLine("Press any key to continue . . .");
      Console.ReadKey();
    }
  }
}
