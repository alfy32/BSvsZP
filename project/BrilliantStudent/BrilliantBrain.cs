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
  public class BrilliantBrain : AgentBrain
  {
    Random random = new Random();
    public BrilliantBrain(Agent agent) : base(agent) { }
    protected override void Think()
    {
      //Check for stuff and update
      StatusMonitor statusMonitor = StatusMonitor.get();

      statusMonitor.postDebug("I'm thinking...");

      if (agent.State != null && agent.State.GameConfiguration != null)
      {
        GameConfiguration gameConfiguration = agent.State.GameConfiguration;

        AgentInfo closestZombie = getClosestZombie();
        bool moving = false;

        if (closestZombie != null)
        {
          double dist = FieldLocation.Distance(agent.State.AgentInfo.Location, closestZombie.Location);
          statusMonitor.postDebug("Closest Zombie: " + dist);
          agent.closeZombie(dist);

          if (dist < 10)
          {
            FieldLocation location = directionToRun(agent.State.AgentInfo.Location, closestZombie.Location);
            move(location);
            moving = true;
          }
        }

        if (!moving)
        {
         
          int choice = random.Next(1, 3);
          if (choice == 1 && agent.getTickCount() > 20)
          {
            AgentInfo eg = agent.State.AgentList.FindClosestToLocation(agent.State.AgentInfo.Location, AgentInfo.PossibleAgentType.ExcuseGenerator);
            if (eg != null) getExcuse(eg.CommunicationEndPoint);            
          }

          if (choice == 2 && agent.getTickCount() > 20) { 
            AgentInfo ws = agent.State.AgentList.FindClosestToLocation(agent.State.AgentInfo.Location, AgentInfo.PossibleAgentType.WhiningSpinner);
            if(ws != null) getWhine(ws.CommunicationEndPoint);
          }

          if (agent.getExcuseCount() > 4 && agent.getTwineCount() < 4 && agent.getTickCount() > 10)
          {
            makeAndThrowBomb();
          }

        }

        System.Threading.Thread.Sleep(gameConfiguration.TickInterval);
      }
      else
      {
        statusMonitor.postDebug("no configuration...");
        System.Threading.Thread.Sleep(2000);
      }
    }

    #region Thoughts
    public void getExcuse(EndPoint endPoint)
    {
      Tick tick = agent.getTickFromStash();
      if (tick == null)
      {
        StatusMonitor.get().postDebug("I don't have any ticks :(");
        return;
      }
      GetResource getResource = new GetResource(agent.State.AgentInfo.Id, GetResource.PossibleResourceType.Excuse);
      getResource.EnablingTick = tick;
      Envelope envelope = new Envelope(getResource, endPoint);
      ExecutionStrategy.StartConversation(envelope);
    }

    public void getWhine(EndPoint endPoint)
    {
      Tick tick = agent.getTickFromStash();
      if (tick == null)
      {
        StatusMonitor.get().postDebug("I don't have any ticks :(");
        return;
      }
      GetResource getResource = new GetResource(agent.State.AgentInfo.Id, GetResource.PossibleResourceType.WhiningTwine);
      getResource.EnablingTick = tick;
      Envelope envelope = new Envelope(getResource, endPoint);
      ExecutionStrategy.StartConversation(envelope);
    }
    public void gotExcuse(Excuse excuse)
    {
      StatusMonitor.get().postDebug("I got an excuse. I don't know what to do with it.");
      agent.addExcuse(excuse);
    }
    public void gotTwine(WhiningTwine twine)
    {
      StatusMonitor.get().postDebug("I got whining twine. I don't know what to do with it.");
      agent.addTwine(twine);
    }

    public void makeAndThrowBomb()
    {
      List<Excuse> excuses = new List<Excuse>();
      List<WhiningTwine> twine = new List<WhiningTwine>();

      excuses.Add(agent.getExcuse());
      excuses.Add(agent.getExcuse());
      excuses.Add(agent.getExcuse());
      excuses.Add(agent.getExcuse());

      twine.Add(agent.getTwine());
      twine.Add(agent.getTwine());
      twine.Add(agent.getTwine());
      twine.Add(agent.getTwine());

      Bomb bomb = new Bomb(agent.State.AgentInfo.Id, excuses, twine, agent.getTickFromStash());

      AgentInfo zombie = agent.State.AgentList.FindClosestToLocation(agent.State.AgentInfo.Location, AgentInfo.PossibleAgentType.ZombieProfessor);

      if (zombie != null) throwBomb(bomb, zombie.Location);
    }

    public void throwBomb(Bomb bomb, FieldLocation location)
    {
      ThrowBomb throwBomb = new ThrowBomb(agent.State.AgentInfo.Id, bomb, location, agent.getTickFromStash());
      Envelope envelope = new Envelope(throwBomb, agent.State.GameEndPoint);
      ExecutionStrategy.StartConversation(envelope);
    }
    #endregion
  }
}
