using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SVMLib;
using SVMLib.Entities;

namespace SVMLib.Visuals
{
  public enum Effect_Type
  {
    Fade, Fade_Out, Fade_In
  }

  public class EntityEffect : MovingEntity
  {
    public Animation Ani { get; set; }
    public Effect_Type Type { get; set; }
    public int Alpha { get; set; }
    private int AlphaMod { get; set; }

    public EntityEffect(Animation ani, Effect_Type type) : base()
    {
      Ani = ani;
      Type = type;
      Alpha = ani.Col.A;

      if (Type == Effect_Type.Fade_In)
        Alpha = GameConstants.ALPHA_MIN;
      else
        Alpha = GameConstants.ALPHA_MAX;

      AlphaMod = 1;
    }

    public override void Draw(SpriteBatch batch)
    {
      if (Ani is PositionAnimation)
      {
        ((PositionAnimation)Ani).Position = Position;
      }
      Ani.Draw(batch);
    }

    public override void Animate()
    {
      Ani.Animate();
    }

    public override void Shift(Vector2 center)
    {
      if (Ani is PositionAnimation)
        ((PositionAnimation)Ani).Center(center);

      base.Shift(center);
    }

    public override void Tick()
    {
      if (Type == Effect_Type.Fade_Out && Alpha > GameConstants.ALPHA_MIN)
      {
        Alpha -= GameConstants.ALPHA_STEP * AlphaMod;
        Ani.Col = new Color(Ani.Col.R, Ani.Col.G, Ani.Col.B, Alpha);
      }
      else if (Type == Effect_Type.Fade_In && Alpha < GameConstants.ALPHA_MAX)
      {
        Alpha += GameConstants.ALPHA_STEP * AlphaMod;
        Ani.Col = new Color(Ani.Col.R, Ani.Col.G, Ani.Col.B, Alpha);
      }
      else if (Type == Effect_Type.Fade)
      {
        if (Alpha <= GameConstants.ALPHA_MIN || Alpha >= GameConstants.ALPHA_MAX)
          AlphaMod *= -1;

        Alpha += GameConstants.ALPHA_STEP * AlphaMod;
        Ani.Col = new Color(Ani.Col.R, Ani.Col.G, Ani.Col.B, Alpha);
      }
    }
  }
}