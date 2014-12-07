using System.Threading;

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
                    Tiles[i][j] = new Tile(new Vector3(i, j, 0));
                    Tiles[i][j].IsWalkable = true;
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

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var obstacle in Obstacles)
            {
                spriteBatch.Draw(_wallTexture, new Vector2(obstacle.Position.X, obstacle.Position.Y), Color.White);
            }
        }
    }
}
