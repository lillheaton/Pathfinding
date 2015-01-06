using Lillheaton.Monogame.Steering;
using Microsoft.Xna.Framework;

namespace Pathfinding
{
    public class Obstacle : IObstacle
    {
        public Vector3 Position { get; set; }
        public Vector3 Center { get; set; }

        public Obstacle(Vector3 position)
        {
            this.Position = position;
            this.Center = new Vector3(position.X + Tile.TileSize / 2, position.Y + Tile.TileSize / 2, 0);
        }

        public float GetRadius()
        {
            return Tile.TileSize;
        }
    }
}