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
  class Agent
  {
    #region Private Members
    private Communicator communicator;
    private Listener listener;
    private Doer doer;
    #endregion

    #region Public Members
    public Communicator Communicator { get { return communicator; } }
    public AgentInfo AgentInfo { get; set; }
    #endregion

    #region Constructors
    public Agent(AgentInfo.PossibleAgentType agentType)
    {
      AgentInfo = new AgentInfo();
      AgentInfo.AgentType = agentType;
      AgentInfo.ANumber = "A01072246";
      AgentInfo.FirstName = "Alan";
      AgentInfo.LastName = "Christensen";

      communicator = new Communicator(Communicator.nextAvailablePort());

      listener = new Listener(communicator);
      doer = new Doer(communicator);
    }

    public Agent(AgentInfo.PossibleAgentType agentType, int port)
    {
      AgentInfo = new AgentInfo();
      AgentInfo.AgentType = agentType;
      AgentInfo.ANumber = "A01072246";
      AgentInfo.FirstName = "Alan";
      AgentInfo.LastName = "Christensen";

      this.communicator = new Communicator(port);

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
      Games gameRegistry = new Games();
      Registrar.GameInfo game = gameRegistry.getGameByLabel("alan");

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

    void askUserForGame()
    {
      Games gameRegistry = new Games();
      gameRegistry.displayAvailableGames();

      Console.Write("Enter the id of the game you want to play: ");
      short gameId = short.Parse(Console.ReadLine());

      Registrar.GameInfo game = gameRegistry.getGameInfoById(gameId);

      int address = game.CommunicationEndPoint.Address;
      int port = game.CommunicationEndPoint.Port;

      EndPoint endPoint = new EndPoint(address, port);

      startJoinGameConversation(gameId, endPoint);
    }

    void startJoinGameConversation(short gameId, EndPoint endPoint)
    {
      JoinGame joinGame = new JoinGame(gameId, AgentInfo);
      Envelope envelope = new Envelope(joinGame, endPoint);

      Console.WriteLine("Starting JoinGame conversation...");

      ExecutionStrategy.StartConversation(envelope);
    }

    void stop()
    {
      listener.Stop();
      doer.Stop();
    }

    public static void Run(string[] args)
    {
      int port = 23456;
      AgentInfo.PossibleAgentType agentType = AgentInfo.PossibleAgentType.BrilliantStudent;

      if(args.Length >= 1) port = Convert.ToInt32(args[0]);
      if (args.Length >= 2)
      {
        if (args[1] == "BS")  agentType = AgentInfo.PossibleAgentType.BrilliantStudent;
        else if (args[1] == "WS") agentType = AgentInfo.PossibleAgentType.WhiningSpinner;
        else if (args[1] == "EG") agentType = AgentInfo.PossibleAgentType.ExcuseGenerator;
      }
      
      Console.WriteLine("Creating Agent...");
      Agent agent = new Agent(agentType, port);

      Console.WriteLine("Adding strategies...");
      ExecutionStrategy.addStrategy(Message.MESSAGE_CLASS_IDS.JoinGame, new JoinGameExecutionStrategy(1, agent));

      Console.WriteLine("Starting Agent...");
      agent.start();
      //Console.WriteLine("Communicator running on port " + port);

      Console.WriteLine("Picking Game...");
      if (args.Length == 3) agent.askUserForGame();
      else agent.autoPickGame(); 

      MessageQueue requestQueue = RequestMessageQueue.getQueue();

      //Console.WriteLine();

      //Console.WriteLine("Hit any key to quit...");

      Console.ReadKey(false);

      //agent.stop();
    }
  }
}
