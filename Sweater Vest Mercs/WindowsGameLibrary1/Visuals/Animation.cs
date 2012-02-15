using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SVMLib.Entities;

namespace SVMLib.Visuals
{
  public enum Animation_Type
  {
    Stand = 0, Move_North, Move_South, Move_East, Move_West, Attack_Sword, Block, Cast, Attack_Bow, Stop, Die
  }

  public class Animation
  {
    protected List<Entity> Steps { get; set; }
    Entity _current;
    int _index;
    protected Entity _lead;
    private Color _col;
    public Color Col { get { return _col; } set { _col = value; foreach (Entity step in Steps) step.Color = _col; } }
    private float _lay;
    public float Lay { get { return _lay; } set { _lay = value; foreach (Entity step in Steps) step.Layer = _lay; } }

    public Animation()
    {
      _lead = null;
      Steps = new List<Entity>();
      Col = Color.White;
    }

    public Animation(Entity lead)
    {
      _lead = lead;
      Steps = new List<Entity>();
      Col = Color.White;
    }

    public void AddStep(Entity step)
    {
      step.Layer = _lead.Layer;
      step.Position = _lead.Position;
      step.Color = Col;

      Steps.Add(step);
    }

    public void Draw(SpriteBatch batch)
    {
      if (_current == null)
      {
        if (Steps.Count == 0)
          return;
        else
          _current = Steps[0];
      }

      _current.Position = _lead.Position;

      _current.Draw(batch);
    }

    public void Animate()
    {
      if (Steps.Count == 0)
        throw new InvalidOperationException("The Animation has no steps.");

      if (_current == null)
      {
        _current = Steps[0];
        _index = 0;
        return;
      }

      if (_current.Equals(Steps[Steps.Count - 1]))
      {
        _current = Steps[0];
        _index = 0;
        return;
      }

      _index++;
      _current = Steps[_index];
    }
  }
}
