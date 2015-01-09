using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Lillheaton.Monogame.Dijkstra
{
    public interface IWaypoint
    {
        Vector2 Position { get; }
        List<IWaypoint> RelatedPoints { get; }
    }
}