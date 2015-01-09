using Lillheaton.Monogame.Steering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pathfinding.Primitives;
using System.Linq;

namespace Pathfinding
{
    public class Unit : BaseUnit
    {
        private Triangle _triangle;
        private Path _path;
        private World _world;

        public Unit(Vector3 startPosition, World world) : base(startPosition)
        {
            this._world = world;
            this.Obstacles = world.Obstacles;
            this._triangle = new Triangle();
        }

        public void MoveToPosition(Vector2 toPosition)
        {
            // First we check if we even need to calculate a path
            if (PathHelper.ClearViewFrom(new Vector2(this.Position.X, this.Position.Y), toPosition, this.Obstacles.Cast<Obstacle>().ToArray()))
            {
                _path = new Path();
                _path.AddNode(new Vector3(toPosition, 0));
                return;
            }

            var path = PathHelper.CalculatePath(_world, this.Obstacles.Cast<Obstacle>().ToArray(), new Vector2(this.Position.X, this.Position.Y), toPosition);
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
    }
}