using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;
using Common;

namespace AgentCommon
{
  public class StrategyStartGame : ExecutionStrategy
  {
    public StrategyStartGame(Agent agent)
      : base(agent) { }

    public override void Execute(Object startEnvelope)
    {
      Envelope envelope = (Envelope)startEnvelope;
      MessageQueue messageQueue = ConversationMessageQueues.getQueue(envelope.message.ConversationId);
      if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.StartGame)
      {
        StartGame startGame = (StartGame)envelope.message;
        StatusMonitor.get().postStatus("Recieved StartGame message.");

        ReadyReply ready = new ReadyReply(Reply.PossibleStatus.Success);
        ready.ConversationId = startGame.ConversationId;
        agent.Communicator.Send(new Envelope(ready, envelope.endPoint));
        StatusMonitor.get().postDebug("Sent Ready message.");

        while (!messageQueue.hasItems())
          System.Threading.Thread.Sleep(1);

        Envelope response = messageQueue.pop();
        if (response.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.AckNak)
        {
          StatusMonitor.get().postStatus("Recieved Proceed Message.");

          AgentInfo agentInfo = agent.State.AgentInfo;
          agentInfo.AgentStatus = AgentInfo.PossibleAgentStatus.InGame;

          agent.State.AgentInfo = agentInfo;

          agent.Brain.Start();

          agent.Brain.getResource(GetResource.PossibleResourceType.GameConfiguration);
          agent.Brain.getResource(GetResource.PossibleResourceType.PlayingFieldLayout);

          agent.Brain.startUpdateStream();
        }
      }
    }
  }
}
