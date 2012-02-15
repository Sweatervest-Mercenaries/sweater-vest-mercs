using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SVMLib.Entities;
using Microsoft.Xna.Framework;

namespace SVMLib.Visuals
{
  public class PositionAnimation : Animation
  {
    public Entity Lead { get; set; }
    public Vector2 Position { get { return Lead.Position; } set { Lead.Position = value; } }

    public PositionAnimation(Vector2 pos) : base()
    {
      Lead = new Entity();
      Position = new Vector2(pos.X, pos.Y);
      _lead = Lead;
    }

    public void Center(Vector2 center)
    {
      Lead.Shift(center);
    }
  }
}
