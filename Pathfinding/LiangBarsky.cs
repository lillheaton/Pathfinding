using Microsoft.Xna.Framework;

namespace Pathfinding
{
    public class LiangBarsky
    {
        /// <summary>
        /// https://gist.github.com/ChickenProp/3194723
        /// http://www.dailyfreecode.com/code/liang-barsky-algorithm-line-clipping-709.aspx
        /// </summary>
        /// <param name="pointA"></param>
        /// <param name="pointB"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static bool Collides(Vector2 pointA, Vector2 pointB, Rectangle rect)
        {
            var u1 = 0.0f;
            var u2 = 1.0f;

            var dx = pointB.X - pointA.X;
            var t1 = pointA.X - rect.Left;
            var t2 = rect.Right - pointA.X;
            var t3 = pointA.Y - rect.Top;
            var t4 = rect.Bottom - pointA.Y;
            var t5 = -1 * dx;

            if (ClipTest(t5, t1, ref u1, ref u2) == true)
            {
                if (ClipTest(dx, t2, ref u1, ref u2) == true)
                {
                    var dy = pointB.Y - pointA.Y;
                    var t6 = -1 * dy;

                    if (ClipTest(t6, t3, ref u1, ref u2) == true)
                    {
                        if (ClipTest(dy, t4, ref u1, ref u2) == true)
                        {
                            if (u1 > 0.0f)
                            {
                                return true;
                            }
                            if (u2 < 1.0f)
                            {
                                return true;
                            }
                        }
                    }

                }
            }

            return false;
        }

        private static bool ClipTest(float p, float q, ref float u1, ref float u2)
        {
            float r;
            bool flag = true;

            if (p < 0.0f)
            {
                r = q / p;
                if (r > u2)
                {
                    flag = false;
                }
                else if(r > u1)
                {
                    u2 = r;
                }
            }
            else if(p > 0.0f)
            {
                r = q / p;
                if (r < u1)
                {
                    flag = false;
                }
            }
            else if (q < 0.0f)
            {
                flag = false;
            }

            return flag;
        }
    }
}