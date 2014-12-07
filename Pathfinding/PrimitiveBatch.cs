using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Pathfinding
{
    public class PrimitiveBatch
    {
        private GraphicsDevice _graphics;
        private BasicEffect _basicEffect;
        private VertexPositionColor[] _vertices;
        private int _positionInBuffer = 0;
        private PrimitiveType _primitiveType;
        private bool _hasBegun = false;

        public PrimitiveBatch(GraphicsDevice graphicsDevice)
        {
            this._graphics = graphicsDevice;
            
            this.Init();
        }

        private void Init()
        {
            var center = new Vector2(this._graphics.Viewport.Width * 0.5f, this._graphics.Viewport.Height * 0.5f);

            this._basicEffect = new BasicEffect(this._graphics);
            this._basicEffect.World = Matrix.Identity;
            this._basicEffect.View = Matrix.CreateLookAt(new Vector3(center, 0), new Vector3(center, 1), Vector3.Down);
            this._basicEffect.Projection = Matrix.CreateOrthographic(center.X * 2, center.Y * 2, -0.5f, 1);

            this._basicEffect.VertexColorEnabled = true;
            var rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;

            this._graphics.RasterizerState = rasterizerState;

            this._vertices = new VertexPositionColor[500];
        }

        public void Begin(PrimitiveType primitiveType)
        {
            if (this._hasBegun)
            {
                throw new Exception("You should not call the begin method twice");
            }

            this._primitiveType = primitiveType;

            //tell our basic effect to begin.
            this._basicEffect.CurrentTechnique.Passes[0].Apply();

            this._hasBegun = true;
        }

        public void End()
        {
            if (!this._hasBegun)
            {
                throw new Exception("Must call begin before you call end");
            }

            var primitivesCount = this._positionInBuffer / this.NumVertsPerPrimitive();

            if (this._primitiveType == PrimitiveType.LineStrip)
            {
                for (int i = 0; i < primitivesCount; i++)
                {
                    this._graphics.DrawUserPrimitives<VertexPositionColor>(this._primitiveType, this._vertices, i * this.NumVertsPerPrimitive(), this.NumVertsPerPrimitive() - 1);      
                }
            }
            else
            {
                if (primitivesCount > 0)
                {
                    this._graphics.DrawUserPrimitives<VertexPositionColor>(this._primitiveType, this._vertices, 0, primitivesCount);
                }    
            }

            this._positionInBuffer = 0;
            this._hasBegun = false;
        }

        public void AddVertices(VertexPositionColor[] vertices)
        {
            this.AddVerticesToBuffer(vertices);
        }

        private void AddVerticesToBuffer(VertexPositionColor[] vertices)
        {
            foreach (var vertexPositionColor in vertices)
            {
                this._vertices[this._positionInBuffer] = vertexPositionColor;
                this._positionInBuffer++;
            }
        }

        private int NumVertsPerPrimitive()
        {
            switch (this._primitiveType)
            {
                case PrimitiveType.TriangleList:
                    return 3;

                case PrimitiveType.LineList:
                    return 2;

                case PrimitiveType.LineStrip:
                    return 100;
            }

            return 0;
        }
    }
}