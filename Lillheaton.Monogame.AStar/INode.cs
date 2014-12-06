
namespace Lillheaton.Monogame.Pathfinding
{
    public interface INode
    {
        ITile Tile { get; set; }
        INode Parent { get; set; }
        int G { get; set; }
        float H { get; set; }
        float F { get; set; }
    }
}
