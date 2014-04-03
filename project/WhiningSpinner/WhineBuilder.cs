using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace WhiningSpinner
{
  class WhineBuilder
  {
    #region Singleton Area
    private static WhineBuilder whineBuilder = null;

    public static WhineBuilder getWhineBuilder()
    {
      if (whineBuilder == null) whineBuilder = new WhineBuilder();
      return whineBuilder;
    }
    #endregion

    private List<WhiningTwine> twine = new List<WhiningTwine>();

    public WhineBuilder() { }

    public void buildTwine(List<Tick> ticks, Tick tick) {
      twine.Add(new WhiningTwine(10, ticks, tick));
    }
  }
}
