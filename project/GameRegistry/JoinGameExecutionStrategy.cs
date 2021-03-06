﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentCommon;
using Messages;
using Common;

namespace GameRegistry
{
  class JoinGameExecutionStrategy: ExecutionStrategy
  {
    Agent agent;

    public JoinGameExecutionStrategy(int conversationId, Agent agent)
      : base(conversationId)
    {
      this.agent = agent;
    }

    protected override void Execute()
    {
      if (messageQueue.hasItems())
      {
        Envelope envelope = messageQueue.pop();

        JoinGame joinGame = (JoinGame)envelope.message;

        agent.Communicator.Send(envelope);

        while (!messageQueue.hasItems())
        {
          System.Threading.Thread.Sleep(100);
        }

        envelope = messageQueue.pop();
        AckNak ackNak = (AckNak)envelope.message;

        if (ackNak.Status == Reply.PossibleStatus.Success)
        {
          StatusMonitor.get().post("Successfully joined a game...");

          AgentInfo resultAgentInfo = (AgentInfo)ackNak.ObjResult;
          StatusMonitor.get().post(" Status: " + resultAgentInfo.AgentStatus);
          StatusMonitor.get().post(" Location: " + resultAgentInfo.Location);
          StatusMonitor.get().post(" Strength: " + resultAgentInfo.Strength);
          StatusMonitor.get().post("");
        }
        else
        {
          StatusMonitor.get().post("Failed to join the game...");
          StatusMonitor.get().post(ackNak.Message);
        }

        Stop();
      }
    }

  }
}
