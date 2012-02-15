using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SVMLib.Actions;
using SVMLib.Visuals;

namespace SVMLib.Entities
{
  public class AnimatedEntity : MovingEntity
  {
    protected Animation[] Animations { get; set; }
    public const int NUM_ACTIONS = 11;
    public Animation_Type CurrentAction { get; set; }
    
    public AnimatedEntity() : base()
    {
      setupAnimations();
      CurrentAction = Animation_Type.Stand;
    }

    public AnimatedEntity(Texture2D tex) : base(tex)
    {
      setupAnimations();
      CurrentAction = Animation_Type.Stand;
    }

    public AnimatedEntity(Texture2D tex, Rectangle src) : base(tex, src)
    {
      setupAnimations();
      CurrentAction = Animation_Type.Stand;
    }

    public AnimatedEntity(Texture2D tex, Rectangle src, Vector2 pos) : base(tex, src, pos)
    {
      setupAnimations();
      CurrentAction = Animation_Type.Stand;
    }

    public AnimatedEntity(Texture2D tex, Rectangle src, Vector2 pos, float speed) : base(tex, src, pos, speed)
    {
      setupAnimations();
      CurrentAction = Animation_Type.Stand;
    }

    private void setupAnimations()
    {
      Animations = new Animation[NUM_ACTIONS];

      for (int i = 0; i < NUM_ACTIONS; i++)
      {
        Animations[i] = new Animation(this);
      }
    }

    public override void Draw(SpriteBatch batch)
    {
      Animations[(int)CurrentAction].Draw(batch);

      foreach (Entity entity in Decorations)
        entity.Draw(batch);

      //if (Target != null)
      //  Target.Draw(batch);
    }

    public override void Animate()
    {
      Animations[(int)CurrentAction].Animate();
    }

    public override void Tick()
    {
      if (Moving)
      {
        switch (_direction)
        {
          case Direction.North:
            CurrentAction = Animation_Type.Move_North;
            break;
          case Direction.South:
            CurrentAction = Animation_Type.Move_South;
            break;
          case Direction.East:
            CurrentAction = Animation_Type.Move_East;
            break;
          case Direction.West:
            CurrentAction = Animation_Type.Move_West;
            break;
        }
      }
      else
        CurrentAction = Animation_Type.Stand;

      base.Tick();
    }

    public override void move()
    {
      if (Moving)
      {
        switch (_direction)
        {
          case Direction.North:
            CurrentAction = Animation_Type.Move_North;
            break;
          case Direction.South:
            CurrentAction = Animation_Type.Move_South;
            break;
          case Direction.East:
            CurrentAction = Animation_Type.Move_East;
            break;
          case Direction.West:
            CurrentAction = Animation_Type.Move_West;
            break;
        }
      }
      else
        CurrentAction = Animation_Type.Stand;

      base.move();
    }
  }
}
