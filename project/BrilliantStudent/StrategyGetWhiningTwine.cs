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
  public class StrategyGetWhiningTwine : ExecutionStrategy
  {
    private Agent agent;

    public StrategyGetWhiningTwine(int conversationId, Agent agent)
      : base(conversationId)
    {
      this.agent = agent;
    }

    protected override void Execute()
    {
      if (messageQueue.hasItems())
      {
        Envelope envelope = messageQueue.pop();
        if (envelope.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.GetResource)
        {
          StatusMonitor.get().postDebug("Getting Twine...");
          agent.Communicator.Send(envelope);

          while (!messageQueue.hasItems())
            System.Threading.Thread.Sleep(1);

          Envelope response = messageQueue.pop();

          if (response.message.MessageTypeId() == Message.MESSAGE_CLASS_IDS.ResourceReply)
          {
            ResourceReply reply = (ResourceReply)response.message;
            if (reply.Status == Reply.PossibleStatus.Success)
            {
              StatusMonitor.get().postDebug("Recieved twine");
              WhiningTwine twine = (WhiningTwine)reply.Resource;

              ((BrilliantBrain)agent.Brain).gotTwine(twine);
            }
            else
            {
              StatusMonitor.get().postDebug("Failed to get twine: " + reply.Note);
            }
          }
        }
        Stop();
      }
    }
  }
}
