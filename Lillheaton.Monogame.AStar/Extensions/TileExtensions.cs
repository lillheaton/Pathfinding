using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Lillheaton.Monogame.Pathfinding.Extensions
{
    public static class TileExtensions
    {
        public static IEnumerable<ITile> GetNeighbours(this ITile that, ITile[][] map)
        {
            return map.GetEnumerableNeighbours(new Vector2(that.Position.X, that.Position.Y)).Where(s => s != null && s.IsWalkable);
        }
    }
}