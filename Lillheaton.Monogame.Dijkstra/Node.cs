using System.Collections.Generic;

namespace Lillheaton.Monogame.Dijkstra
{
    public class Node
    {
        public IWaypoint Waypoint { get; set; }
        public float Distance { get; set; }
        public Node Previus { get; set; }
        public List<Node> RelatedNodes { get; set; }

        public Node()
        {
            this.RelatedNodes = new List<Node>();
        }
    }
}