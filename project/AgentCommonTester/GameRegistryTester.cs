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
    public void GameRegisrty_CanGetGames()
    {
      GameRegistry gameRegistry = new GameRegistry();
      List<string> gameLabels = gameRegistry.getAvailableGameList();

      Assert.IsNotNull(gameLabels);
    }

    [TestMethod]
    public void GameRegistry_GetProcessId_unique()
    {
      GameRegistry gameRegistry = new GameRegistry();

      short pid = gameRegistry.getProcessId();
      short pid2 = gameRegistry.getProcessId();

      Assert.IsTrue(pid != pid2);
    }
  }
}
