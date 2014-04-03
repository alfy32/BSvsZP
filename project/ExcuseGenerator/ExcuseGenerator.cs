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
  class ExcuseGenerator : Agent
  {
     public ExcuseGenerator(int port = -1)
      : base(AgentInfo.PossibleAgentType.ExcuseGenerator, port)
    {
      ExecutionStrategy.addStrategy(Message.MESSAGE_CLASS_IDS.JoinGame, typeof(JoinGameStrategy));
    }

     public void autoPickGame()
     {
       GameRegistry gameRegistry = new GameRegistry();
       AgentCommon.Registrar.GameInfo game = gameRegistry.getGameByLabel("alan");

       if (game == null)
       {
         Console.Write("There are no games to join. Press any key to quit...");
         Console.ReadKey(false);
         Console.WriteLine();
         Console.WriteLine("Shutting Down...");
         Environment.Exit(0);
       }

       Console.WriteLine("Auto choosing game:");
       Console.WriteLine(" ID: " + game.Id + " Game: " + game.Label);

       int address = game.CommunicationEndPoint.Address;
       int port = game.CommunicationEndPoint.Port;

       startJoinGameConversation(game.Id, new EndPoint(address, port));
     }

     public void askUserForGame()
     {
       GameRegistry gameRegistry = new GameRegistry();
       gameRegistry.displayAvailableGames();

       Console.Write("Enter the id of the game you want to play: ");
       short gameId = short.Parse(Console.ReadLine());

       AgentCommon.Registrar.GameInfo game = gameRegistry.getGameInfoById(gameId);

       int address = game.CommunicationEndPoint.Address;
       int port = game.CommunicationEndPoint.Port;

       EndPoint endPoint = new EndPoint(address, port);

       startJoinGameConversation(gameId, endPoint);
     }

     public void startJoinGameConversation(short gameId, EndPoint endPoint)
     {
       JoinGame joinGame = new JoinGame(gameId, this.Info);
       Envelope envelope = new Envelope(joinGame, endPoint);

       Console.WriteLine("Starting JoinGame conversation...");

       ExecutionStrategy.StartConversation(envelope, this);
     }
  }
}
