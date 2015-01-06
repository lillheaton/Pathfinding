using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Lillheaton.Monogame.Pathfinding.Extensions
{
    public static class EnumerableExtensions
    {
        private static Vector2 _east = new Vector2(1, 0);
        private static Vector2 _west = new Vector2(-1, 0);
        private static Vector2 _north = new Vector2(0, -1);
        private static Vector2 _south = new Vector2(0, 1);


        private static T GetNeigbour<T>(T[][] twoDimensionalArray, Vector2 position) where T : class
        {
            var xLength = twoDimensionalArray.Length;
            var yLength = twoDimensionalArray[0].Length;

            if (position.X > 0 && position.X < xLength && 
                position.Y > 0 && position.Y < yLength)
            {
                return twoDimensionalArray[(int)position.X][(int)position.Y];
            }

            return null;
        }

        public static IEnumerable<T> GetEnumerableNeighbours<T>(this T[][] twoDimensionalArray, Vector2 position) where T : class
        {
            yield return GetNeigbour(twoDimensionalArray, position + _east);
            yield return GetNeigbour(twoDimensionalArray, position + _west);
            yield return GetNeigbour(twoDimensionalArray, position + _north);
            yield return GetNeigbour(twoDimensionalArray, position + _south);

            yield return GetNeigbour(twoDimensionalArray, position + _north + _west);
            yield return GetNeigbour(twoDimensionalArray, position + _north + _east);
            yield return GetNeigbour(twoDimensionalArray, position + _south + _west);
            yield return GetNeigbour(twoDimensionalArray, position + _south + _east);
        }
    }
}