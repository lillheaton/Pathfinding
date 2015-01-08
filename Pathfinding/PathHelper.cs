using Lillheaton.Monogame.Pathfinding;
using Lillheaton.Monogame.Pathfinding.Node;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding
{
    public class PathHelper
    {
        public static Vector2[] CalculatePath(Tile[][] map, Obstacle[] obstacles, Vector2 currentPosition, Vector2 goal)
        {
            // First we check if we even need to calculate a path
            if (ClearViewFrom(currentPosition, goal, obstacles))
            {
                return new Vector2[] { goal };
            }

            var xLength = map.Length;
            var yLength = map[0].Length;

            Tile currentTile = null;
            Tile goalTile = null;
            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    if (map[i][j].Rectangle.Contains(currentPosition))
                    {
                        currentTile = map[i][j];
                    }

                    if (map[i][j].Rectangle.Contains(goal))
                    {
                        goalTile = map[i][j];
                    }
                }
            }

            // Calculate path and return solution
            List<TileNode> visitedNodes;
            return Astar.CalculatePath(map, currentTile, goalTile, out visitedNodes).Reverse().ToArray();
        }

        public static Vector2[] CalculatePath(World world, Obstacle[] obstacles, Vector2 currentPosition, Vector2 goal)
        {
            // First we check if we even need to calculate a path
            if (ClearViewFrom(currentPosition, goal, obstacles))
            {
                return new Vector2[] { goal };
            }

            var xLength = world.Tiles.Length;
            var yLength = world.Tiles[0].Length;
            Tile currentTile = null;
            Tile goalTile = null;
            for (int i = 0; i < xLength; i++)
            {
                for (int j = 0; j < yLength; j++)
                {
                    if (world.Tiles[i][j].Rectangle.Contains(currentPosition))
                    {
                        currentTile = world.Tiles[i][j];
                    }

                    if (world.Tiles[i][j].Rectangle.Contains(goal))
                    {
                        goalTile = world.Tiles[i][j];
                    }
                }
            }
            // Create start position and goal as waypoints
            var startWaypoint = new Waypoint { Position = currentTile.Position };
            var endWaypoint = new Waypoint { Position = goalTile.Position };

            // Calculate there related waypoints
            World.CalculateRelatedWaypoints(startWaypoint, world.Waypoints, obstacles);
            World.CalculateRelatedWaypoints(endWaypoint, world.Waypoints, obstacles);

            // Copy list waypointList
            var waypointsCopy = world.Waypoints.Concat(new[] { startWaypoint, endWaypoint });

            // Those who is related to endWaypoint should also be related to end
            foreach (var relatedWaypoint in endWaypoint.RelatedPoints)
            {
                relatedWaypoint.RelatedPoints.Add(endWaypoint);
            }

            // Calculate path and return solution
            List<WaypointNode> visitedNodes;
            var solution = Astar.CalculatePath(waypointsCopy.ToArray(), startWaypoint, endWaypoint, out visitedNodes).Reverse().ToArray();

            // Need to do some cleanup
            foreach (var relatedWaypoint in endWaypoint.RelatedPoints)
            {
                relatedWaypoint.RelatedPoints.Remove(endWaypoint);
            }

            // Return the solution
            return solution;
        }

        public static bool ClearViewFrom(Vector2 pointA, Vector2 pointB, Obstacle[] obstacles)
        {
            bool clearView = true;
            foreach (var obstacle in obstacles)
            {
                if(LiangBarsky.Collides(pointA, pointB, obstacle.Rectangle))
                {
                    clearView = false;
                }
            }

            return clearView;
        }
    }
}