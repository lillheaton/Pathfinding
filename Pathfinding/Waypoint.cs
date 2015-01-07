using Lillheaton.Monogame.Pathfinding;

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Pathfinding
{
    public class Waypoint : IWaypoint
    {
        public List<IWaypoint> RelatedPoints { get; set; }
        public Vector2 Position { get; set; }

        public Waypoint()
        {
            RelatedPoints = new List<IWaypoint>();
        }
    }
}