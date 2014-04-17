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

    public event StringMethod debugMessageEvent;
    public event StringMethod statusMessageEvent;

    public StatusMonitor() { }

    public void postDebug(string message)
    {
      Console.WriteLine("DEBUG: " + message);

      if (debugMessageEvent != null) 
        debugMessageEvent(message);         
    }

    public void postStatus(string message)
    {
      Console.WriteLine("STATUS: " + message);

      if (statusMessageEvent != null) 
        statusMessageEvent(message);
    }
  }
}
