using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Lillheaton.Monogame.Dijkstra
{
    public class Dijkstra
    {
        public static IEnumerable<Vector2> CalculatePath(List<IWaypoint> graph, IWaypoint start, IWaypoint end)
        {
            // Initialize
            var searched = new List<Node>();
            var openSet =
                graph.Where(s => s != start)
                    .Select(vertex => new Node { Distance = float.MaxValue, Waypoint = vertex })
                    .ToList();

            foreach (var node in openSet)
            {
                node.RelatedNodes.AddRange(openSet.Where(s => node.Waypoint.RelatedPoints.Contains(s.Waypoint)));
            }

            // Add start node with distance 0
            var startNode = new Node { Distance = 0, Waypoint = start };
            startNode.RelatedNodes.AddRange(openSet.Where(s => start.RelatedPoints.Contains(s.Waypoint)));
            openSet.Add(startNode);
            searched.Add(startNode);

            // Run as long as there are more vertex to search
            while (openSet.Any())
            {
                // Get the current node with the lowest distance
                var current = openSet.First(s => s.Distance == openSet.Min(n => n.Distance));

                // Remove current from OpenSet
                openSet.Remove(current);

                foreach (var neighbor in current.RelatedNodes)
                {
                    var alt = current.Distance + Vector2.Distance(current.Waypoint.Position, neighbor.Waypoint.Position);
                    if (alt < neighbor.Distance)
                    {
                        neighbor.Distance = alt;
                        neighbor.Previus = current;
                    }
                }

                searched.Add(current);

                if (current.Waypoint == end)
                {
                    break;
                }
            }

            var endNode = searched.FirstOrDefault(s => s.Waypoint == end);
            if (endNode != null)
            {
                return ReconstructPath(endNode);
            }
            return null;
        }

        private static IEnumerable<Vector2> ReconstructPath(Node node)
        {
            var current = node;
            yield return current.Waypoint.Position;
            while (current.Previus != null)
            {
                current = current.Previus;
                yield return current.Waypoint.Position;
            }
        } 
    }
}