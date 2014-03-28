using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Messages;
using Common;

namespace AgentCommon
{
    public class Envelope
    {
        #region Public Properties
        public Message message { get; set; }
        public EndPoint endPoint { get; set; }
        #endregion

        #region Constructors
        public Envelope() { }

        public Envelope(Message message, EndPoint endPoint)
        {
            this.message = message;
            this.endPoint = endPoint;
        }
        #endregion
    }
}
