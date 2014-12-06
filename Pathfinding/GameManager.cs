using System.Collections.Generic;
using System.Linq;

using Lillheaton.Monogame.Pathfinding;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pathfinding
{
    public class GameManager
    {
        private Tile[][] _map;
        private GraphicsDevice _graphicsDevice;
        private Texture2D _texture2d;
        private List<INode> _solution;

        public GameManager(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;

            this.Init();
        }

        private void Init()
        {
            _map = new Tile[30][];
            for (int i = 0; i < 30; i++)
            {
                _map[i] = new Tile[30];
                for (int j = 0; j < 30; j++)
                {
                    _map[i][j] = new Tile(new Vector3(i, j, 0));
                }
            }

            var start = _map[0][0];
            var goal = _map[10][10];

            _solution = Astar.CalculatePath(_map, start, goal).ToList();

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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var node in _solution)
            {
                spriteBatch.Draw(
                    _texture2d,
                    new Vector2((int)node.Tile.Position.X * Tile.TileSize, (int)node.Tile.Position.Y * Tile.TileSize),
                    Color.White);
            }
        }
    }
}
