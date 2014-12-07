using Lillheaton.Monogame.Pathfinding;
using Microsoft.Xna.Framework;

namespace Pathfinding
{
    public class Tile : ITile
    {
        public const int TileSize = 30;
        public Vector3 Position { get; private set; }
        public int Size {  get { return TileSize; } }
        public bool IsWalkable { get; set; }

        public Tile(Vector3 position)
        {
            this.Position = position;
        }
    }
}
