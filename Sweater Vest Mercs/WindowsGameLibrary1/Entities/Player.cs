using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SVMLib.Entities;
using SVMLib.Visuals;

namespace SVMLib.Entities
{
    public class Player : LivingEntity
    {
        const float Player_Move_Speed = 2.5f;

        public Player()
            : base()
        {
            MoveSpeed = Player_Move_Speed;
            Collide = true;
        }

        public Player( Texture2D tex )
            : base()
        {
            MoveSpeed = Player_Move_Speed;
            loadAnimations();
            Collide = true;
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
