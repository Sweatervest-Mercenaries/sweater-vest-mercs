using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SVMLib.Entities;
using SVMLib.Helpers;

namespace SVMLib.Tiles
{
    /// <summary>
    /// Level class. Holds all of the assets for a loaded level including tiles, entities, etc.
    /// </summary>
    public class Level
    {
        Texture2D levelData;
        public List<TileEntity>[,] Tiles { get; set; }
        public List<NPCPath> NPCPaths { get; set; }
        public Boolean HasGravity { get; set; }
        public float Gravity { get; set; }
        public TileEntity Spawn { get; private set; }
        public int NumTilesX { get; set; }
        public int NumTilesY { get; set; }
        public bool Loaded { get; set; }
        public List<Entity> Entities = new List<Entity>();

        /// <summary>
        /// Main ctor. Pass in the level image.
        /// </summary>
        /// <param name="tex">Level image to load off of</param>
        public Level( Texture2D tex )
        {
            levelData = tex;
            Tiles = new List<TileEntity>[tex.Height, tex.Width];
            NPCPaths = new List<NPCPath>();
            Spawn = new TileEntity();
            Loaded = false;
            Entities = new List<Entity>();
            loadTiles();
        }

        /// <summary>
        /// Draws all the tiles loaded in the level
        /// </summary>
        /// <param name="batch">SpriteBatch to draw with.</param>
        public void DrawTiles( SpriteBatch batch )
        {
            // Loop through all tile lists
            foreach ( List<TileEntity> list in Tiles )
            {
                // Loop through each layer
                foreach ( TileEntity tile in list )
                {
                    // Safety check
                    if ( tile != null )
                        tile.Draw( batch );
                }
            }
        }

        /// <summary>
        /// Uses the tileData from the ctor to load a level. Each pixle = specific tile/information.
        /// </summary>
        private void loadTiles()
        {
            Color[] colors = new Color[levelData.Width * levelData.Height];
            List<TileEntity> pathTiles = new List<TileEntity>();
            levelData.GetData<Color>( colors );
            float layer = (float) GameConstants.LAYER_RANGE_TERRAIN;
            float layer_step = 0.0001f;
            //int indexOffset = 0;
            int mapWidth = 0, mapHeight = 0;
            int b1 = -1, b2 = -1, b3 = -1, b4 = -1;
            int numSkip = 0;
            int row = 0, col = -1;
            bool skipRow = false;
            float x, y;
            Color c;
            TileEntity tile;

            // Loop through each pixle, can jump around
            for ( int i = 0; i < colors.Length; i++ )
            {
                c = colors[i];
                tile = null;

                col++;

                // Black is used as a special information block
                if ( c.Equals( Color.Black ) )
                {
                    //indexOffset = i + 1;
                    layer -= layer_step;

                    skipRow = !skipRow;
                    row = 0;

                    // DEBUG
                    Console.WriteLine( "BLACK!" );

                    // Found the first black tile on the line
                    if ( b1 == -1 )
                        b1 = i;
                    else if ( b2 == -1 )
                        b2 = i;
                    else if ( b3 == -1 )
                        b3 = i;
                    else if ( b4 == -1 )
                        b4 = i;
                    else
                    {
                        // Already have all of our data needed, skip to the next row
                        col = -1;
                        i += levelData.Width - 1;
                        skipRow = false;
                        continue;
                    }

                    if ( b1 != -1 && b2 != -1 )
                    {
                        // We read the whole first row...
                        if ( mapWidth == 0 )
                        {
                            mapWidth = b2 - b1 + 1;
                            NumTilesX = mapWidth;

                            // Add one to skip the first col
                            numSkip = levelData.Width - mapWidth;

                            // DEBUG
                            Console.WriteLine( "Map Width is: " + mapWidth );
                            Console.WriteLine( "Number to skip is: " + numSkip );
                        }

                        if ( b3 != -1 && mapHeight == 0 )
                        {
                            // We have at least read the first black on last line
                            mapHeight = ( i - levelData.Width ) / levelData.Width;
                            NumTilesY = mapHeight;

                            // DEBUG
                            Console.WriteLine( "Map Height is: " + mapHeight );

                            // Resize our tiles array
                            List<TileEntity>[,] newTiles = new List<TileEntity>[mapHeight, mapWidth];

                            // Copy over
                            for ( int y1 = 0; y1 < mapHeight; y1++ )
                            {
                                for ( int x1 = 0; x1 < mapWidth; x1++ )
                                {
                                    newTiles[y1, x1] = Tiles[y1, x1];
                                }
                            }

                            Tiles = newTiles;
                        }

                        if ( b4 != -1 )
                        {
                            // Double check our mapWidth
                            if ( b4 - b3 + 1 != mapWidth )
                                throw new InvalidOperationException( "Invalid Map!" );
                        }
                    }

                    if ( !skipRow )
                    {
                        // This was the second black in the row, skip ahead to the next row
                        i += numSkip;
                        col = -1;
                        continue;
                    }
                }

                // DEBUG
                //if ( i % texture.Width == 0 )
                //{
                //    Console.WriteLine( "i: " + i );
                //}

                //Console.WriteLine( "col: " + col + "\t\trow: " + row );
                if ( skipRow )
                {
                    continue;
                }

                if ( mapWidth != 0 )
                {
                    if ( col == mapWidth )
                    {
                        // Rest are skip
                        row++;
                        col = -1;
                        i += numSkip - 1;

                        continue;
                    }
                }

                if ( row == mapHeight && mapHeight != 0 )
                {
                    // We are done here, there is excess white on the bottom
                    break;
                }

                /////////////////////////////////////////////////////////////////////////
                // Below this line the pixel is interpreted as a tile rather than data //
                /////////////////////////////////////////////////////////////////////////
                if ( Tiles[row, col] == null )
                {
                    Tiles[row, col] = new List<TileEntity>();
                }

                // Get the tile
                if ( !c.Equals( Color.Black ) )
                    tile = TileHelper.lookupTile( c );
                else
                    continue;

                x = col * GameConstants.NUM_PIXEL * GameConstants.SCALE;
                y = row * GameConstants.NUM_PIXEL * GameConstants.SCALE;

                // DEBUG
                //if ( !tile.ColorData.Equals( Color.White ) )
                //    Console.WriteLine( "Read: " + tile.ColorData.ToString() );

                if ( tile != null )
                {
                    tile.Position = new Vector2( x, y );
                    if ( tile.Layer == 1 )
                        tile.Layer = layer;
                }
                else
                    tile = new TileEntity();

                if ( Tiles[row, col].Count == 0 )
                    Tiles[row, col].Add( tile );
                else if ( !c.Equals( Color.White ) )
                    Tiles[row, col].Add( tile );
                //Tiles[i % texture.Width, i / texture.Height] = tile;

                if ( tile.PathStartID != 0 || tile.PathEndID != 0 )
                {
                    Console.WriteLine( "Adding a pathTile" );
                    Console.WriteLine( "Color is: " + c.ToString() );
                    pathTiles.Add( tile );
                }
                if ( tile.Spawn )
                {
                    Console.WriteLine( "Added spawn" );
                    Spawn = tile;
                }
            }

            TileEntity start, end;
            for ( int i = 1; i <= pathTiles.Count; i++ )
            {
                start = end = null;

                foreach ( TileEntity pathTile in pathTiles )
                {
                    if ( pathTile.PathStartID == ( i ) )
                        start = pathTile;
                    if ( pathTile.PathEndID == ( i ) )
                        end = pathTile;
                }

                if ( start != null && end != null )
                {
                    Console.WriteLine( "Adding path: " + i );
                    NPCPaths.Add( new NPCPath( start, end ) );
                    pathTiles.Remove( start );
                    pathTiles.Remove( end );
                }
            }

            Loaded = true;
        }

