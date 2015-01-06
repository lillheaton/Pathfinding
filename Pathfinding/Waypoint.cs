using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Pathfinding
{
    public class Waypoint
    {
        public List<Waypoint> RelatedPoints { get; set; }
        public Vector3 Position { get; set; }
    }
}