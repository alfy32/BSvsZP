using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common;

namespace BrilliantStudent
{
  class BombBuilder
  {
    #region Singelton
    private static BombBuilder bombBuilder = null;
    public static BombBuilder getBombBuilder()
    {
      if (bombBuilder == null) bombBuilder = new BombBuilder();
      return bombBuilder;
    }
    #endregion

    private List<Bomb> bombs = new List<Bomb>();

    public BombBuilder() { }

    public void buildBomb(List<Excuse> excuses, List<WhiningTwine> whines, Tick tick)
    {
      bombs.Add(new Bomb(10, excuses, whines, tick));
    }

    public Bomb throwBomb(int index)
    {
      return bombs.ElementAt(index);
    }
  }
}
