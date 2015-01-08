using Lillheaton.Monogame.Pathfinding.Extensions;
using Lillheaton.Monogame.Pathfinding.Node;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lillheaton.Monogame.Pathfinding
{
    public class Astar
    {
        public static IEnumerable<Vector2> CalculatePath(ITile[][] map, ITile start, ITile goal, out List<TileNode> visitedNodes)
        {
            var closedSet = new List<TileNode>();
            var openSet = new List<TileNode>();

            // Open set should contain the start
            openSet.Add(new TileNode { Tile = start, Position = start.Position, G = 0, F = 0, H = 0 });

            while (openSet.Any())
            {
                // Get the node with lowest F value
                var current = openSet.First(s => s.F == openSet.Min(n => n.F));

                if (current.Tile == goal)
                {
                    visitedNodes = new List<TileNode>(openSet.Concat(closedSet));
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
                    int g = current.Tile.Position.IsDiagonalTo(neighbor.Position) ? 14 : 10;
                    float h = DiagonalHeuristic(neighbor.Position, goal.Position);
                    float f = h + g;

                    var node = openSet.FirstOrDefault(n => n.Tile == neighbor);
                    if (node == null)
                    {
                        node = new TileNode { Tile = neighbor, Position = neighbor.Position, Parent = current, H = h, F = f, G = g };
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
            visitedNodes = new List<TileNode>(openSet.Concat(closedSet));
            return null;
        }

        public static IEnumerable<Vector2> CalculatePath(IWaypoint[] waypoints, IWaypoint start, IWaypoint goal, out List<WaypointNode> visitedNodes)
        {
            var closedSet = new List<WaypointNode>();
            var openSet = new List<WaypointNode>();
            
            // Open set should contain the start
            openSet.Add(new WaypointNode { Waypoint = start, Position = start.Position, G = 0, F = 0, H = 0 });

            while (openSet.Any())
            {
                // Get the node with lowest F value
                var current = openSet.First(s => s.F == openSet.Min(n => n.F));

                if (current.Waypoint == goal)
                {
                    visitedNodes = new List<WaypointNode>(openSet.Concat(closedSet));
                    return ReconstructPath(current);
                }

                // We have checked the current node, now remove it from open and add it to closed
                openSet.Remove(current);
                closedSet.Add(current);

                // Loop through all neighbors
                foreach (var neighbor in current.Waypoint.RelatedPoints)
                {
                    if (closedSet.Any(s => s.Waypoint == neighbor))
                    {
                        continue;
                    }

                    // If diagonal g value is 14 otherwise 10
                    int g = current.Waypoint.Position.IsDiagonalTo(neighbor.Position) ? 14 : 10;
                    float h = DiagonalHeuristic(neighbor.Position, goal.Position);
                    float f = h + g;

                    var node = openSet.FirstOrDefault(n => n.Waypoint == neighbor);
                    if (node == null)
                    {
                        node = new WaypointNode { Waypoint = neighbor, Position = neighbor.Position, Parent = current, H = h, F = f, G = g };
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
            visitedNodes = new List<WaypointNode>(openSet.Concat(closedSet));
            return null;
        }

        private static IEnumerable<Vector2> ReconstructPath(INode goal)
        {
            var current = goal;
            yield return current.Position;
            while (current.Parent != null)
            {
                current = current.Parent;
                yield return current.Position;
            }
        }

        private static float DiagonalHeuristic(Vector2 from, Vector2 to)
        {
            var dx = Math.Abs(from.X - to.X);
            var dy = Math.Abs(from.Y - to.Y);            
            var d = from.IsDiagonalTo(to) ? 14 : 10;
            var d2 = Math.Sqrt(2) * d;

            return (float)(d * (dx + dy) + (d2 - 2 * d) * Math.Min(dx, dy));
        }
    }
}