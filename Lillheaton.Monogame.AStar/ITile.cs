﻿
using Microsoft.Xna.Framework;

namespace Lillheaton.Monogame.Pathfinding
{
    public interface ITile
    {
        Vector2 Position { get; }
        int Size { get; }
        bool IsWalkable { get; }
    }
}