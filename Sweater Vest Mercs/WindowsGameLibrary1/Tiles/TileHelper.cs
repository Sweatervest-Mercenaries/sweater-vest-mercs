using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SVMLib.Visuals;

namespace SVMLib.Tiles
{
    public abstract class TileHelper
    {
        public static TileDataList DataList { get; set; }
        private static Dictionary<Color, TileData> Dictionary { get; set; }
        public static bool Loaded { get; private set; }

        public static void Load( TileDataList tileData )
        {
            if ( Loaded )
                return;

            DataList = tileData;
            Dictionary = new Dictionary<Color, TileData>();
            populateDictionary();
            Loaded = true;
        }

        private static void populateDictionary()
        {
            foreach ( TileData data in DataList.tiles )
            {
                Dictionary.Add( GameConstants.hexToColor( data.Hex ), data );
            }
        }

        public static TileEntity lookupTile( Color c )
        {
            if ( !Loaded )
                throw new InvalidOperationException( "TileHelper must be loaded before use!" );

            if ( !Dictionary.ContainsKey( c ) )
                return new TileEntity();

            TileData data = Dictionary[c];

            TileEntity tile = new TileEntity( SpriteSheet.getSpriteSheet( data.Sheet ).Texture, SpriteSheet.getSpriteSheet( data.Sheet ).getSourceRect( data.SheetPosition ) );

            tile.Collide = data.Collide;
            tile.Spawn = data.Spawn;
            tile.PathStartID = data.PathStartID;
            tile.PathEndID = data.PathEndID;
            tile.ColorData = c;
            tile.TeleportID = data.TeleportID;

            // Special check for buildings...
            if ( data.Sheet.Equals( Sheet.Buildings ) )
            {
                tile.Layer = (float) GameConstants.LAYER_RANGE_BUILDING;
            }

            if ( tile.PathEndID != 0 )
                Console.WriteLine( "Found a path end." );
            if ( tile.PathStartID != 0 )
                Console.WriteLine( "Found a path start." );
            if ( tile.Spawn )
                Console.WriteLine( "Loaded spawn." );
            if ( tile.TeleportID != 0 )
                Console.WriteLine( "Found a teleport: " + tile.TeleportID );

            return tile;
        }
    }

}
