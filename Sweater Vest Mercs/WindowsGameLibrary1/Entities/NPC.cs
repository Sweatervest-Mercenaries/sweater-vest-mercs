using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SVMLib.Helpers;
using SVMLib.Tiles;
using SVMLib.Visuals;

namespace SVMLib.Entities
{
    public class NPC : LivingEntity
    {
        public NPCPath NPCPath { get; set; }
        private int IdleCycles { get; set; }
        public int CurrentCycle { get; set; }
        public Vector2 PrevMoveTarget { get; set; }
        public int PathID { get; set; }

        public NPC( int pathId )
        {
            findPath( pathId );
            loadAnimations();
            IdleCycles = 250;
            CurrentCycle = 0;
            Collide = true;
            PathID = pathId;
        }

        private void findPath( int id )
        {
            NPCPath = GameConstants.LoadedLevel.getNPCPath( id );

            if ( NPCPath != null )
            {
                Position = NPCPath.Start.Position;
            }
        }

        public override void move()
        {
            if ( Position.Equals( PrevMoveTarget ) )
                Path = new Path();

            if ( !Moving && CurrentCycle >= IdleCycles )
            {
                CurrentCycle = 0;

                if ( Path.VectorPath.Count != 0 )
                {
                    // FIND A NEW WAY
                    Path = Path.findPath( Position, Path.VectorPath.ElementAt( Path.VectorPath.Count - 1 ), GameConstants.LoadedLevel );
                }
                else if ( Position.Equals( NPCPath.Start.Position ) )
                {
                    Path = Path.findPath( Position, NPCPath.End.Position, GameConstants.LoadedLevel );
                    PrevMoveTarget = NPCPath.End.Position;
                }
                else if ( Position.Equals( NPCPath.End.Position ) )
                {
                    Path = Path.findPath( Position, NPCPath.Start.Position, GameConstants.LoadedLevel );
                    PrevMoveTarget = NPCPath.Start.Position;
                }
                else
                {
                    Path = Path.findPath( Position, PrevMoveTarget, GameConstants.LoadedLevel );
                }
            }
            else if ( !Moving )
            {
                CurrentCycle++;
            }

            base.move();
        }

        private void loadAnimations()
        {
            // Standing
            Animations[(int) Animation_Type.Stand].AddStep( new Entity( SpriteSheet.getSpriteSheet( Sheet.Characters ).Texture, SpriteSheet.getSpriteSheet( Sheet.Characters ).getSourceRect( 0 ) ) );
            Animations[(int) Animation_Type.Stand].AddStep( new Entity( SpriteSheet.getSpriteSheet( Sheet.Characters ).Texture, SpriteSheet.getSpriteSheet( Sheet.Characters ).getSourceRect( 0 ) ) );
            Animations[(int) Animation_Type.Stand].AddStep( new Entity( SpriteSheet.getSpriteSheet( Sheet.Characters ).Texture, SpriteSheet.getSpriteSheet( Sheet.Characters ).getSourceRect( 0 ) ) );
            Animations[(int) Animation_Type.Stand].AddStep( new Entity( SpriteSheet.getSpriteSheet( Sheet.Characters ).Texture, SpriteSheet.getSpriteSheet( Sheet.Characters ).getSourceRect( 0 ) ) );
            Animations[(int) Animation_Type.Stand].AddStep( new Entity( SpriteSheet.getSpriteSheet( Sheet.Characters ).Texture, SpriteSheet.getSpriteSheet( Sheet.Characters ).getSourceRect( 1 ) ) );
            Animations[(int) Animation_Type.Stand].AddStep( new Entity( SpriteSheet.getSpriteSheet( Sheet.Characters ).Texture, SpriteSheet.getSpriteSheet( Sheet.Characters ).getSourceRect( 1 ) ) );
            Animations[(int) Animation_Type.Stand].AddStep( new Entity( SpriteSheet.getSpriteSheet( Sheet.Characters ).Texture, SpriteSheet.getSpriteSheet( Sheet.Characters ).getSourceRect( 1 ) ) );
            Animations[(int) Animation_Type.Stand].AddStep( new Entity( SpriteSheet.getSpriteSheet( Sheet.Characters ).Texture, SpriteSheet.getSpriteSheet( Sheet.Characters ).getSourceRect( 1 ) ) );

            // Walking North
            Animations[(int) Animation_Type.Move_North].AddStep( new Entity( SpriteSheet.getSpriteSheet( Sheet.Characters ).Texture, SpriteSheet.getSpriteSheet( Sheet.Characters ).getSourceRect( 2 ) ) );
            Animations[(int) Animation_Type.Move_North].AddStep( new Entity( SpriteSheet.getSpriteSheet( Sheet.Characters ).Texture, SpriteSheet.getSpriteSheet( Sheet.Characters ).getSourceRect( 18 ) ) );
            Animations[(int) Animation_Type.Move_North].AddStep( new Entity( SpriteSheet.getSpriteSheet( Sheet.Characters ).Texture, SpriteSheet.getSpriteSheet( Sheet.Characters ).getSourceRect( 2 ) ) );
            Animations[(int) Animation_Type.Move_North].AddStep( new Entity( SpriteSheet.getSpriteSheet( Sheet.Characters ).Texture, SpriteSheet.getSpriteSheet( Sheet.Characters ).getSourceRect( 34 ) ) );

            // Walking South
            // TODO
            Animations[(int) Animation_Type.Move_South].AddStep( new Entity( SpriteSheet.getSpriteSheet( Sheet.Characters ).Texture, SpriteSheet.getSpriteSheet( Sheet.Characters ).getSourceRect( 0 ) ) );
            Animations[(int) Animation_Type.Move_South].AddStep( new Entity( SpriteSheet.getSpriteSheet( Sheet.Characters ).Texture, SpriteSheet.getSpriteSheet( Sheet.Characters ).getSourceRect( 1 ) ) );

            // Walking East
            Animations[(int) Animation_Type.Move_East].AddStep( new Entity( SpriteSheet.getSpriteSheet( Sheet.Characters ).Texture, SpriteSheet.getSpriteSheet( Sheet.Characters ).getSourceRect( 0 ) ) );
            Animations[(int) Animation_Type.Move_East].AddStep( new Entity( SpriteSheet.getSpriteSheet( Sheet.Characters ).Texture, SpriteSheet.getSpriteSheet( Sheet.Characters ).getSourceRect( 16 ) ) );

            // Walking West
            Animations[(int) Animation_Type.Move_West].AddStep( new Entity( SpriteSheet.getSpriteSheet( Sheet.Characters ).Texture, SpriteSheet.getSpriteSheet( Sheet.Characters ).getSourceRect( 1 ) ) );
            Animations[(int) Animation_Type.Move_West].AddStep( new Entity( SpriteSheet.getSpriteSheet( Sheet.Characters ).Texture, SpriteSheet.getSpriteSheet( Sheet.Characters ).getSourceRect( 17 ) ) );
        }

        public override void Shift( Vector2 point )
        {
            if ( Moving )
                Destination += point;
            if ( Target != null )
                Target.Shift( point );

            base.Shift( point );
        }
    }
}
