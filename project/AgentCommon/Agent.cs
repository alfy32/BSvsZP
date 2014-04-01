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
  public class Agent
  {
    #region Private Members
    private Communicator communicator;
    private Listener listener;
    private Doer doer;
    #endregion

    #region Public Members
    public Communicator Communicator { get { return communicator; } }
    public AgentInfo Info { get; set; }
    #endregion

    #region Constructors
    public Agent(AgentInfo.PossibleAgentType agentType, int port = -1)
    {
      Info = new AgentInfo();
      Info.AgentType = agentType;
      Info.ANumber = "A01072246";
      Info.FirstName = "Alan";
      Info.LastName = "Christensen";

      if (port == -1) port = Communicator.nextAvailablePort();

      communicator = new Communicator(port);

      listener = new Listener(communicator);
      doer = new Doer(communicator);

      listener.Start();
      doer.Start();
    }

    ~Agent()
    {
      listener.Stop();
      doer.Stop();
    }
    #endregion

  }
}
