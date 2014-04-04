using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentCommon
{
  public class StatusMonitor
  {
    #region Singelton
    private static StatusMonitor statusMonitor = null;
    public static StatusMonitor get()
    {
      if (statusMonitor == null) statusMonitor = new StatusMonitor();
      return statusMonitor;
    }
    #endregion

    public delegate void StringMethod(string param);

    public event StringMethod postMessageEvent;

    public StatusMonitor() { }

    public void post(string message)
    {
      Console.WriteLine(message);

      if (postMessageEvent != null)
        postMessageEvent(message);
    }
  }
}
