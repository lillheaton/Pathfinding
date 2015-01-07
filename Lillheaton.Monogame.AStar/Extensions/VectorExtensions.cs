using Microsoft.Xna.Framework;

namespace Lillheaton.Monogame.Pathfinding.Extensions
{
    public static class VectorExtensions
    {
        public static bool IsDiagonalTo(this Vector2 that, Vector2 goal)
        {
            if (that + Direction.SouthEast == goal
                || that + Direction.SouthWest == goal
                || that + Direction.NorthEast == goal
                || that + Direction.NorthWest == goal)
            {
                return true;
            }

            return false;
        }
    }
}
