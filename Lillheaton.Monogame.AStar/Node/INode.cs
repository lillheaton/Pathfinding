using Microsoft.Xna.Framework;

namespace Lillheaton.Monogame.Pathfinding.Node
{
    public interface INode
    {
        Vector2 Position { get; set; }
        INode Parent { get; set; }
        int G { get; set; }
        float H { get; set; }
        float F { get; set; }
    }
}
