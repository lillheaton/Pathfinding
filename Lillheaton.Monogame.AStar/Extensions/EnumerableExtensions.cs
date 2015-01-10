using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Lillheaton.Monogame.Pathfinding.Extensions
{
    public static class EnumerableExtensions
    {
        private static T GetNeigbour<T>(T[][] twoDimensionalArray, Vector2 position) where T : class
        {
            var xLength = twoDimensionalArray.Length;
            var yLength = twoDimensionalArray[0].Length;

            if (position.X > -1 && position.X < xLength && 
                position.Y > -1 && position.Y < yLength)
            {
                return twoDimensionalArray[(int)position.X][(int)position.Y];
            }

            return null;
        }

        public static IEnumerable<T> GetEnumerableNeighbours<T>(this T[][] twoDimensionalArray, Vector2 position) where T : class
        {
            yield return GetNeigbour(twoDimensionalArray, position + Direction.East);
            yield return GetNeigbour(twoDimensionalArray, position + Direction.West);
            yield return GetNeigbour(twoDimensionalArray, position + Direction.North);
            yield return GetNeigbour(twoDimensionalArray, position + Direction.South);

            yield return GetNeigbour(twoDimensionalArray, position + Direction.North + Direction.West);
            yield return GetNeigbour(twoDimensionalArray, position + Direction.North + Direction.East);
            yield return GetNeigbour(twoDimensionalArray, position + Direction.South + Direction.West);
            yield return GetNeigbour(twoDimensionalArray, position + Direction.South + Direction.East);
        }

        public static IEnumerable<T> GetNorthWestNeighbours<T>(this T[][] twoDimensionalArray, Vector2 position) where T : class
        {
            yield return GetNeigbour(twoDimensionalArray, position + Direction.North);
            yield return GetNeigbour(twoDimensionalArray, position + Direction.West);
            yield return GetNeigbour(twoDimensionalArray, position + Direction.North + Direction.West);
        }

        public static IEnumerable<T> GetNorthEastNeighbours<T>(this T[][] twoDimensionalArray, Vector2 position) where T : class
        {
            yield return GetNeigbour(twoDimensionalArray, position + Direction.North);
            yield return GetNeigbour(twoDimensionalArray, position + Direction.East);
            yield return GetNeigbour(twoDimensionalArray, position + Direction.North + Direction.East);
        }

        public static IEnumerable<T> GetSouthWestNeighbours<T>(this T[][] twoDimensionalArray, Vector2 position) where T : class
        {
            yield return GetNeigbour(twoDimensionalArray, position + Direction.South);
            yield return GetNeigbour(twoDimensionalArray, position + Direction.West);
            yield return GetNeigbour(twoDimensionalArray, position + Direction.South + Direction.West);
        }

        public static IEnumerable<T> GetSouthEastNeighbours<T>(this T[][] twoDimensionalArray, Vector2 position) where T : class
        {
            yield return GetNeigbour(twoDimensionalArray, position + Direction.South);
            yield return GetNeigbour(twoDimensionalArray, position + Direction.East);
            yield return GetNeigbour(twoDimensionalArray, position + Direction.South + Direction.East);
        }
    }
}