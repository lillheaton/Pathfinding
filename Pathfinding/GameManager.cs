using Lillheaton.Monogame.Pathfinding;
using Lillheaton.Monogame.Steering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pathfinding.Graphics;
using Pathfinding.Primitives;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding
{
    public class GameManager
    {
        private Game1 _game;
        private World _world;
        private BasicGraphicsHelper _graphicsHelper;

        private List<INode> _solution;
        private List<INode> _visitedNodes; 
        private Triangle _triangle;
        private PrimitiveBatch _primitiveBatch;

        public GameManager(Game1 game)
        {
            this._game = game;
            this.Init();
        }

        private void Init()
        {
            _world = new World(_game.GraphicsDevice, 16, 16);
            _graphicsHelper = new BasicGraphicsHelper(_game);

            var start = _world.Tiles[0][0];
            var goal = _world.Tiles[11][10];

            _solution = Astar.CalculatePath(_world.Tiles, start, goal, out _visitedNodes).Reverse().ToList();
            
            _triangle = new Triangle(new Vector3(0, 0, 0));
            _triangle.SetPath(new Path(this._solution.Select(s => new Vector3(s.Tile.Position.X, s.Tile.Position.Y, 0) * Tile.TileSize + new Vector3(Tile.CenterVector, 0)).ToList()));
            _triangle.Obstacles = _world.Obstacles;

            _primitiveBatch = new PrimitiveBatch(_game.GraphicsDevice);
        }

        public void Update(GameTime gameTime)
        {
            _triangle.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // Draw world
            _world.Draw(spriteBatch, _primitiveBatch);

            // Draw solution nodes
            //foreach (var node in _solution)
            //{
            //    _graphicsHelper.DrawNode(spriteBatch, node);
            //}

            //foreach (var visitedNode in _visitedNodes)
            //{
            //    _graphicsHelper.DrawNodeInformation(spriteBatch, visitedNode);
            //}

            spriteBatch.End();

            _primitiveBatch.Begin(PrimitiveType.TriangleList);
            _triangle.Draw(_primitiveBatch);
            _primitiveBatch.End();
        }
    }
}
