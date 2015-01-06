using Microsoft.Xna.Framework;

namespace Pathfinding
{
    public class LiangBarsky
    {
        /// <summary>
        /// https://gist.github.com/ChickenProp/3194723
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static bool Collides(Vector2 pointA, Vector2 pointB, Rectangle rect)
        {
            var p = new [] { -(pointA.X - pointB.X), (pointA.X - pointB.X), -(pointA.Y - pointB.Y), (pointA.X - pointB.X) };
            var q = new[] { (pointA.X - rect.Left), (rect.Right - pointA.X), (pointA.Y - rect.Top), (rect.Bottom - pointA.Y) };
            var u1 = double.NegativeInfinity;
            var u2 = double.PositiveInfinity;

            for (int i = 0; i < 4; i++)
            {
                if ((int)p[i] == 0)
                {
                    if (q[i] < 0)
                    {
                        return false;
                    }
                }
                else
                {
                    var t = q[i] / p[i];
                    if (p[i] < 0 && u1 < t)
                    {
                        u1 = t;
                    }
                    else if(p[i] > 0 && u2 > t)
                    {
                        u2 = t;
                    }
                }
            }

            if (u1 > u2 || u1 > 1 || u1 < 0)
            {
                return false;
            }

            return true;
        }
    }
}