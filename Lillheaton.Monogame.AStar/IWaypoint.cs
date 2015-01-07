using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Lillheaton.Monogame.Pathfinding
{
    public interface IWaypoint
    {
        Vector2 Position { get; }
        List<IWaypoint> RelatedPoints { get; }
    }
}
