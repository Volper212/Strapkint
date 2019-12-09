using System;

namespace Strapkint
{
    enum GameControl
    {
        Left,
        Right,
        Jump
    }

    [Flags]
    enum Corner
    {
        None = 0,
        Right = 1,
        Bottom = 2,
        TopLeft = None | None,
        TopRight = Right | None,
        BottomLeft = None | Bottom,
        BottomRight = Right | Bottom
    }

    enum Axis
    {
        Horizontal = 0,
        Vertical = 1
    }

    [Flags]
    enum Direction
    {
        None = 0,
        Vertical = 1,
        Plus = 2,
        Left = None | None,
        Right = Plus | None,
        Down = Vertical | None,
        Up = Vertical | Plus
    }

    [Flags]
    enum Directions
    {
        None = 0,
        Left = 1,
        Down = 2,
        Right = 4,
        Up = 8
    }
}
