using Lillheaton.Monogame.Pathfinding.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Lillheaton.Monogame.Pathfinding
{
    public class Astar
    {
        public static IEnumerable<INode> CalculatePath(ITile[][] map, ITile start, ITile goal, out List<INode> visitedNodes)
        {
            var closedSet = new List<INode>();
            var openSet = new List<INode>();

            // Open set should contain the start
            openSet.Add(new Node { Tile = start, G = 0, F = 0, H = 0 });

            while (openSet.Any())
            {
                // Get the node with lowest F value
                var current = openSet.First(s => s.F == openSet.Min(n => n.F));

                if (current.Tile == goal)
                {
                    visitedNodes = new List<INode>(openSet.Concat(closedSet));
                    return ReconstructPath(current);
                }

                // We have checked the current node, now remove it from open and add it to closed
                openSet.Remove(current);
                closedSet.Add(current);

                // Loop through all neighbors
                foreach (var neighbor in current.Tile.GetNeighbours(map))
                {                    
                    if (closedSet.Any(s => s.Tile == neighbor))
                    {
                        continue;
                    }

                    // If diagonal g value is 14 otherwise 10
                    int g = current.Tile.IsDiagonalTo(neighbor) ? 2 : 1;
                    float h = DiagonalHeuristic(neighbor, goal);
                    float f = h + g;

                    var node = openSet.FirstOrDefault(n => n.Tile == neighbor);
                    if (node == null)
                    {
                        node = new Node { Tile = neighbor, Parent = current, H = h, F = f, G = g };
                        openSet.Add(node);
                    }
                    else
                    {
                        if (f < node.F)
                        {
                            node.F = f;
                            node.Parent = current;
                        }
                    }
                }
            }
            visitedNodes = new List<INode>(openSet.Concat(closedSet));
            return null;
        }

        public static IEnumerable<INode> ReconstructPath(INode goal)
        {
            var current = goal;
            yield return current;
            while (current.Parent != null)
            {
                current = current.Parent;
                yield return current;
            }
        }

        public static float DiagonalHeuristic(ITile from, ITile to)
        {
            var dx = Math.Abs(from.Position.X - to.Position.X);
            var dy = Math.Abs(from.Position.Y - to.Position.Y);            
            var d = from.IsDiagonalTo(to) ? 14 : 10;
            var d2 = Math.Sqrt(2) * d;

            return (float)(d * (dx + dy) + (d2 - 2 * d) * Math.Min(dx, dy));
        }
    }
}