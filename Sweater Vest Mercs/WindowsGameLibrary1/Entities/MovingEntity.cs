using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SVMLib.Visuals;
using SVMLib.Tiles;
using SVMLib.Helpers;

namespace SVMLib.Entities
{
    public class MovingEntity : Entity
    {
        public Vector2 Velocity { get; set; }
        public float MoveSpeed { get; set; }
        public bool Moving { get; set; }
        public List<Entity> Decorations { get; set; }
        protected Direction _direction;
        public bool NoClip { get; set; }
        private Vector2 _dest;
        public Path Path { get; set; }
        public Vector2 Destination
        {
            get
            {
                return _dest;
            }

            set
            {
                _dest = value;
                if ( _dest.Equals( Position ) )
                    _dest = Vector2.Zero;

                /*
                if (Target == null)
                {
                  Entity box = new Entity(SpriteSheet.getSpriteSheet(Sheet.Effects).Texture, SpriteSheet.getSpriteSheet(Sheet.Effects).getSourceRect(0));
                  PositionAnimation ani = new PositionAnimation(Destination);
                  ani.AddStep(box);
                  ani.Lay = 0;
                  Target = new EntityEffect(ani, Effect_Type.Fade);
                }
                else if (!_dest.Equals(Vector2.Zero))
                  Target.Position = Destination;
                */
                foreach ( Entity ent in GameConstants.LoadedLevel.Entities )
                {
                    if ( ent.ContainsPoint( _dest ) )
                    {
                        if ( ent.Collide )
                        {
                            _dest = Vector2.Zero;
                            break;
                        }
                    }
                    else if ( ent is MovingEntity )
                    {
                        if ( ( (MovingEntity) ent ).Destination.Equals( _dest ) && _dest != Vector2.Zero && !ent.Equals( this ) )
                        {
                            _dest = Vector2.Zero;
                            break;
                        }
                    }
                }

                Vector2 diff = _dest - Position;
                //Console.Write("Dest: " + _dest.ToString() + "\tDiff: " + diff.ToString());
                if ( !diff.Equals( Vector2.Zero ) )
                    diff.Normalize();

                if ( !diff.Equals( Vector2.Zero ) && !_dest.Equals( Vector2.Zero ) )
                    Moving = true;
                else
                    Moving = false;

                //Console.WriteLine("\tMoving: " + Moving);
                Velocity = diff * MoveSpeed;
            }
        }

        public EntityEffect Target { get; set; }

        public MovingEntity()
            : base()
        {
            MoveSpeed = 1.0f;
            Velocity = Vector2.Zero;
            Moving = false;
            Decorations = new List<Entity>();
            NoClip = false;
            Target = null;
            _dest = Vector2.Zero;
            Path = new Path();

            Layer = (float) GameConstants.LAYER_RANGE_MOVING_ENTITY;
        }

        public MovingEntity( Texture2D tex )
            : base( tex )
        {
            MoveSpeed = 1.0f;
            Velocity = Vector2.Zero;
            Moving = false;
            Decorations = new List<Entity>();
            NoClip = false;
            Target = null;
            Destination = Vector2.Zero;
            Path = new Path();
            Layer = (float) GameConstants.LAYER_RANGE_MOVING_ENTITY;
        }

        public MovingEntity( Texture2D tex, Rectangle src )
            : base( tex, src )
        {
            MoveSpeed = 1.0f;
            Velocity = Vector2.Zero;
            Moving = false;
            Decorations = new List<Entity>();
            NoClip = false;
            Target = null;
            Destination = Vector2.Zero;
            Path = new Path();
            Layer = (float) GameConstants.LAYER_RANGE_MOVING_ENTITY;
        }

        public MovingEntity( Texture2D tex, Rectangle src, Vector2 pos )
            : base( tex, src, pos )
        {
            MoveSpeed = 1.0f;
            Velocity = Vector2.Zero;
            Moving = false;
            Decorations = new List<Entity>();
            NoClip = false;
            Target = null;
            Destination = Vector2.Zero;
            Path = new Path();
            Layer = (float) GameConstants.LAYER_RANGE_MOVING_ENTITY;
        }

        public MovingEntity( Texture2D tex, Rectangle src, Vector2 pos, float speed )
            : base( tex, src, pos )
        {
            MoveSpeed = speed;
            Velocity = Vector2.Zero;
            Moving = false;
            Decorations = new List<Entity>();
            NoClip = false;
            Target = null;
            Destination = Vector2.Zero;
            Path = new Path();
            Layer = (float) GameConstants.LAYER_RANGE_MOVING_ENTITY;
        }

