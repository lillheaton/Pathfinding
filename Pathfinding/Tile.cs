using Lillheaton.Monogame.Pathfinding;
using Microsoft.Xna.Framework;

namespace Pathfinding
{
    public class Tile : ITile
    {
        public const int TileSize = 30;
        public static Vector2 CenterVector = new Vector2(TileSize / 2, TileSize / 2);

        public Vector2 Position { get; private set; }
        public Rectangle Rectangle { get; private set; }
        public int Size {  get { return TileSize; } }
        public bool IsWalkable { get; set; }

        public Tile(Vector2 position)
        {
            this.Position = position;
            this.Rectangle = new Rectangle((int)position.X * TileSize, (int)position.Y * TileSize, TileSize, TileSize);
        }
    }
}
