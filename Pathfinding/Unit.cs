using System.Collections.Generic;

using Lillheaton.Monogame.Pathfinding.Node;
using Lillheaton.Monogame.Steering;
using Lillheaton.Monogame.Steering.Behaviours;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pathfinding.Graphics;
using Pathfinding.Primitives;
using System.Linq;

namespace Pathfinding
{
    public class Unit : BaseUnit
    {
        private Triangle _triangle;
        private Path _path;
        private World _world;

        //private List<WaypointNode> _visitedNodes = new List<WaypointNode>(); 

        public Unit(Vector3 startPosition, World world) : base(startPosition)
        {
            this._world = world;
            this._triangle = new Triangle();
        }

        public void MoveToPosition(Vector2 toPosition)
        {
            // Reset path before starting a new
            this.SteeringBehavior.ResetPath();

            // First we check if we even need to calculate a path
            if (PathHelper.ClearViewFrom(new Vector2(this.Position.X, this.Position.Y), toPosition, this._world.Obstacles))
            {
                _path = new Path();
                _path.AddNode(new Vector3(toPosition, 0));
                return;
            }

            var path = PathHelper.CalculatePath(_world, new Vector2(this.Position.X, this.Position.Y), toPosition);
            _path = new Path(path.Select(s => new Vector3(s.X, s.Y, 0) * Tile.TileSize + new Vector3(Tile.CenterVector, 0)).ToList());
        }

        public override void Update(GameTime gameTime)
        {
            if (_path != null)
            {
                SteeringBehavior.FollowPath(_path);
                //SteeringBehavior.CollisionAvoidance(Obstacles);git
                //SteeringBehavior.Queue(WorldBoids);

                // Calculate Steering
                base.Update(gameTime);
            }

            // Update triangle rotation and postion
            _triangle.Update(gameTime, SteeringBehavior.Angle, this.Position);
        }

        public override void Draw(PrimitiveBatch primitiveBatch)
        {
            // Draw triangle
            primitiveBatch.Begin(PrimitiveType.TriangleList);
            _triangle.Draw(primitiveBatch);
            primitiveBatch.End();

            // Draw forces
            //primitiveBatch.Begin(PrimitiveType.LineList);
            //this.DrawForces(primitiveBatch);
            //primitiveBatch.End();
        }

        //public void DrawAStarData(SpriteBatch spriteBatch, BasicGraphicsHelper basicGraphicsHelper)
        //{
        //    spriteBatch.Begin();
        //    foreach (var waypointNode in _visitedNodes)
        //    {
        //        basicGraphicsHelper.DrawNodeInformation(spriteBatch, waypointNode);    
        //    }

        //    spriteBatch.End();
        //}
    }
}