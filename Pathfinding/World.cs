﻿using System.Threading;

using Lillheaton.Monogame.Pathfinding.Extensions;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding
{
    public class World
    {
        private int _width;
        private int _height;
        private Texture2D _wallTexture;

        public Tile[][] Tiles { get; private set; }
        public Obstacle[] Obstacles { get; private set; }
        public List<Waypoint> Waypoints { get; private set; }

        public World(GraphicsDevice graphicsDevice, int width, int height)
        {
            _width = width;
            _height = height;

            this.Init(graphicsDevice);
        }

        public void Init(GraphicsDevice graphicsDevice)
        {
            this.GenerateWorld();
            Obstacles = this.GenerateObstacles().ToArray();
            Waypoints = new List<Waypoint>();

            this.CalculateWaypoints();

            _wallTexture = new Texture2D(graphicsDevice, Tile.TileSize, Tile.TileSize);
            Color[] colorData = new Color[Tile.TileSize * Tile.TileSize];
            for (int i = 0; i < colorData.Length; ++i)
            {
                colorData[i] = Color.Brown;
            }
            _wallTexture.SetData(colorData);
        }

        private void GenerateWorld()
        {
            Tiles = new Tile[_width][];
            for (int i = 0; i < _width; i++)
            {
                Tiles[i] = new Tile[_height];
                for (int j = 0; j < _height; j++)
                {
                    Tiles[i][j] = new Tile(new Vector2(i, j));
                    Tiles[i][j].IsWalkable = true;
                }
            }
        }

        private void CalculateWaypoints()
        {
            // http://www.redblobgames.com/pathfinding/grids/algorithms.html
            // http://www.redblobgames.com/pathfinding/a-star/implementation.html
            // http://simblob.blogspot.se/2014/02/pathfinding-for-tower-defense-games.html

            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    if (Tiles[i][j].IsWalkable)
                    {
                        continue;
                    }

                    if (Tiles.GetNorthEastNeighbours(Tiles[i][j].Position).Count(s => s != null && s.IsWalkable) == 3)
                    {
                        var tile = Tiles[i + 1][j - 1];
                        Waypoints.Add(new Waypoint { Position = tile.Position });
                    }

                    if (Tiles.GetNorthWestNeighbours(Tiles[i][j].Position).Count(s => s != null && s.IsWalkable) == 3)
                    {
                        var tile = Tiles[i - 1][j - 1];
                        Waypoints.Add(new Waypoint { Position = tile.Position });
                    }

                    if (Tiles.GetSouthEastNeighbours(Tiles[i][j].Position).Count(s => s != null && s.IsWalkable) == 3)
                    {
                        var tile = Tiles[i + 1][j + 1];
                        Waypoints.Add(new Waypoint { Position = tile.Position });
                    }

                    if (Tiles.GetSouthWestNeighbours(Tiles[i][j].Position).Count(s => s != null && s.IsWalkable) == 3)
                    {
                        var tile = Tiles[i - 1][j + 1];
                        Waypoints.Add(new Waypoint { Position = tile.Position });
                    }    
                }
            }

            this.CalculateRelatedWaypoints();
        }

        private void CalculateRelatedWaypoints()
        {
            // Loop through every waypoint
            foreach (var waypoint in Waypoints)
            {
                // Loop through all again to see if any waypoint is visible to each other
                foreach (var nestedWaypoint in Waypoints.Where(s => s != waypoint))
                {
                    bool collide = false;
                    foreach (var obstacle in Obstacles)
                    {
                        if (LiangBarsky.Collides(waypoint.Position * Tile.TileSize + new Vector2(Tile.TileSize / 2, Tile.TileSize / 2), nestedWaypoint.Position * Tile.TileSize + new Vector2(Tile.TileSize / 2, Tile.TileSize / 2), obstacle.Rectangle))
                        {
                            collide = true;
                        }
                    }

                    if (collide == false)
                    {
                        waypoint.RelatedPoints.Add(nestedWaypoint);    
                    }
                }
            }
        }

        private IEnumerable<Obstacle> GenerateObstacles()
        {
            for (int i = 0; i < 15; i++)
            {
                Tiles[5][i].IsWalkable = false;
                yield return new Obstacle(Tiles[5][i].Position * Tile.TileSize);
            }

            for (int i = 15; i > 1; i--)
            {
                Tiles[10][i].IsWalkable = false;
                yield return new Obstacle(Tiles[10][i].Position * Tile.TileSize);
            }
        }

        public void Draw(SpriteBatch spriteBatch, PrimitiveBatch primitiveBatch)
        {
            foreach (var obstacle in Obstacles)
            {
                spriteBatch.Draw(_wallTexture, new Vector2(obstacle.Position.X, obstacle.Position.Y), Color.White);
            }
            
            foreach (var waypoint in Waypoints)
            {
                var vertices = new List<VertexPositionColor>();
                primitiveBatch.Begin(PrimitiveType.LineList);
                
                foreach (var related in waypoint.RelatedPoints)
                {
                    vertices.Add(new VertexPositionColor(new Vector3(waypoint.Position, 0) * Tile.TileSize + new Vector3(Tile.TileSize / 2, Tile.TileSize / 2, 0), Color.Red));
                    vertices.Add(new VertexPositionColor(new Vector3(related.Position, 0) * Tile.TileSize + new Vector3(Tile.TileSize / 2, Tile.TileSize / 2, 0), Color.Red));
                }

                primitiveBatch.AddVertices(vertices.ToArray());
                primitiveBatch.End();
            }
            
        }
    }
}