﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AgentCommon;

namespace WhiningSpinner
{
  class Brain : BackgroundThread
  {
    public override string ThreadName()
    {
      return "Brain";
    }

    protected override void Process()
    {
      while (keepGoing)
      {
        while (keepGoing && !suspended)
        {
          //This is the active AI portion of the whining spinner


          // If no bombs lets start requesting some materials.

          // This is to slow down the work a little. 
          // The while loops just keep going like crazy without this.
          System.Threading.Thread.Sleep(10);
        }
      }
    }
  }
}