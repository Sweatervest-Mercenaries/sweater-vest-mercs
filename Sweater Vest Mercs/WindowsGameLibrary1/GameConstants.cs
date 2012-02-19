using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Globalization;
using SVMLib.Tiles;
using Microsoft.Xna.Framework.Input;
using SVMLib.Entities;
using Microsoft.Xna.Framework.Graphics;

namespace SVMLib
{
    public class GameConstants
    {
        public const int NUM_PIXEL = 16;
        public const float SCALE = 3.0f;
        public const float ITEM_SCALE = 1.0f;
        public const byte ALPHA_STEP = 1;
        public const byte ALPHA_MAX = 100;
        public const byte ALPHA_MIN = 0;
        public static Level LoadedLevel = null;
        public const double MAX_SPEED_VERT = 32;
        public const double MAX_SPEED_HOR = 32;
        public const double TEXT_SCALE = 0.9f;
        public static int SCREEN_MAX_X = 800;
        public static int SCREEN_MAX_Y = 800;
        public static SpriteFont FONT;

        public const double LAYER_RANGE_TERRAIN = .99;
        public const double LAYER_RANGE_MOVING_ENTITY = .59;
        public const double LAYER_RANGE_BUILDING = .49;
        public const double LAYER_RANGE_UI = .39;
        public const double LAYER_RANGE_MENU_BACKGROUND = .29;
        public const double LAYER_RANGE_MENU = .25;
        public const double LAYER_RANGE_DEBUG = 0;

        public const double LAYER_TEXT_OFFSET = 0.00001;

        public static void DrawString(SpriteBatch batch, String str, Vector2 pos, float layer)
        {
            batch.DrawString( GameConstants.FONT, str, pos, Color.Black, 0, Vector2.Zero, (float) GameConstants.TEXT_SCALE, SpriteEffects.None, layer);
        }

        public static Color hexToColor( String hexStr )
        {
            Color c = Color.White;
            uint hex = uint.Parse( hexStr, System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture );

            if ( hexStr.StartsWith( "#" ) )
                hexStr = hexStr.Substring( 1 );

            if ( hexStr.Length == 8 )
            {
                c.A = (byte) ( hex >> 24 );
                c.R = (byte) ( hex >> 16 );
                c.G = (byte) ( hex >> 8 );
                c.B = (byte) ( hex );
            }
            else if ( hexStr.Length == 6 )
            {
                c.R = (byte) ( hex >> 16 );
                c.G = (byte) ( hex >> 8 );
                c.B = (byte) ( hex );
            }
            else
                throw new InvalidOperationException( "Invald hex representation of an ARGB or RGB color value." );

            return c;
        }

        public static bool mouseInBounds( MouseState state, GraphicsDeviceManager graphics )
        {
            return graphics.GraphicsDevice.Viewport.Bounds.Contains( state.X, state.Y );
        }
    }
}
