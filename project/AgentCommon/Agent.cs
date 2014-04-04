using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentCommon;
using Messages;
using Common;

namespace AgentCommon
{
  public abstract class Agent
  {
    #region Private Members
    private Communicator communicator;
    private Listener listener;
    private Doer doer;
    protected AgentBrain brain;
    protected AgentState state;
    #endregion

    #region Public Members
    public Communicator Communicator { get { return communicator; } }
    public AgentState State { get { return state; } set { state = value; } }
    public AgentBrain Brain { get { return brain; } }
    #endregion

    public Tick getTickFromStash()
    {
      //TODO
      return new Tick();
    }
    public void stashTick(Tick tick)
    {
      //TODO
    }

    #region Constructors
    public Agent(int port = -1)
    {
      //ExecutionStrategy.addStrategy(Message.MESSAGE_CLASS_IDS.StartUpdateStream, typeof(StrategyStartUpdateStream));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.ChangeStrength, typeof(StrategyChangeStrength));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.Collaborate, typeof(StrategyCollaborate));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.EndGame, typeof(StrategyEndGame));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.GetResource + 1000 * (int)GetResource.PossibleResourceType.BrillianStudentList, typeof(StrategyGetBSList));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.GetResource + 1000 * (int)GetResource.PossibleResourceType.GameConfiguration, typeof(StrategyGetConfiguration));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.GetResource + 1000 * (int)GetResource.PossibleResourceType.ExcuseGeneratorList, typeof(StrategyGetEGList));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.GetResource + 1000 * (int)GetResource.PossibleResourceType.PlayingFieldLayout, typeof(StrategyGetLayout));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.GetResource, typeof(StrategyGetResource));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.GetStatus, typeof(StrategyGetStatus));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.TickDelivery, typeof(StrategyGetTick));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.GetResource + 1000 * (int)GetResource.PossibleResourceType.WhiningSpinnerList, typeof(StrategyGetWSList));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.GetResource + 1000 * (int)GetResource.PossibleResourceType.ZombieProfessorList, typeof(StrategyGetZPList));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.JoinGame, typeof(StrategyJoinGame));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.StartGame, typeof(StrategyStartGame));

      if (port == -1) port = Communicator.nextAvailablePort();

      communicator = new Communicator(port);

      listener = new Listener(communicator);
      doer = new Doer(communicator, this);

      listener.Start();
      doer.Start();
    }

    ~Agent()
    {
      listener.Stop();
      doer.Stop();
    }
    #endregion

    #region Join Game Stuff
    public void autoPickGame()
    {
      GameRegistry gameRegistry = new GameRegistry();
      AgentCommon.Registrar.GameInfo game = gameRegistry.getGameByLabel("alan");

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
      JoinGame joinGame = new JoinGame(gameId, this.State.getAgentInfo());
      Envelope envelope = new Envelope(joinGame, endPoint);

      StatusMonitor.get().post("Starting JoinGame conversation...");

      ExecutionStrategy.StartConversation(envelope, this);
    }
    #endregion
  }
}
