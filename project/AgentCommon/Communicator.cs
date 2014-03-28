using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using Common;
using Messages;

namespace AgentCommon
{
  public class Communicator
  {
    #region Static Members
    private static Dictionary<int, bool> used = new Dictionary<int, bool>();
    private static int currentPort = 12000;

    public static int nextAvailablePort()
    {
      while (used.ContainsKey(currentPort))
        currentPort++;

      used.Add(currentPort, true);

      return currentPort++;
    }

    private static void markPortUsed(int port)
    {
      if (!used.ContainsKey(port))
        used.Add(port, true);
    }
    #endregion

    #region Private Data Members
    private UdpClient udpClient;
    private const int DEFAULT_PORT = 12400;
    private const int TIMEOUT = 500;
    #endregion

    #region Constructors
    public Communicator()
    {
      IPEndPoint localEP = new IPEndPoint(IPAddress.Any, DEFAULT_PORT);
      udpClient = new UdpClient(localEP);
    }

    public Communicator(int port)
    {
      IPEndPoint localEP = new IPEndPoint(IPAddress.Any, port);
      udpClient = new UdpClient(localEP);
      udpClient.Client.ReceiveTimeout = TIMEOUT;

      markPortUsed(port);
    }

    ~Communicator()
    {
      udpClient.Close();
    }
    #endregion

    #region Private Methods
    private string GetIPAddress(Envelope envelope)
    {
      string fullAddress = envelope.endPoint.ToString();

      char[] delimiters = { ':' };
      string[] results = fullAddress.Split(delimiters);

      if (results.Length == 2) return results[0];
      else return "Fail";
    }

    private IPAddress GetHostAddress(string hostName)
    {
      IPAddress result = null;
      IPAddress[] addresses = Dns.GetHostAddresses(hostName);
      if (addresses.Length > 0 && addresses[0] != null)
        result = addresses[0];
      return result;
    }
    #endregion

    public int GetAvailable()
    {
      return udpClient.Available;
    }

    public void Send(Envelope envelope)
    {
      string address = GetIPAddress(envelope);
      int port = envelope.endPoint.Port;

      IPEndPoint remoteEP = new IPEndPoint(GetHostAddress(address), port);

      ByteList bytes = new ByteList();

      envelope.message.Encode(bytes);
      byte[] b = bytes.ToBytes();
      udpClient.Send(bytes.ToBytes(), bytes.Length, remoteEP);
    }

    public Envelope Recieve()
    {
      IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
      byte[] receiveBuffer = udpClient.Receive(ref remoteEP);

      Common.EndPoint endPoint = new Common.EndPoint(remoteEP);

      ByteList bytes = new ByteList(receiveBuffer);


      return new Envelope(Message.Create(bytes), endPoint);
    }
  }
}
