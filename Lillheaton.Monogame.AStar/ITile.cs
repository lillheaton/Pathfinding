
using Microsoft.Xna.Framework;

namespace Lillheaton.Monogame.Pathfinding
{
    public interface ITile
    {
        Vector3 Position { get; }
        int Size { get; }
        bool IsWalkable { get; }
    }
}