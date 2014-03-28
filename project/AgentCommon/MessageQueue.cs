using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentCommon
{
    public class MessageQueue
    {
        ConcurrentQueue<Envelope> queue;

        public MessageQueue()
        {
            queue = new ConcurrentQueue<Envelope>();
        }

        public void push(Envelope envelope)
        {
            queue.Enqueue(envelope);
        }

        public Envelope pop()
        {
            Envelope envelope = new Envelope();

            queue.TryDequeue(out envelope);

            return envelope;
        }

        public bool hasItems()
        {
            return !queue.IsEmpty;
        }
    }
}