        public void updateDirection( Direction dir )
        {
            _direction = dir;
            Vector2 norm = Vector2.Zero;

            switch ( dir )
            {
                case Direction.North:
                    norm = new Vector2( 0, -MoveSpeed );
                    break;
                case Direction.South:
                    norm = new Vector2( 0, MoveSpeed );
                    break;
                case Direction.West:
                    norm = new Vector2( -MoveSpeed, 0 );
                    break;
                case Direction.East:
                    norm = new Vector2( MoveSpeed, 0 );
                    break;
            }

            norm.Normalize();
            List<TileEntity> tiles = GameConstants.LoadedLevel.getTilesUnder( new Vector2( Position.X + ( GameConstants.NUM_PIXEL * GameConstants.SCALE * norm.X ),
              Position.Y + ( GameConstants.NUM_PIXEL * GameConstants.SCALE * norm.Y ) ) );
            bool collide = false;

            foreach ( TileEntity tile in tiles )
            {
                if ( tile.Collide )
                {
                    collide = true;
                }
            }

            if ( !Destination.Equals( tiles[0].Position ) )
                if ( !collide )
                    Destination = tiles[0].Position;
        }

        public virtual void move()
        {
            //if ( Destination != null && !Destination.Equals( Vector2.Zero ) && !Moving )
            //{
            //    Position = Destination;
            //    Destination = Vector2.Zero;
            //    Moving = false;
            //}

            if ( Path.VectorPath.Count != 0 && Destination.Equals( Vector2.Zero ) && !Moving )
            {
                Vector2 tmp = Path.VectorPath.ElementAt( 0 );

                foreach ( Entity ent in GameConstants.LoadedLevel.Entities )
                {
                    if ( ent.ContainsPoint( tmp ) )
                    {
                        if ( ent.Collide )
                        {
                            tmp = Vector2.Zero;
                            Console.WriteLine( "Collision!" );

                            if (this is NPC)
                                Path = Path.findPath( this, ( (NPC) this ).PrevMoveTarget, GameConstants.LoadedLevel );
                        }
                    }
                }

                if ( tmp != Vector2.Zero )
                {
                    Path.VectorPath.Dequeue();
                    Destination = tmp;
                }
            }

            /*if (Moving && !GameConstants.LoadedLevel.canMove(Position, Destination))
            {
              TileEntity tile = GameConstants.LoadedLevel.getTilesUnder(Position.X, Position.Y)[0];
              Destination = tile.Position;
            }*/

            if ( Moving )
            {
                if ( Velocity != Vector2.Zero && ( Velocity.X == 0 || Velocity.Y == 0 ) )
                    Position = Vector2.Add( Position, Velocity );
                else if ( Velocity.X != 0 && Velocity.Y != 0 )
                {
                    Path = new Path();
                    Moving = false;
                    return;
                }

                Vector2 diff = Position - Destination;

                if ( diff.Length() < MoveSpeed || Position.Equals( Destination ) || Velocity.Equals( Vector2.Zero ) )
                {
                    if ( Destination != Vector2.Zero )
                        Position = Destination;

                    Destination = Vector2.Zero;

                    if ( Path.VectorPath.Count == 0 && Path.TilePath.Count == 0 )
                        Moving = false;
                }
            }

            foreach ( Entity entity in Decorations )
            {
                entity.Position = Position;
            }
        }

        public void Decorate( Entity entity )
        {
            Console.WriteLine( "Decorating" );
            entity.Layer = 0.09f;
            entity.Scale = GameConstants.ITEM_SCALE;
            entity.Position = Position + new Vector2(Source.Width / 2, Source.Height / 2);
            Decorations.Add( entity );
        }

        public override void Draw( SpriteBatch batch )
        {
            foreach ( Entity entity in Decorations )
                entity.Draw( batch );

            //if (Target != null)
            //  Target.Draw(batch);

            base.Draw( batch );
        }

        public override void Shift( Vector2 point )
        {
            if ( Moving && !Destination.Equals( Vector2.Zero ) )
                Destination += point;
            //Path.shift(point);

            foreach ( Entity decor in Decorations )
                decor.Shift( point );

            base.Shift( point );
        }

        public override void Tick()
        {
            // Check to see if the path is empty or zero. 
            if ( ( Path.VectorPath.Count != 0 && Path.TilePath.Count != 0 ) && Destination.Equals( Vector2.Zero ) )
            {
                Vector2 tmp = Path.TilePath.Dequeue().Position;
                Destination = tmp;
            }

            // Apply gravity
            if ( GameConstants.LoadedLevel.HasGravity && Math.Abs( Velocity.Y ) + GameConstants.LoadedLevel.Gravity <= GameConstants.MAX_SPEED_VERT )
            {
                Velocity = Vector2.Add( Velocity, new Vector2( 0, GameConstants.LoadedLevel.Gravity ) );
            }

            updateDirection();
        }

        public void updateDirection()
        {
            Vector2 dir = new Vector2( Velocity.X, Velocity.Y );
            dir.Normalize();

            if ( dir.Equals( new Vector2( 0, -1 ) ) )
            {
                // North
                _direction = Direction.North;
                //Console.WriteLine("North");
            }
            else if ( dir.Equals( new Vector2( 0, 1 ) ) )
            {
                // South
                _direction = Direction.South;
                //Console.WriteLine("South");
            }
            else if ( dir.Equals( new Vector2( 1, 0 ) ) )
            {
                // East
                _direction = Direction.East;
                //Console.WriteLine("East");
            }
            else if ( dir.Equals( new Vector2( -1, 0 ) ) )
            {
                // West
                _direction = Direction.West;
                //Console.WriteLine("West");
            }
        }
    }
}
