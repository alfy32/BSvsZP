using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace ExcuseGenerator
{
  class ExcuseBuilder
  {
    #region Singleton Area
    private static ExcuseBuilder excuseBuilder;
    public static ExcuseBuilder getExcuseBuilder()
    {
      if (excuseBuilder == null)
        excuseBuilder = new ExcuseBuilder();

      return excuseBuilder;
    }
    #endregion

    private List<Excuse> excuses = new List<Excuse>();
    private List<Tick> ticks = new List<Tick>();
    public ExcuseBuilder() { }

    public void startNewExcuse(Tick tick) {
      Excuse excuse = new Excuse(10, ticks, tick);
      excuses.Add(excuse);
    }

    public Excuse getExcuse()
    {
      return excuses.ElementAt(0);
    }
  }
}
