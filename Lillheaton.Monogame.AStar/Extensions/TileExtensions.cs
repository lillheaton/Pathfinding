using System.Collections.Generic;

namespace Lillheaton.Monogame.Pathfinding.Extensions
{
    public static class TileExtensions
    {
        public static bool IsDiagonalTo(this ITile that, ITile tile)
        {
            if (that.Position + Direction.SouthEast == tile.Position
                || that.Position + Direction.SouthWest == tile.Position
                || that.Position + Direction.NorthEast == tile.Position
                || that.Position + Direction.NorthWest == tile.Position)
            {
                return true;
            }

            return false;
        }


        public static IEnumerable<ITile> GetNeighbours(this ITile that, ITile[][] map)
        {
            var xLength = map.Length;
            var yLength = map[0].Length;

            // East
            if (that.Position.X + 1 < xLength)
            {
                if (map[(int)(that.Position.X + 1)][(int)that.Position.Y].IsWalkable)
                {
                    yield return map[(int)(that.Position.X + 1)][(int)that.Position.Y];    
                }
            }

            // West
            if (that.Position.X - 1 > 0)
            {
                if (map[(int)(that.Position.X - 1)][(int)that.Position.Y].IsWalkable)
                {
                    yield return map[(int)(that.Position.X - 1)][(int)that.Position.Y];    
                }
            }

            // North
            if (that.Position.Y - 1 > 0)
            {
                if (map[(int)(that.Position.X)][(int)that.Position.Y - 1].IsWalkable)
                {
                    yield return map[(int)(that.Position.X)][(int)that.Position.Y - 1];    
                }
            }

            // South
            if (that.Position.Y + 1 < yLength)
            {
                if (map[(int)(that.Position.X)][(int)that.Position.Y + 1].IsWalkable)
                {
                    yield return map[(int)(that.Position.X)][(int)that.Position.Y + 1];    
                }
            }

            // North-West
            if (that.Position.Y - 1 > 0 && that.Position.X - 1 > 0)
            {
                if (map[(int)(that.Position.X - 1)][(int)that.Position.Y - 1].IsWalkable)
                {
                    yield return map[(int)(that.Position.X - 1)][(int)that.Position.Y - 1];    
                }
            }

            // North-East
            if (that.Position.Y - 1 > 0 && that.Position.X + 1 < xLength)
            {
                if (map[(int)(that.Position.X + 1)][(int)that.Position.Y - 1].IsWalkable)
                {
                    yield return map[(int)(that.Position.X + 1)][(int)that.Position.Y - 1];    
                }
            }

            // South-West
            if (that.Position.Y + 1 < yLength && that.Position.X - 1 > 0)
            {
                if (map[(int)(that.Position.X - 1)][(int)that.Position.Y + 1].IsWalkable)
                {
                    yield return map[(int)(that.Position.X - 1)][(int)that.Position.Y + 1];    
                }
            }

            // South-East
            if (that.Position.Y + 1 < yLength && that.Position.X + 1 < xLength)
            {
                if (map[(int)(that.Position.X + 1)][(int)that.Position.Y + 1].IsWalkable)
                {
                    yield return map[(int)(that.Position.X + 1)][(int)that.Position.Y + 1];    
                }
            }
        }
    }
}