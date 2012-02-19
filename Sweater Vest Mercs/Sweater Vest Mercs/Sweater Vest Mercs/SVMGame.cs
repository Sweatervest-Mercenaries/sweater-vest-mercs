using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using SVMLib;
using SVMLib.Actions;
using SVMLib.Entities;
using SVMLib.Tiles;
using SVMLib.Visuals;
using SVMLib.Helpers;
using SVMLib.Menus;
using SVMLib.Items;

namespace Sweater_Vest_Mercs
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SVMGame : Microsoft.Xna.Framework.Game
    {
        private const int NUM_TILES_X = 17;
        private const int NUM_TILES_Y = 17;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int screenWidth;
        int screenHeight;
        const float CameraBufferDistance = 0.1f;
        Player player;
        NPC npc;
        public static Level level;
        public static Vector2 Center;
        TimeSpan lastAnimate;
        TimeSpan lastTick;
        TimeSpan Animate_Time = new TimeSpan( 2000000 );
        TimeSpan Tick_Time = new TimeSpan( 100 );
        public static TileHelper tileHelper;
        MovingEntity cursor;
        Menu menu;
        bool menu_key_down;

        public SVMGame()
        {
            graphics = new GraphicsDeviceManager( this );
            Content.RootDirectory = "Content";
        }

        public ContentManager GetContent()
        {
            return Content;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = (int) ( NUM_TILES_X * GameConstants.NUM_PIXEL * GameConstants.SCALE );
            graphics.PreferredBackBufferHeight = (int) ( NUM_TILES_Y * GameConstants.NUM_PIXEL * GameConstants.SCALE );
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            GameConstants.SCREEN_MAX_X = graphics.PreferredBackBufferWidth;
            GameConstants.SCREEN_MAX_Y = graphics.PreferredBackBufferHeight;
            GameConstants.FONT = Content.Load<SpriteFont>( "Calibri" );

            Window.Title = "Sweater Vest Mercenaries";

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch( GraphicsDevice );

            // TODO: use this.Content to load your game content here

            // Load the SpriteSheets
            SpriteSheet.Load( Content );

            // Load the TileData
            TileHelper.Load( Content.Load<TileDataList>( "Tiles" ) );

            // Load the level
            level = new Level( Content.Load<Texture2D>( "level4" ) );
            GameConstants.LoadedLevel = level;

            Camera.Initialize( new Rectangle( 0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height ),
                new Rectangle( 0, 0,
                    (int)Math.Ceiling(level.NumTilesX * GameConstants.SCALE * GameConstants.NUM_PIXEL),
                    (int)Math.Ceiling(level.NumTilesY * GameConstants.SCALE * GameConstants.NUM_PIXEL) ) );

            // Load the player
            player = new Player( SpriteSheet.getSpriteSheet( Sheet.Characters ).Texture );
            player.MoveSpeed = 4f;
            GameConstants.LoadedLevel.Entities.Add( player );

            // Load the npc
            npc = new NPC(1);
            GameConstants.LoadedLevel.Entities.Add( npc );
            for ( int i = 2; i <= level.NPCPaths.Count; i++ )
            {
                GameConstants.LoadedLevel.Entities.Add( new NPC( i ) );
            }

                // Set the Screen Vars
                screenHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;
            screenWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
            Center = new Vector2( screenWidth / 2, screenHeight / 2 );
            //CameraCenter = Vector2.Zero;

            // Cursor
            cursor = new MovingEntity( SpriteSheet.getSpriteSheet( Sheet.UIElements ).Texture, SpriteSheet.getSpriteSheet( Sheet.UIElements ).getSourceRect( 6 ) );
            cursor.Layer = 0;
            cursor.NoClip = true;
            cursor.Scale = 2.0f;

            // Try and find the spawn
            TileEntity tmp = level.Spawn;
            if ( tmp != null )
            {
                player.Position = level.Spawn.Position;
            }
            else
            {
                Console.WriteLine( "No spawn found!" );
                player.Position = Vector2.Zero;
            }

            // Center the camera on the char
            centerCamera( player.Position );

            menu = new Menu( MenuType.Main );

            menu.Background = new Texture2D( GraphicsDevice, 1, 1 );
            menu.Background.SetData(new[]{Color.White});
            menu.Items.Add( new MenuItem( "Test Item 1", new Item( "THIS IS AN ITEM" ) ) );
            menu.Items.Add( new MenuItem( "Test Item 2", new Item( "THIS IS AN ITEM" ) ) );
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update( GameTime gameTime )
        {
            // Allows the game to exit
            if ( GamePad.GetState( PlayerIndex.One ).Buttons.Back == ButtonState.Pressed )
                this.Exit();

            // TODO: Add your update logic here
            updateMouse();

            if ( gameTime.TotalGameTime >= lastAnimate + Animate_Time )
                Animate( gameTime );

            if ( gameTime.TotalGameTime >= lastTick + Tick_Time )
                Tick( gameTime );

            checkMouse();
            checkKeys();
            updateMove();

            base.Update( gameTime );
        }

        private void checkMouse()
        {
            MouseState state = Mouse.GetState();

            //if ( GameConstants.mouseInBounds( state, graphics ) )
            //{
            //    if ( state.LeftButton.Equals( ButtonState.Pressed ) )
            //    {
            //        Entity lead = level.getTilesUnder( state.X, state.Y )[0];
            //        Entity tile = new Entity( SpriteSheet.getSpriteSheet( Sheet.Effects ).Texture, SpriteSheet.getSpriteSheet( Sheet.Effects ).getSourceRect( 0 ) );
            //        PositionAnimation ani = new PositionAnimation( lead.Position );
            //        ani.AddStep( tile );
            //        ani.Lay = 0;
            //        Entities.Add( new EntityEffect( ani, Effect_Type.Fade ) );
            //        player.Path = SVMLib.Helpers.Path.findPath( level.getTilesUnder( player.Position )[0].Position, lead.Position, level );
            //    }
            //}
        }

        private void updateMouse()
        {
            MouseState state = Mouse.GetState();
            cursor.Position = new Vector2( state.X, state.Y );
        }

        private void Animate( GameTime time )
        {
            lastAnimate = time.TotalGameTime;

            foreach ( Entity ent in GameConstants.LoadedLevel.Entities )
            {
                ent.Animate();
            }

            level.Animate();
            //player.Animate();
        }

        private void Tick( GameTime time )
        {
            lastTick = time.TotalGameTime;

            foreach ( Entity entity in GameConstants.LoadedLevel.Entities )
            {
                entity.Tick();
            }

            level.Tick();
            //player.Tick();
            cursor.Tick();
        }

        private void updateMove()
        {
            foreach ( Entity entity in GameConstants.LoadedLevel.Entities )
            {
                if ( entity is MovingEntity )
                {
                    ( (MovingEntity) entity ).move();
                }
            }

            //player.move();
            Vector3 pos = Camera.ToScreenPosition( new Vector3( player.Position, 0 ) );
            centerCamera( new Vector2( pos.X, pos.Y ) );
            cursor.move();

            if ( pos.X <= screenWidth * CameraBufferDistance )
                centerCamera( player.Position );
            else if ( pos.X >= screenWidth - screenWidth * CameraBufferDistance )
                centerCamera( player.Position );
            else if ( pos.Y <= screenHeight * CameraBufferDistance )
                centerCamera( player.Position );
            else if ( pos.Y >= screenHeight - screenHeight * CameraBufferDistance )
                centerCamera( player.Position );
            //if ( player.Position.X <= screenWidth * CameraBufferDistance )
            //    centerCamera( player.Position );
            //else if ( player.Position.X >= screenWidth - screenWidth * CameraBufferDistance )
            //    centerCamera( player.Position );
            //else if ( player.Position.Y <= screenHeight * CameraBufferDistance )
            //    centerCamera( player.Position );
            //else if ( player.Position.Y >= screenHeight - screenHeight * CameraBufferDistance )
            //    centerCamera( player.Position );
        }

        private void centerCamera( Vector2 pos )
        {
            Vector2 diff = pos - Center;

            Camera.Move( diff );
            Camera.TransformMatrix();

            //level.Center( diff );

            //foreach ( Entity e in Entities )
            //    e.Shift( diff );

            //player.Shift( diff );
            //player.Center(Center);
            //player.Position = Vector2.Zero;
            //player.Position += Center;
        }

        private void checkMoveKeys( KeyboardState keys )
        {
            if ( keys.IsKeyDown( Keys.W ) || keys.IsKeyDown( Keys.Up ) )
                player.updateDirection( Direction.North );
            else if ( keys.IsKeyDown( Keys.S ) || keys.IsKeyDown( Keys.Down ) )
                player.updateDirection( Direction.South );
            else if ( keys.IsKeyDown( Keys.A ) || keys.IsKeyDown( Keys.Left ) )
                player.updateDirection( Direction.West );
            else if ( keys.IsKeyDown( Keys.D ) || keys.IsKeyDown( Keys.Right ) )
                player.updateDirection( Direction.East );
        }

        private void checkKeys()
        {
            KeyboardState keys = Keyboard.GetState();

            if ( !player.Moving )
                checkMoveKeys( keys );
            
            if ( keys.IsKeyDown( Keys.NumPad0 ) )
            {
                player.Decorations.Clear();
                player.Decorate( new Entity( SpriteSheet.getSpriteSheet( Sheet.Items ).Texture, SpriteSheet.getSpriteSheet( Sheet.Items ).getSourceRect( 0 ) ) );
            }
            else if ( keys.IsKeyDown( Keys.NumPad1 ) )
            {
                player.Decorations.Clear();
                player.Decorate( new Entity( SpriteSheet.getSpriteSheet( Sheet.Items ).Texture, SpriteSheet.getSpriteSheet( Sheet.Items ).getSourceRect( 1 ) ) );
            }
            else if ( keys.IsKeyDown( Keys.NumPad2 ) )
            {
                player.Decorations.Clear();
                player.Decorate( new Entity( SpriteSheet.getSpriteSheet( Sheet.Items ).Texture, SpriteSheet.getSpriteSheet( Sheet.Items ).getSourceRect( 2 ) ) );
            }
            else if ( keys.IsKeyDown( Keys.NumPad3 ) )
            {
                player.Decorations.Clear();
                player.Decorate( new Entity( SpriteSheet.getSpriteSheet( Sheet.Items ).Texture, SpriteSheet.getSpriteSheet( Sheet.Items ).getSourceRect( 3 ) ) );
            }
            else if ( keys.IsKeyDown( Keys.NumPad4 ) )
            {
                player.Decorations.Clear();
                player.Decorate( new Entity( SpriteSheet.getSpriteSheet( Sheet.Items ).Texture, SpriteSheet.getSpriteSheet( Sheet.Items ).getSourceRect( 4 ) ) );
            }
            else if ( keys.IsKeyDown( Keys.NumPad5 ) )
            {
                player.Decorations.Clear();
                player.Decorate( new Entity( SpriteSheet.getSpriteSheet( Sheet.Items ).Texture, SpriteSheet.getSpriteSheet( Sheet.Items ).getSourceRect( 5 ) ) );
            }
            else if ( keys.IsKeyDown( Keys.NumPad6 ) )
            {
                player.Decorations.Clear();
                player.Decorate( new Entity( SpriteSheet.getSpriteSheet( Sheet.Items ).Texture, SpriteSheet.getSpriteSheet( Sheet.Items ).getSourceRect( 6 ) ) );
            }
            else if ( keys.IsKeyDown( Keys.NumPad7 ) )
            {
                player.Decorations.Clear();
                player.Decorate( new Entity( SpriteSheet.getSpriteSheet( Sheet.Items ).Texture, SpriteSheet.getSpriteSheet( Sheet.Items ).getSourceRect( 7 ) ) );
            }
            else if ( keys.IsKeyDown( Keys.Space ) )
            {
                testMethod();
            }
            else if ( keys.IsKeyDown( Keys.H ) )
            {
                Camera.Move( new Vector2( -Camera.CameraSpeed, 0 ) );
                Camera.TransformMatrix();
            }
            else if ( keys.IsKeyDown( Keys.L ) )
            {
                Camera.Move( new Vector2( Camera.CameraSpeed, 0 ) );
                Camera.TransformMatrix();
            }
            else if ( keys.IsKeyDown( Keys.K ) )
            {
                Camera.Move( new Vector2( 0, -Camera.CameraSpeed ) );
                Camera.TransformMatrix();
            }
            else if ( keys.IsKeyDown( Keys.J ) )
            {
                Camera.Move( new Vector2( 0, Camera.CameraSpeed ) );
                Camera.TransformMatrix();
            }
            else if ( keys.IsKeyDown( Keys.D0 ) )
            {
                player.Position = level.Spawn.Position;
                Camera.Position = Vector2.Zero;
                centerCamera( player.Position );
            }
            else if ( keys.IsKeyDown( Keys.B ) && !menu_key_down )
            {
                menu_key_down = true;
                if ( menu.Visable )
                    menu.Close();
                else
                    menu.Open();
            }
            else if ( keys.IsKeyUp( Keys.B ) )
            {
                menu_key_down = false;
            }
        }

        private void testMethod()
        {
            TileEntity tile = TileHelper.lookupTile( GameConstants.hexToColor( "267F00" ) );
            tile.Position = player.Position;
            tile.Color = new Color( 255, 255, 255, 0 );
            GameConstants.LoadedLevel.Entities.Add( tile );
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw( GameTime gameTime )
        {
            GraphicsDevice.Clear( Color.CornflowerBlue );

            // TODO: Add your drawing code here
            if ( level.Loaded )
            {
                spriteBatch.Begin( SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, Camera.Transform );
                DrawScenery();
                DrawEntities();
                spriteBatch.End();

                spriteBatch.Begin( SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null );
                DrawUI();
                spriteBatch.End();
            }

            base.Draw( gameTime );
        }

        private void DrawEntities()
        {
            foreach ( Entity entity in GameConstants.LoadedLevel.Entities )
            {
                entity.Draw( spriteBatch );
            }

            //player.Draw( spriteBatch );
        }

        private void DrawScenery()
        {
            level.DrawTiles( spriteBatch );
        }

        private void DrawUI()
        {
            //spriteBatch.Draw(getSpriteSheet(Tex.Ui).Texture, Vector2.Zero, Color.White);
            cursor.Draw( spriteBatch );
            SpriteFont font = Content.Load<SpriteFont>("Calibri");
            DrawString( "Camera X: " + Camera.Position.X + "   Y: " + Camera.Position.Y, 0 );
            DrawString( "NPC:", 1 );
            DrawString( "   Pos X: " + npc.Position.X + "   Y: " + npc.Position.Y, 2 );
            DrawString( "   Path: " + npc.Path.VectorPath.Count, 3 );
            DrawString( "   Moving: " + npc.Moving, 4 );
            DrawString( "   Cycle: " + npc.CurrentCycle, 5 );
            DrawString( "   Path:", 6 );
            DrawString( "      Start: " + npc.NPCPath.Start.Position.ToString(), 7 );
            DrawString( "      End: " + npc.NPCPath.End.Position.ToString(), 8 );
            DrawString( "   PrevTarget: " + npc.PrevMoveTarget.ToString(), 9 );

            menu.Draw( spriteBatch );
        }

        private void DrawString(String str, int row)
        {
            spriteBatch.DrawString( GameConstants.FONT, str, new Vector2( 0, (float) ( row * 16 * GameConstants.TEXT_SCALE ) ), Color.Red, 0, Vector2.Zero, (float) GameConstants.TEXT_SCALE, SpriteEffects.None, 0 );
        }
    }
}
