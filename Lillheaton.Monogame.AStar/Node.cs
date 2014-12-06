
namespace Lillheaton.Monogame.Pathfinding
{
    public class Node : INode
    {
        public ITile Tile { get; set; }
        public INode Parent { get; set; }
        public int G { get; set; }
        public float H { get; set; }
        public float F { get; set; }
    }
}
