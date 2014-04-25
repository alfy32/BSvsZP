using System;
using System.Collections.Concurrent;
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
    private ConcurrentQueue<Tick> ticks = new ConcurrentQueue<Tick>();
    #endregion

    #region Public Members
    public Communicator Communicator { get { return communicator; } }
    public AgentState State { get { return state; } set { state = value; } }
    public AgentBrain Brain { get { return brain; } }
    public const int MAX_TICKS_TO_KEEP = 150;
    #endregion

    #region Delegates and Events
    public delegate void IntMethod(int param);

    public event IntMethod tickCountEvent;
    public event IntMethod excuseCountEvent;
    public event IntMethod whineCountEvent;
    public event IntMethod closestZombieEvent;

    public void closeZombie(double dist) { if (closestZombieEvent != null) closestZombieEvent((int)dist); }
    #endregion

    #region Ticks
    public Tick getTickFromStash()
    {
      Tick tick;
      ticks.TryDequeue(out tick);

      if (tickCountEvent != null) tickCountEvent(ticks.Count);

      return tick;
    }
    public void stashTick(Tick tick)
    {
      ticks.Enqueue(tick);
      if (ticks.Count > MAX_TICKS_TO_KEEP)
      {
        Tick tempTick;
        while (!ticks.TryDequeue(out tempTick)) ;
      }

      if (tickCountEvent != null) tickCountEvent(ticks.Count);
    }
    public int getTickCount() { return ticks.Count; }
    #endregion

    #region Excuses
    private ConcurrentQueue<Excuse> excuses = new ConcurrentQueue<Excuse>();
    
    public bool ExcuseAvailable()
    {
      return excuses.Count != 0;
    }
    public Excuse getExcuse()
    {
      if (ExcuseAvailable())
      {
        Excuse excuse = null;
        while (ExcuseAvailable() && !excuses.TryDequeue(out excuse)) ;
        return excuse;
      }
      else
      {
        return null;
      }
    }
    public void addExcuse(Excuse excuse)
    {
      excuses.Enqueue(excuse);
      if (this.excuseCountEvent != null) excuseCountEvent(getExcuseCount());
    }
    public int getExcuseCount()
    {
      return excuses.Count;
    }
    #endregion

    #region Whines
    public ConcurrentQueue<WhiningTwine> twine = new ConcurrentQueue<WhiningTwine>();
    
    public bool TwineAvailable()
    {
      return twine.Count != 0;
    }
    public WhiningTwine getTwine()
    {
      if (TwineAvailable())
      {
        WhiningTwine twine = null;
        while (TwineAvailable() && !this.twine.TryDequeue(out twine)) ;
        return twine;
      }
      else
      {
        return null;
      }
    }
    public void addTwine(WhiningTwine twine)
    {
      this.twine.Enqueue(twine);
      if (this.whineCountEvent != null) whineCountEvent(getTwineCount());
    }
    public int getTwineCount()
    {
      return this.twine.Count;
    }
    #endregion

    #region Constructors
    public Agent(int port = -1)
    {
      GameRegistry gameRegistry = new GameRegistry();
      MessageNumber.LocalProcessId = gameRegistry.getProcessId();

      ExecutionStrategy updateStreamStrategy = new StrategyAgentUpdateStream(this);
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.StartUpdateStream, updateStreamStrategy);
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.EndUpdateStream, updateStreamStrategy);
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.ChangeStrength, new StrategyChangeStrength(this));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.Collaborate, new StrategyCollaborate(this));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.EndGame, new StrategyEndGame(this));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.GetResource + 1000 * (int)GetResource.PossibleResourceType.BrillianStudentList, new StrategyGetBSList(this));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.GetResource + 1000 * (int)GetResource.PossibleResourceType.GameConfiguration, new StrategyGetConfiguration(this));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.GetResource + 1000 * (int)GetResource.PossibleResourceType.ExcuseGeneratorList, new StrategyGetEGList(this));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.GetResource + 1000 * (int)GetResource.PossibleResourceType.PlayingFieldLayout, new StrategyGetLayout(this));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.GetResource, new StrategyGetResource(this));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.GetStatus, new StrategyGetStatus(this));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.TickDelivery, new StrategyGetTick(this));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.GetResource + 1000 * (int)GetResource.PossibleResourceType.WhiningSpinnerList, new StrategyGetWSList(this));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.GetResource + 1000 * (int)GetResource.PossibleResourceType.ZombieProfessorList, new StrategyGetZPList(this));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.JoinGame, new StrategyJoinGame(this));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.StartGame, new StrategyStartGame(this));
      ExecutionStrategy.addStrategy((int)Message.MESSAGE_CLASS_IDS.ExitGame, new StrategyExitGame(this));

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
      GameInfo game = gameRegistry.getGameByLabel("alan");

      if (game == null)
      {
        Console.Write("There are no games to join. Press any key to quit...");
        Console.ReadKey(false);
        StatusMonitor.get().postDebug("");
        StatusMonitor.get().postDebug("Shutting Down...");
        Environment.Exit(0);
      }

      StatusMonitor.get().postDebug("Auto choosing game:");
      StatusMonitor.get().postDebug(" ID: " + game.Id + " Game: " + game.Label);

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

      GameInfo game = gameRegistry.getGameInfoById(gameId);

      int address = game.CommunicationEndPoint.Address;
      int port = game.CommunicationEndPoint.Port;

      EndPoint endPoint = new EndPoint(address, port);

      startJoinGameConversation(gameId, endPoint);
    }

    public void pickGameByLabel(string label)
    {
      GameRegistry gameRegistry = new GameRegistry();
      GameInfo game = gameRegistry.getGameByLabel(label);

      if (game == null)
      {
        Console.Write("There are no games to join. Press any key to quit...");
        Console.ReadKey(false);
        StatusMonitor.get().postDebug("");
        StatusMonitor.get().postDebug("Shutting Down...");
        Environment.Exit(0);
      }

      StatusMonitor.get().postDebug("Joining Game ID: " + game.Id + " Game: " + game.Label);

      int address = game.CommunicationEndPoint.Address;
      int port = game.CommunicationEndPoint.Port;

      startJoinGameConversation(game.Id, new EndPoint(address, port));
    }

    public void startJoinGameConversation(short gameId, EndPoint endPoint)
    {
      JoinGame joinGame = new JoinGame(gameId, this.State.AgentInfo);
      Envelope envelope = new Envelope(joinGame, endPoint);

      StatusMonitor.get().postDebug("Starting JoinGame conversation...");

      ExecutionStrategy.StartConversation(envelope);
    }
    #endregion
  }
}
