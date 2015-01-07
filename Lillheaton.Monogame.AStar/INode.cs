
namespace Lillheaton.Monogame.Pathfinding
{
    public interface INode
    {
        INode Parent { get; set; }
        int G { get; set; }
        float H { get; set; }
        float F { get; set; }
    }
}
