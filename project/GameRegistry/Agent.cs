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
      doer = new Doer(communicator, new BrilliantStudent.BrilliantStudent());
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
      doer = new Doer(communicator, new BrilliantStudent.BrilliantStudent());
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
        StatusMonitor.get().post("");
        StatusMonitor.get().post("Shutting Down...");
        Environment.Exit(0);
      }
       
      StatusMonitor.get().post("Auto choosing game:");
      StatusMonitor.get().post(" ID: " + game.Id + " Game: " + game.Label);

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

      StatusMonitor.get().post("Starting JoinGame conversation...");

      ExecutionStrategy.StartConversation(envelope, new BrilliantStudent.BrilliantStudent());
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
      
      StatusMonitor.get().post("Creating Agent...");
      Agent agent = new Agent(agentType, port);

      StatusMonitor.get().post("Adding strategies...");
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.JoinGame, typeof(JoinGameExecutionStrategy));

      StatusMonitor.get().post("Starting Agent...");
      agent.start();
      //StatusMonitor.get().post("Communicator running on port " + port);

      StatusMonitor.get().post("Picking Game...");
      if (args.Length == 3) agent.askUserForGame();
      else agent.autoPickGame(); 

      MessageQueue requestQueue = RequestMessageQueue.getQueue();

      //StatusMonitor.get().post("");

      //StatusMonitor.get().post("Hit any key to quit...");

      Console.ReadKey(false);

      //agent.stop();
    }
  }
}
