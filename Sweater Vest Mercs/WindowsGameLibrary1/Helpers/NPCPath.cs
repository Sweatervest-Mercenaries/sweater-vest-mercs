using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SVMLib.Tiles;

namespace SVMLib.Helpers
{
  public class NPCPath
  {
    public TileEntity Start { get; set; }
    public TileEntity End { get; set; }
    public int ID { get; set; }

    public NPCPath(TileEntity s, TileEntity e)
    {
      Start = s;
      End = e;
      ID = s.PathStartID;
    }
  }
}
