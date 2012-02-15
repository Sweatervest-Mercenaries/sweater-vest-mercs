using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SVMLib.Visuals
{
    public enum Sheet
    {
        Floor, Terrain, Characters, Items, Alphabet, GUI, UIElements, Effects, Buildings
    }

    public class SpriteSheet
    {
        public Texture2D Texture { get; set; }
        static SpriteSheet floors, terrain, characters, items, alphabet, gui, uiElements, effects, buildings;
        public static bool Loaded { get; private set; }

        public SpriteSheet( Texture2D tex )
        {
            Texture = tex;
        }

        public Rectangle getSourceRect( int index )
        {
            return getSourceRect( index, Texture );
        }

        public static Rectangle getSourceRect( int index, Texture2D tex )
        {
            if ( !Loaded )
                throw new InvalidOperationException( "SpriteSheet must be loaded before use!" );

            int tilesW, tilesH;

            tilesW = tex.Width / GameConstants.NUM_PIXEL;
            tilesH = tex.Height / GameConstants.NUM_PIXEL;

            if ( index >= tilesW * tilesH )
                throw new InvalidOperationException( "Index is too large for given texture." );

            int xOffs, yOffs;

            xOffs = ( index % tilesW ) * GameConstants.NUM_PIXEL;
            yOffs = ( index / tilesH ) * GameConstants.NUM_PIXEL;

            Rectangle rect = new Rectangle( xOffs, yOffs, GameConstants.NUM_PIXEL, GameConstants.NUM_PIXEL );

            return rect;
        }

        public static SpriteSheet getSpriteSheet( Sheet tex )
        {
            if ( !Loaded )
                throw new InvalidOperationException( "SpriteSheet must be loaded before use!" );

            switch ( tex )
            {
                case Sheet.Floor:
                    return floors;
                case Sheet.Terrain:
                    return terrain;
                case Sheet.Items:
                    return items;
                case Sheet.Characters:
                    return characters;
                case Sheet.Alphabet:
                    return alphabet;
                case Sheet.GUI:
                    return gui;
                case Sheet.UIElements:
                    return uiElements;
                case Sheet.Effects:
                    return effects;
                case Sheet.Buildings:
                    return buildings;
            }

            return null;
        }

        public static void Load( ContentManager content )
        {
            if ( Loaded )
                return;

            floors = new SpriteSheet( content.Load<Texture2D>( "floors" ) );
            terrain = new SpriteSheet( content.Load<Texture2D>( "terrain" ) );
            characters = new SpriteSheet( content.Load<Texture2D>( "characters" ) );
            items = new SpriteSheet( content.Load<Texture2D>( "items" ) );
            alphabet = new SpriteSheet( content.Load<Texture2D>( "alphabet" ) );
            gui = new SpriteSheet( content.Load<Texture2D>( "gui" ) );
            uiElements = new SpriteSheet( content.Load<Texture2D>( "ui" ) );
            effects = new SpriteSheet( content.Load<Texture2D>( "effects" ) );
            buildings = new SpriteSheet( content.Load<Texture2D>( "buildings" ) );

            Loaded = true;
        }
    }
}
