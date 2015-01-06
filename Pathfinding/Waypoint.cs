using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Pathfinding
{
    public class Waypoint
    {
        public List<Waypoint> RelatedPoints { get; set; }
        public Vector2 Position { get; set; }

        public Waypoint()
        {
            RelatedPoints = new List<Waypoint>();
        }
    }
}