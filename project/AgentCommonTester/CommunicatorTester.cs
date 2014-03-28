using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


using System.Net;
using System.Collections.Generic;

using AgentCommon;
using Messages;
using Common;

namespace AgentCommonTester
{
    [TestClass]
    public class CommunicatorTester
    {
        [TestMethod]
        public void Communicator_CheckInitialize()
        {
          Communicator com = new Communicator();

          Communicator com2 = new Communicator(Communicator.nextAvailablePort());
        }

        [TestMethod]
        public void Communicator_SendRecieve()
        {
          int senderPort = Communicator.nextAvailablePort();
          int recieverPort = Communicator.nextAvailablePort();

          Communicator sender = new Communicator(senderPort);
          Communicator reciever = new Communicator(recieverPort);

          AgentInfo agentInfo = new AgentInfo();
          agentInfo.ANumber = "A00123";
          agentInfo.FirstName = "Joe";
          agentInfo.LastName = "Jones";

          Message message = new JoinGame(10, agentInfo);
          IPEndPoint localEP = new IPEndPoint(IPAddress.Loopback, recieverPort);
          Common.EndPoint endPoint = new Common.EndPoint(localEP);
          Envelope sendEnvelope = new Envelope(message, endPoint);

          sender.Send(sendEnvelope);

          Envelope recieveEnvelope = reciever.Recieve();

          JoinGame jg = (JoinGame)recieveEnvelope.message;

          Assert.AreEqual(10, jg.GameId);
          Assert.AreEqual("A00123", jg.AgentInfo.ANumber);
          Assert.AreEqual("Joe", jg.AgentInfo.FirstName);
          Assert.AreEqual("Jones", jg.AgentInfo.LastName);
        }

        [TestMethod]
        public void Communicator_SendToGreg()
        {
          int sendPort = 2345;
          int recieverPort = 9876;

          short gameId = 10;

          string hostName = "thingsforreasons.com";

          Communicator sender = new Communicator(sendPort);

          AgentInfo agentInfo = new AgentInfo();
          Message message = new JoinGame(gameId, agentInfo);
          IPEndPoint recieverEP = new IPEndPoint(GetHostAddress(hostName), recieverPort);
          Common.EndPoint endPoint = new Common.EndPoint(recieverEP);
          Envelope sendEnvelope = new Envelope(message, endPoint);

          sender.Send(sendEnvelope);
        }

        [TestMethod]
        public void Communicator_ListenToGreg()
        {
            int recieverPort = 23456;

            Communicator reciever = new Communicator(recieverPort);

            Listener listener = new Listener(reciever);

            MessageQueue messageQueue = RequestMessageQueue.getQueue();

            listener.Start();

            listener.Stop();

            
        }

        static private IPAddress GetHostAddress(string hostName)
        {
            IPAddress result = null;
            IPAddress[] addresses = Dns.GetHostAddresses(hostName);
            if (addresses.Length > 0 && addresses[0] != null)
                result = addresses[0];
            return result;
        }
    }
}
