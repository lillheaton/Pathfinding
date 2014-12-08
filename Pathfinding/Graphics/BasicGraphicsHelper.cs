using Lillheaton.Monogame.Pathfinding;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pathfinding.Graphics
{
    public class BasicGraphicsHelper
    {
        private Game1 _game;
        private Texture2D _tileTexture;

        private Color[] _black;
        private Color[] _red;
        private Color[] _green;

        public BasicGraphicsHelper(Game1 game)
        {
            this._game = game;
            this._tileTexture = new Texture2D(_game.GraphicsDevice, Tile.TileSize, Tile.TileSize);

            this.SetUpColorData();
        }

        private void SetUpColorData()
        {
            _black = new Color[Tile.TileSize * Tile.TileSize];
            for (int i = 0; i < _black.Length; ++i)
            {
                _black[i] = Color.Black;
            }

            _red = new Color[Tile.TileSize * Tile.TileSize];
            for (int i = 0; i < _red.Length; ++i)
            {
                _red[i] = Color.Red;
            }

            _green = new Color[Tile.TileSize * Tile.TileSize];
            for (int i = 0; i < _green.Length; ++i)
            {
                _green[i] = Color.Green;
            }
        }

        public void DrawNodeInformation(SpriteBatch spriteBatch, INode node)
        {
            if (_game.assetsManager == null)
            {
                return;
            }

            var vector = node.Tile.Position * Tile.TileSize;

            spriteBatch.DrawString(
                _game.assetsManager.FontDictionary["MyFont"],
                ((int)node.F).ToString(),
                new Vector2(vector.X, vector.Y),
                Color.White);
        }

        public void DrawNode(SpriteBatch spriteBatch, INode node)
        {
            this._tileTexture.SetData(_black);

            spriteBatch.Draw(
                this._tileTexture,
                new Vector2((int)node.Tile.Position.X * Tile.TileSize, (int)node.Tile.Position.Y * Tile.TileSize),
                Color.White);
        }
    }
}