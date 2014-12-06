using Lillheaton.Monogame.Pathfinding.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Lillheaton.Monogame.Pathfinding
{
    public class Astar
    {
        public static IEnumerable<INode> CalculatePath(ITile[][] map, ITile start, ITile goal)
        {
            var closedSet = new List<INode>();
            var openSet = new List<INode>();
            int tileSize = start.Size;

            // Open set should contain the start
            openSet.Add(new Node { Tile = start, G = 0, F = 0, H = 0 });

            while (!openSet.Any())
            {
                // Get the node with lowest F value
                var current = openSet.First(s => s.F == openSet.Min(n => n.F));

                if (current.Tile == goal)
                {
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
                    int g = current.Tile.IsDiagonalTo(neighbor) ? 14 : 10;
                    float h = ManhattanHeuristic(current.Tile, neighbor);
                    float f = h + g;
                    //float h = Vector3.Distance(current.Tile.Position * (tileSize / 2), neighbor.Position * (tileSize / 2));

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

        public static float ManhattanHeuristic(ITile start, ITile goal)
        {
            var xd = start.Position.X - goal.Position.X;
            var yd = start.Position.X - goal.Position.Y;

            return Math.Abs(xd) + Math.Abs(yd);
        }
    }
}