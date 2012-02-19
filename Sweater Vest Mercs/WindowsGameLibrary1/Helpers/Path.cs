using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SVMLib.Tiles;
using SVMLib.Entities;

namespace SVMLib.Helpers
{
    public class Path
    {
        public Queue<TileEntity> TilePath { get; set; }
        public Queue<Vector2> VectorPath { get; set; }
        public Level Level { get; set; }

        public Path()
        {
            VectorPath = new Queue<Vector2>();
            TilePath = new Queue<TileEntity>();
            Level = null;
        }

        public static bool isAdd( Entity ent, Vector2 pos, Level level )
        {
            List<TileEntity> tiles = new List<TileEntity>();
            tiles = level.getTilesUnder( pos );
            foreach ( TileEntity tile in tiles )
            {
                if ( tile.Collide )
                    return false;
            }

            foreach ( Entity e in GameConstants.LoadedLevel.Entities )
            {
                if ( ( e.ContainsPoint( pos ) || ( (MovingEntity) e ).Destination.Equals( pos ) ) && !e.Equals( ent ) )
                    if ( e.Collide )
                        return false;
            }

            return tiles.Count != 0;
        }

        public static Path findPath( Vector2 pos, Vector2 dest, Level level )
        {
            foreach ( Entity ent in GameConstants.LoadedLevel.Entities )
            {
                if ( ent.Position.Equals( pos ) )
                {
                    return findPath( ent, dest, level );
                }
            }

            return null;
        }

        public static Path findPath( Entity ent, Vector2 dest, Level level )
        {
            Path path = new Path();
            path.Level = level;
            List<Vector2> visited = new List<Vector2>();
            List<PathElement> elements = new List<PathElement>();
            Queue<PathElement> queue = new Queue<PathElement>();
            Vector2 pos = ent.Position;

            bool posFound = false, add;
            PathElement ele;
            Vector2 tmp, north, south, east, west;
            int dist = 0;

            visited.Add( dest );
            queue.Enqueue( new PathElement( dest, dist ) );
            elements.Add( new PathElement( dest, dist ) );
            while ( queue.Count != 0 )
            {
                ele = queue.Dequeue();
                dist = ele.Distance + 1;
                tmp = ele.Position;

                north = tmp + new Vector2( -GameConstants.SCALE * GameConstants.NUM_PIXEL, 0 );
                south = tmp + new Vector2( GameConstants.SCALE * GameConstants.NUM_PIXEL, 0 );
                east = tmp + new Vector2( 0, GameConstants.SCALE * GameConstants.NUM_PIXEL );
                west = tmp + new Vector2( 0, -GameConstants.SCALE * GameConstants.NUM_PIXEL );

                if ( !visited.Contains( north ) )
                {
                    add = true;
                    visited.Add( north );
                    add = isAdd( ent, north, level );
                    if ( add )
                    {
                        PathElement pe = new PathElement( north, dist );
                        elements.Add( pe );
                        queue.Enqueue( pe );
                        pe.Neighbors.Add( ele );
                        ele.Neighbors.Add( pe );
                    }
                }
                if ( !visited.Contains( south ) )
                {
                    add = true;
                    visited.Add( south );
                    add = isAdd( ent, south, level );
                    if ( add )
                    {
                        PathElement pe = new PathElement( south, dist );
                        elements.Add( pe );
                        queue.Enqueue( pe );
                        pe.Neighbors.Add( ele );
                        ele.Neighbors.Add( pe );
                    }
                }
                if ( !visited.Contains( east ) )
                {
                    add = true;
                    visited.Add( east );
                    add = isAdd( ent, east, level );
                    if ( add )
                    {
                        PathElement pe = new PathElement( east, dist );
                        elements.Add( pe );
                        queue.Enqueue( pe );
                        pe.Neighbors.Add( ele );
                        ele.Neighbors.Add( pe );
                    }
                }
                if ( !visited.Contains( west ) )
                {
                    add = true;
                    visited.Add( west );
                    add = isAdd( ent, west, level );
                    if ( add )
                    {
                        PathElement pe = new PathElement( west, dist );
                        elements.Add( pe );
                        queue.Enqueue( pe );
                        pe.Neighbors.Add( ele );
                        ele.Neighbors.Add( pe );
                    }

                }

                posFound = ( north.Equals( pos ) || south.Equals( pos ) || west.Equals( pos ) || east.Equals( pos ) );

                if ( posFound )
                    break;
            }

            // Find the positions path element
            PathElement currentElement = null;
            foreach ( PathElement iter in elements )
            {
                if ( iter.Position.Equals( pos ) )
                    currentElement = iter;
            }
            if ( currentElement == null )
            {
                //throw new InvalidOperationException( "Could not find the starting path element!" );
                return path;
            }

            while ( !path.VectorPath.Contains( dest ) )
            {
                foreach ( PathElement n in currentElement.Neighbors )
                {
                    if ( n.Distance < currentElement.Distance )
                        currentElement = n;
                }
                path.VectorPath.Enqueue( currentElement.Position );
                path.TilePath.Enqueue( level.getTilesUnder( currentElement.Position )[0] );
            }

            return path;
        }

        public static bool checkPath( Path p )
        {
            if ( p.VectorPath.Count < 2 )
                throw new InvalidOperationException( "A valid path must have at least two points." );
            Vector2 curr;

            while ( p.VectorPath.Count > 1 )
            {
                curr = p.VectorPath.Dequeue();
                if ( !p.Level.canMove( curr, p.VectorPath.Peek() ) )
                    return false;
            }

            return true;
        }

        public void shift( Vector2 point )
        {
            for ( int i = 0; i < VectorPath.Count; i++ )
            {
                //VectorPath[i] = VectorPath[i] + point;
            }
        }
    }

    public class PathElement
    {
        public Vector2 Position { get; set; }
        public int Distance { get; set; }
        public List<PathElement> Neighbors { get; set; }

        public PathElement( Vector2 pos, int dist )
        {
            Position = pos;
            Distance = dist;
            Neighbors = new List<PathElement>();
        }
    }
}