        /// <summary>
        /// Returns a list of the tiles under a given point
        /// </summary>
        /// <param name="point">Point to look under</param>
        /// <returns>List of tiles under the given point</returns>
        public List<TileEntity> getTilesUnder( Vector2 point )
        {
            int xOff = (int) Math.Round( point.X / ( GameConstants.NUM_PIXEL * GameConstants.SCALE ) );
            int yOff = (int) Math.Round( point.Y / ( GameConstants.NUM_PIXEL * GameConstants.SCALE ) );

            return Tiles[yOff, xOff];
        }

        /// <summary>
        /// Returns the correct npc path for the given ID
        /// </summary>
        /// <param name="id">ID of the NPCPath</param>
        /// <returns>NPCPath for the given ID, null if not found</returns>
        public NPCPath getNPCPath( int id )
        {
            // Loop through all the NPCPaths
            foreach ( NPCPath iter in NPCPaths )
            {
                //Console.WriteLine( "Start: " + iter.Start + "\tEnd: " + iter.End );
                if ( iter.ID == id )
                    return iter;
            }
            return null;
        }

        /// <summary>
        /// Ticks all tiles in the level
        /// </summary>
        public void Tick()
        {
            // Go through all the lists of tiles
            foreach ( List<TileEntity> list in Tiles )
            {
                // Go through all the layers in each list
                foreach ( TileEntity tile in list )
                {
                    // Safety check
                    if ( tile != null )
                        tile.Tick();
                }
            }
        }

        /// <summary>
        /// Animates all tiles in the level
        /// </summary>
        public void Animate()
        {
            // Go through all the lists of tiles
            foreach ( List<TileEntity> list in Tiles )
            {
                // Go through all the layers in each list
                foreach ( TileEntity tile in list )
                {
                    // Safety check
                    if ( tile != null )
                        tile.Animate();
                }
            }
        }

        /// <summary>
        /// Looks to see if there are any tile collisions between the two points given.
        /// </summary>
        /// <param name="start">Start point of the move</param>
        /// <param name="end">End point of the move</param>
        /// <returns>True if the move is valid, false otherwise</returns>
        public bool canMove( Vector2 start, Vector2 end )
        {
            List<TileEntity> list = new List<TileEntity>(), tempList;
            Vector2 tmp;
            bool endFound = false;

            // Find the difference between the two points
            Vector2 norm = Vector2.Subtract( end, start );
            norm.Normalize(); // Unit vector it

            // Loop through all unit lengths of the distance between start and end
            for ( int i = 0; i < Vector2.Subtract( start, end ).Length(); i++ )
            {
                // Find the vector to the new point
                tmp = norm * i;

                tempList = getTilesUnder( new Vector2( start.X + tmp.X, start.Y + tmp.Y ) );

                // Only loop through if the list is different
                if ( !tempList.Equals( list ) )
                {
                    list = tempList;

                    // Loop through all the tiles
                    foreach ( TileEntity tile in list )
                    {
                        // Look for a collision
                        if ( tile.Collide )
                            return false;

                        // Check to see if the current point is the ending tile
                        if ( tile.Position.Equals( end ) || tile.ContainsPoint( end ) )
                            endFound = true;
                    }
                }

                if ( endFound )
                    return true;
            }

            return false;
        }

        public void Save()
        {

        }
    }
}
