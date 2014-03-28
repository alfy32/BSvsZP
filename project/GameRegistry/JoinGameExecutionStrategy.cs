using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentCommon;
using Messages;

namespace GameRegistry
{
  class JoinGameExecutionStrategy: ExecutionStrategy
  {
    public JoinGameExecutionStrategy(int conversationId)
      : base(conversationId) { }

    protected override void Execute()
    {
      if (messageQueue.hasItems())
      {
        Envelope envelope = messageQueue.pop();

        JoinGame joinGame = (JoinGame)envelope.message;

        Communicator communicator = new Communicator();
        communicator.Send(envelope);
      }
    }

  }
}
