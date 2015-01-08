using Microsoft.Xna.Framework;

namespace Lillheaton.Monogame.Pathfinding.Node
{
    public class TileNode : INode
    {
        public Vector2 Position { get; set; }
        public ITile Tile { get; set; }
        public INode Parent { get; set; }
        public int G { get; set; }
        public float H { get; set; }
        public float F { get; set; }
    }
}
