﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;

namespace AgentCommon
{
  public class Doer : BackgroundThread
  {
    private Communicator communicator;
    private Agent agent;

    public Doer(Communicator communicator, Agent agent)
    {
      this.communicator = communicator;
      this.agent = agent;
    }

    public override string ThreadName()
    {
      return "Doer";
    }

    protected override void Process()
    {
      MessageQueue requestMessageQueue = RequestMessageQueue.getQueue();

      while (keepGoing)
      {
        while (keepGoing && !suspended)
        {
          if (requestMessageQueue.hasItems())
          {
            ExecutionStrategy.StartConversation(requestMessageQueue.pop());
          }
          System.Threading.Thread.Sleep(1);
        }
      }
    }
  }
}
