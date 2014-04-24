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
    public StrategyGetConfiguration(Agent agent)
      : base(agent) { }

    private void sendMessage(Envelope envelope)
    {
      GetResource getResource = (GetResource)envelope.message;
      getResource.GameId = agent.State.AgentInfo.Id;
      getResource.EnablingTick = agent.getTickFromStash();

      StatusMonitor.get().postDebug("Sent Get " + getResource.GetResourceType.ToString() + " Message.");
      agent.Communicator.Send(envelope);
    }

    private void handleResponse(Envelope envelope)
    {
      if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.ConfigurationReply)
      {
        ConfigurationReply configurationReply = (ConfigurationReply)envelope.message;
        if (configurationReply.Status == Reply.PossibleStatus.Success)
        {
          StatusMonitor statusMonitor = StatusMonitor.get();
          statusMonitor.postDebug("Recieved configuration reply");
          agent.State.GameConfiguration = configurationReply.Configuration;
        }
        else
        {
          StatusMonitor.get().postDebug("Failed to get Configuration");
        }
      }
    }
    public override void Execute(Object startEnvelope)
    {
      Envelope envelope = (Envelope)startEnvelope;
      MessageQueue messageQueue = ConversationMessageQueues.getQueue(envelope.message.ConversationId);
      if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.GetResource)
      {
        sendMessage(envelope);

        while (!messageQueue.hasItems())
          System.Threading.Thread.Sleep(10);

        handleResponse(messageQueue.pop());
      }
    }
  }
}
