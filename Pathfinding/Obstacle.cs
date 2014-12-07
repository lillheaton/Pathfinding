using Lillheaton.Monogame.Steering;
using Microsoft.Xna.Framework;

namespace Pathfinding
{
    public class Obstacle : IObstacle
    {
        public Vector3 Position { get; set; }

        public Obstacle(Vector3 position)
        {
            this.Position = position;
        }

        public float GetRadius()
        {
            return Tile.TileSize / 2;
        }
    }
}
