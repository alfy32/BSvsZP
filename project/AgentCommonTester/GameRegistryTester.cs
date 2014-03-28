using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using AgentCommon;

namespace AgentCommonTester
{
  [TestClass]
  public class GameRegistryTester
  {
    [TestMethod]
    public void TestMethod1()
    {
      GameRegistry gameRegistry = new GameRegistry();
      List<string> gameLabels = gameRegistry.getAvailableGameList();
    }
  }
}
