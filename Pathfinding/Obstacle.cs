using Lillheaton.Monogame.Steering;
using Microsoft.Xna.Framework;

namespace Pathfinding
{
    public class Obstacle : IRectangleObstacle
    {
        public Vector3 Center { get; set; }
        public Rectangle Rectangle { get; private set; }
        public Vector2 Position { get; private set; }

        public Obstacle(Vector2 position)
        {
            this.Position = position;
            this.Rectangle = new Rectangle((int)position.X, (int)position.Y, Tile.TileSize, Tile.TileSize);
            this.Center = new Vector3(position.X + Tile.TileSize / 2, position.Y + Tile.TileSize / 2, 0);
        }
    }
}