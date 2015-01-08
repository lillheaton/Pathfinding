using Microsoft.Xna.Framework;

namespace Lillheaton.Monogame.Pathfinding.Node
{
    public class WaypointNode : INode
    {
        public Vector2 Position { get; set; }
        public IWaypoint Waypoint { get; set; }
        public INode Parent { get; set; }
        public int G { get; set; }
        public float H { get; set; }
        public float F { get; set; }
    }
}
