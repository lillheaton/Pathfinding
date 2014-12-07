using Lillheaton.Monogame.Pathfinding;
using Lillheaton.Monogame.Steering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pathfinding.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding
{
    public class GameManager
    {
        private GraphicsDevice _graphicsDevice;
        private World _world;

        private Texture2D _texture2d;
        private List<INode> _solution;
        private Triangle _triangle;
        private PrimitiveBatch _primitiveBatch;

        public GameManager(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;

            this.Init();
        }

        private void Init()
        {
            _world = new World(_graphicsDevice, 16, 16);

            var start = _world.Tiles[0][0];
            var goal = _world.Tiles[11][10];

            _solution = Astar.CalculatePath(_world.Tiles, start, goal).Reverse().ToList();
            
            _triangle = new Triangle(new Vector3(0, 0, 0));
            _triangle.SetPath(new Path(_solution.Select(s => s.Tile.Position * Tile.TileSize).ToList()));

            _primitiveBatch = new PrimitiveBatch(_graphicsDevice);

            _texture2d = new Texture2D(_graphicsDevice, Tile.TileSize, Tile.TileSize);
            Color[] data = new Color[Tile.TileSize * Tile.TileSize];
            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = Color.Black;
            }
            _texture2d.SetData(data);
        }

        public void Update(GameTime gameTime)
        {
            _triangle.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            _world.Draw(spriteBatch);

            foreach (var node in _solution)
            {
                spriteBatch.Draw(
                    _texture2d,
                    new Vector2((int)node.Tile.Position.X * Tile.TileSize, (int)node.Tile.Position.Y * Tile.TileSize),
                    Color.White);
            }

            spriteBatch.End();

            _primitiveBatch.Begin(PrimitiveType.TriangleList);
            _triangle.Draw(_primitiveBatch);
            _primitiveBatch.End();
        }
    }
}
