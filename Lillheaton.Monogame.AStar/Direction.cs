using Microsoft.Xna.Framework;

namespace Lillheaton.Monogame.Pathfinding
{
    public static class Direction
    {
        public static Vector3 SouthEast = new Vector3(1, 1, 0);
        public static Vector3 NorthWest = -SouthEast;
        public static Vector3 NorthEast = new Vector3(1, -1, 0);
        public static Vector3 SouthWest = -NorthEast;

        public static Vector3 North = new Vector3(0, -1, 0);
        public static Vector3 South = -North;
        public static Vector3 East = new Vector3(1, 0, 0);
        public static Vector3 West = -East;
    }
}