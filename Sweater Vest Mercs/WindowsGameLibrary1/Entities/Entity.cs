using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SVMLib.Entities
{
    public class Entity
    {
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public Rectangle Source { get; set; }
        public bool Visable { get; set; }
        public bool Collide { get; set; }
        public float Layer { get; set; }
        public Vector2 Origin { get; set; }
        public float Scale { get; set; }
        public Color Color { get; set; }

        public Entity()
        {
            Texture = null;
            Source = Rectangle.Empty;
            Visable = false;
            Position = Vector2.Zero;
            Layer = 1;
            Origin = Vector2.Zero;
            Scale = GameConstants.SCALE;
            Color = Color.White;
        }

        public Entity( Texture2D texture )
        {
            Texture = texture;
            Source = new Rectangle( 0, 0, Texture.Width, Texture.Height );
            Visable = true;
            Position = Vector2.Zero;
            Layer = 1;
            Origin = Vector2.Zero;
            Scale = GameConstants.SCALE;
            Color = Color.White;
        }

        public Entity( Texture2D texture, Rectangle source )
        {
            Texture = texture;
            Source = source;
            Visable = true;
            Position = Vector2.Zero;
            Layer = 1;
            //Origin = Vector2.Zero;
            Origin = new Vector2( source.Width / 2, source.Height / 2 );
            Scale = GameConstants.SCALE;
            Color = Color.White;
        }

        public Entity( Texture2D texture, Rectangle source, Vector2 pos )
        {
            Texture = texture;
            Source = source;
            Position = pos;
            Visable = true;
            Layer = 1;
            //Origin = Vector2.Zero;
            Origin = new Vector2( source.Width / 2, source.Height / 2 );
            Scale = GameConstants.SCALE;
            Color = Color.White;
        }

        public virtual void Draw( SpriteBatch batch )
        {
            if ( Visable )
                batch.Draw( Texture, Position, Source, Color, 0, Origin, Scale, SpriteEffects.None, Layer );
        }

        public virtual void Tick()
        {
        }

        public virtual void Animate()
        {
        }

        public virtual void Shift( Vector2 point )
        {
            Position += point;
        }

        public virtual void Center( Vector2 center )
        {
            Position = Vector2.Zero;
            Position += center;
        }

        public virtual bool ContainsPoint( Vector2 point )
        {
            return ( point.X >= Position.X - ( GameConstants.NUM_PIXEL * Scale ) / 2 && point.X <= Position.X + ( GameConstants.NUM_PIXEL * Scale ) / 2
              && point.Y >= Position.Y - ( GameConstants.NUM_PIXEL * Scale ) / 2 && point.Y <= Position.Y + ( GameConstants.NUM_PIXEL * Scale ) / 2 );
        }
    }

    public enum Direction
    {
        North, South, East, West
    }
}
