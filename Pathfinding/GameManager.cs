using Lillheaton.Monogame.Pathfinding.Node;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pathfinding.Graphics;
using System;
using System.Collections.Generic;

namespace Pathfinding
{
    public class GameManager
    {
        private Game1 _game;
        private World _world;
        private BasicGraphicsHelper _graphicsHelper;

        private Unit _unit;
        private List<Vector2> _solution;
        private List<TileNode> _visitedNodes; 
        private PrimitiveBatch _primitiveBatch;

        public GameManager(Game1 game)
        {
            this._game = game;
            this.Init();
        }

        private void Init()
        {
            _world = new World(_game.GraphicsDevice, _game.TileWidth, _game.TileHeight);
            _graphicsHelper = new BasicGraphicsHelper(_game);
            _unit = new Unit(new Vector3(50, 50, 0), _world);
            _primitiveBatch = new PrimitiveBatch(_game.GraphicsDevice);
        }

        private void UpdateMouseInput()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                _unit.MoveToPosition(new Vector2(Mouse.GetState().X, Mouse.GetState().Y));
            }
        }

        public void Update(GameTime gameTime)
        {
            this.UpdateMouseInput();
            _unit.Update(gameTime);
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

            _unit.Draw(_primitiveBatch);
        }
    }
}
