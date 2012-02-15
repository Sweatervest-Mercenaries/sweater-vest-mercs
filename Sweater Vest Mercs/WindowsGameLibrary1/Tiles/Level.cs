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
    public class Level
    {
        Texture2D texture;
        public List<TileEntity>[,] Tiles { get; set; }
        public List<NPCPath> NPCPaths { get; set; }
        public Boolean HasGravity { get; set; }
        public float Gravity { get; set; }
        public Vector2 spawnLoc;
        public int NumTilesX { get; set; }
        public int NumTilesY { get; set; }
        public bool Loaded { get; set; }

        public Level( Texture2D tex )
        {
            texture = tex;
            Tiles = new List<TileEntity>[tex.Height, tex.Width];
            NPCPaths = new List<NPCPath>();
            spawnLoc = new Vector2();
            Loaded = false;
            loadTiles();
        }

        public void DrawTiles( SpriteBatch batch )
        {
            foreach ( List<TileEntity> list in Tiles )
            {
                foreach ( TileEntity tile in list )
                {
                    if ( tile != null )
                        tile.Draw( batch );
                }
            }
        }

        private void loadTiles()
        {
            Color[] colors = new Color[texture.Width * texture.Height];
            List<TileEntity> pathTiles = new List<TileEntity>();
            texture.GetData<Color>( colors );
            float layer = 1;
            //int indexOffset = 0;
            int mapWidth = 0, mapHeight = 0;
            int b1 = -1, b2 = -1, b3 = -1, b4 = -1;
            int numSkip = 0;
            int row = 0, col = -1;
            bool skipRow = false;
            float x, y;
            Color c;
            TileEntity tile;

            for ( int i = 0; i < colors.Length; i++ )
            {
                c = colors[i];
                tile = null;

                col++;

                if ( c.Equals( Color.Black ) )
                {
                    //indexOffset = i + 1;
                    layer -= 0.01f;

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
                        i += texture.Width - 1;
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
                            numSkip = texture.Width - mapWidth;

                            // DEBUG
                            Console.WriteLine( "Map Width is: " + mapWidth );
                            Console.WriteLine( "Number to skip is: " + numSkip );
                        }

                        if ( b3 != -1 && mapHeight == 0 )
                        {
                            // We have at least read the first black on last line
                            mapHeight = ( i - texture.Width ) / texture.Width;
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
                    spawnLoc = new Vector2( col, row );
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

        public void Center( Vector2 newCenter )
        {
            foreach ( List<TileEntity> list in Tiles )
            {
                foreach ( TileEntity tile in list )
                {
                    if ( tile != null )
                        tile.Shift( newCenter );
                }
            }
        }

        public void Shift( Vector2 shiftVector )
        {
            foreach ( List<TileEntity> list in Tiles )
            {
                foreach ( TileEntity tile in list )
                {
                    if ( tile != null )
                        tile.Position += shiftVector;
                }
            }
        }

        public List<TileEntity> getTilesAt( float x, float y )
        {
            int xOff = (int) x % GameConstants.NUM_PIXEL;
            int yOff = (int) Math.Round( y / GameConstants.NUM_PIXEL );
            //List<TileEntity> matches = new List<TileEntity>();

            //foreach ( TileEntity tile in list )
            //{
            //    if ( tile != null )
            //    {
            //        if ( x >= tile.Position.X - ( GameConstants.NUM_PIXEL * GameConstants.SCALE ) && x <= tile.Position.X + ( GameConstants.NUM_PIXEL * GameConstants.SCALE )
            //          && y >= tile.Position.Y - ( GameConstants.NUM_PIXEL * GameConstants.SCALE )
            //          && y <= tile.Position.Y + ( GameConstants.NUM_PIXEL * GameConstants.SCALE ) )
            //        {
            //            matches.Add( tile );
            //        }
            //    }
            //}

            //return matches;
            return Tiles[yOff, xOff];
        }

        public List<TileEntity> getTilesUnder( float x, float y )
        {
            return getTilesUnder( new Vector2( x, y ) );
        }

        public List<TileEntity> getTilesUnder( Vector2 point )
        {
            int xOff = (int) Math.Round( point.X / ( GameConstants.NUM_PIXEL * GameConstants.SCALE ) );
            int yOff = (int) Math.Round( point.Y / ( GameConstants.NUM_PIXEL * GameConstants.SCALE ) );
            List<TileEntity> matches = new List<TileEntity>();

            foreach ( TileEntity tile in Tiles[yOff, xOff] )
            {
                if ( tile != null )
                {
                    if ( tile.ContainsPoint( point ) )
                    {
                        matches.Add( tile );
                    }
                }
            }

            return matches;
        }

        public NPCPath getNPCPath( int id )
        {
            foreach ( NPCPath iter in NPCPaths )
            {
                Console.WriteLine( "Start: " + iter.Start + "\tEnd: " + iter.End );
                if ( iter.ID == id )
                    return iter;
            }
            return null;
        }

        public void Tick()
        {
            foreach ( List<TileEntity> list in Tiles )
            {
                foreach ( TileEntity tile in list )
                {
                    if ( tile != null )
                        tile.Tick();
                }
            }
        }

        public void Animate()
        {
            foreach ( List<TileEntity> list in Tiles )
            {
                foreach ( TileEntity tile in list )
                {
                    if ( tile != null )
                        tile.Animate();
                }
            }
        }

        public TileEntity getSpawn()
        {
            foreach ( TileEntity tile in Tiles[(int) spawnLoc.Y, (int) spawnLoc.X] )
            {
                if ( tile != null )
                    if ( tile.Spawn )
                        return tile;
            }
            return null;
        }

        public bool canMove( Vector2 start, Vector2 end )
        {
            List<TileEntity> list;
            Vector2 tmp;
            bool endFound = false;
            Vector2 norm = Vector2.Subtract( end, start );
            norm.Normalize();

            for ( int i = 0; i < Vector2.Subtract( start, end ).Length(); i++ )
            {
                tmp = norm * i;

                list = getTilesUnder( start.X + tmp.X, start.Y + tmp.Y );
                foreach ( TileEntity tile in list )
                {
                    //Console.WriteLine("i: " + i + " Tile: " + tile.Position.ToString());
                    if ( tile.Collide )
                        return false;

                    if ( tile.Position.Equals( end ) )
                        endFound = true;
                }

                if ( endFound )
                    return true;
            }

            return false;
        }

        public bool canMove( MovingEntity entity, Vector2 dest )
        {
            List<TileEntity> list = getTilesUnder( dest.X, dest.Y );

            foreach ( TileEntity tile in list )
            {
                if ( tile.Collide )
                    return false;
            }

            return true;
        }

        public bool canMove( MovingEntity entity )
        {
            List<TileEntity> list = getTilesAt( entity.Position.X, entity.Position.Y );

            foreach ( TileEntity tile in list )
            {
                if ( tile.Collide )
                    return false;
            }

            return true;
        }
    }
}
