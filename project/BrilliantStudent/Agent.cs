using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentCommon;
using Messages;
using Common;
using GameServer;

namespace BrilliantStudent
{
  class Agent
  {
    #region Private Members
    private Communicator communicator;
    private Listener listener;
    private Doer doer;
    #endregion

    #region Constructors
    public Agent()
    {
      communicator = new Communicator(Communicator.nextAvailablePort());

      listener = new Listener(communicator);
      doer = new Doer(communicator);
    }

    public Agent(int port)
    {
      communicator = new Communicator(port);

      listener = new Listener(communicator);
      doer = new Doer(communicator);
    }
    #endregion

    void start()
    {
      listener.Start();
      doer.Start();
    }

    void autoPickGame()
    {
      GameRegistry gameRegistry = new GameRegistry();
      GameServer.Registrar.GameInfo game = gameRegistry.getRandomGame();

      int address = game.CommunicationEndPoint.Address;
      int port = game.CommunicationEndPoint.Port;

      startJoinGameConversation(game.Id, new EndPoint(address, port));
    }

    void askUserForGame()
    {
      GameRegistry gameRegistry = new GameRegistry();
      gameRegistry.displayAvailableGames();

      Console.Write("Enter the id of the game you want to play: ");
      short gameId = short.Parse(Console.ReadLine());

      GameServer.Registrar.GameInfo game = gameRegistry.getGameInfoById(gameId);

      int address = game.CommunicationEndPoint.Address;
      int port = game.CommunicationEndPoint.Port;

      EndPoint endPoint = new EndPoint(address, port);

      startJoinGameConversation(gameId, endPoint);
    }

    void startJoinGameConversation(short gameId, EndPoint endPoint)
    {
      AgentInfo agentInfo = new Common.AgentInfo(10, AgentInfo.PossibleAgentType.BrilliantStudent);
      JoinGame joinGame = new JoinGame(gameId, agentInfo);
      Envelope envelope = new Envelope(joinGame, endPoint);

      communicator.Send(envelope);
    }

    void stop()
    {
      listener.Stop();
      doer.Stop();
    }

    static void Main(string[] args)
    {
      int port = 23456;

      if(args.Length >= 1) port = Convert.ToInt32(args[0]);

      Agent agent = new Agent(port);

      agent.start();

      if (args.Length == 2) agent.askUserForGame();
      else agent.autoPickGame(); 

      MessageQueue requestQueue = RequestMessageQueue.getQueue();

      Console.WriteLine("Agent Started...");
      Console.WriteLine();
      Console.WriteLine("Communicator running on port " + port);
      Console.WriteLine();

      Console.WriteLine("Hit any key to quit...");

      Console.ReadKey(false);

      agent.stop();
    }
  }
}
