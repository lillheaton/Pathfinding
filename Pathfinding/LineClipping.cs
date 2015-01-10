using Microsoft.Xna.Framework;

namespace Pathfinding
{
    public class LineClipping
    {
        public static bool LineIntersectsRect(Vector2 p1, Vector2 p2, Rectangle r)
        {
            return LineIntersectsLine(p1, p2, new Vector2(r.X, r.Y), new Vector2(r.X + r.Width, r.Y)) ||
                   LineIntersectsLine(p1, p2, new Vector2(r.X + r.Width, r.Y), new Vector2(r.X + r.Width, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Vector2(r.X + r.Width, r.Y + r.Height), new Vector2(r.X, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Vector2(r.X, r.Y + r.Height), new Vector2(r.X, r.Y)) ||
                   (r.Contains(p1) && r.Contains(p2));
        }

        /// <summary>
        /// https://gist.github.com/ChickenProp/3194723
        /// http://www.dailyfreecode.com/code/liang-barsky-algorithm-line-clipping-709.aspx
        /// </summary>
        /// <param name="Vector2A"></param>
        /// <param name="Vector2B"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static bool LiangBarsky(Vector2 Vector2A, Vector2 Vector2B, Rectangle rect)
        {
            var u1 = 0.0f;
            var u2 = 1.0f;

            var dx = Vector2B.X - Vector2A.X;
            var t1 = Vector2A.X - rect.Left;
            var t2 = rect.Right - Vector2A.X;
            var t3 = Vector2A.Y - rect.Top;
            var t4 = rect.Bottom - Vector2A.Y;
            var t5 = -1 * dx;

            if (ClipTest(t5, t1, ref u1, ref u2) == true)
            {
                if (ClipTest(dx, t2, ref u1, ref u2) == true)
                {
                    var dy = Vector2B.Y - Vector2A.Y;
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

        private static bool LineIntersectsLine(Vector2 l1P1, Vector2 l1P2, Vector2 l2P1, Vector2 l2P2)
        {
            float q = (l1P1.Y - l2P1.Y) * (l2P2.X - l2P1.X) - (l1P1.X - l2P1.X) * (l2P2.Y - l2P1.Y);
            float d = (l1P2.X - l1P1.X) * (l2P2.Y - l2P1.Y) - (l1P2.Y - l1P1.Y) * (l2P2.X - l2P1.X);

            if (d == 0)
            {
                return false;
            }

            float r = q / d;

            q = (l1P1.Y - l2P1.Y) * (l1P2.X - l1P1.X) - (l1P1.X - l2P1.X) * (l1P2.Y - l1P1.Y);
            float s = q / d;

            if (r < 0 || r > 1 || s < 0 || s > 1)
            {
                return false;
            }

            return true;
        }
    }
}