using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using AgentCommon;

namespace AgentCommonTester
{
    [TestClass]
    public class BackgroundThreadTester
    {
        [TestMethod]
        public void BackgroundThread_StartStop()
        {
            BackgroundThreadImpl thread = new BackgroundThreadImpl();

            thread.Start();

            thread.Suspend();

            thread.Resume();

            thread.Stop();
        }

        private class BackgroundThreadImpl : BackgroundThread
        {
            public override string ThreadName()
            {
                return "Lstener";
            }

            protected override void Process()
            {
                Console.WriteLine("I'm processing...");
            }
        }
    }
}
