﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;
using Common;

namespace AgentCommon
{
  public class StrategyGetConfiguration : ExecutionStrategy
  {
    private Agent agent;

    public StrategyGetConfiguration(int conversationId, Agent agent)
      : base(conversationId)
    {
      this.agent = agent;
    }

    private void sendMessage(Envelope envelope)
    {
      GetResource getResource = (GetResource)envelope.message;
      getResource.GameId = agent.State.getAgentInfo().Id;
      getResource.EnablingTick = agent.getTickFromStash();

      StatusMonitor.get().post("Sent Get " + getResource.GetResourceType.ToString() + " Message.");
      agent.Communicator.Send(envelope);
    }

    private void handleResponse(Envelope envelope)
    {
      if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.ConfigurationReply)
      {
        ConfigurationReply configuration = (ConfigurationReply)envelope.message;
        if (configuration.Status == Reply.PossibleStatus.Success)
        {
          StatusMonitor statusMonitor = StatusMonitor.get();
          statusMonitor.post("Recieved configuration reply");
          GameConfiguration config = configuration.Configuration;

          //TODO: save configuration on agent
          statusMonitor.post(" Zombie Creation Rate: " + config.ZombieCreationRate);
        }
        else
        {
          StatusMonitor.get().post("Failed to get Configuration");
        }
      }
    }
    protected override void Execute()
    {
      if (messageQueue.hasItems())
      {
        Envelope envelope = messageQueue.pop();
        if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.GetResource)
        {
          sendMessage(envelope);

          while (!messageQueue.hasItems())
            System.Threading.Thread.Sleep(10);

          handleResponse(messageQueue.pop());
        }
        Stop();
      }
    }
  }
}
