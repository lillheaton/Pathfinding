using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

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
            return map.GetEnumerableNeighbours(new Vector2(that.Position.X, that.Position.Y)).Where(s => s != null && s.IsWalkable);
        }
    }
}