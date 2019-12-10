using System;

namespace Strapkint
{
    class Position
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Position(Position position)
        {
            X = position.X;
            Y = position.Y;
        }

        public static Position Zero => new Position(0, 0);

        public int X { get; set; }
        public int Y { get; set; }

        public int this[Axis axis]
        {
            get => axis == Axis.Horizontal ? X : Y;
            set
            {
                if (axis == Axis.Horizontal)
                {
                    X = value;
                }
                else
                {
                    Y = value;
                }
            }
        }

        public static Position operator +(Position left, Size right)
        {
            return new Position(left.X + right.Width, left.Y + right.Height);
        }
        public static Position operator +(Position left, Position right)
        {
            return new Position(left.X + right.X, left.Y + right.Y);
        }

        public static Position operator -(Position left, Position right)
        {
            return new Position(left.X - right.X, left.Y - right.Y);
        }

        public static bool operator >(Position left, Position right)
        {
            return left.X > right.X && left.Y > right.Y;
        }

        public static bool operator <(Position left, Position right)
        {
            return left.X < right.X && left.Y < right.Y;
        }

        public static implicit operator PositionF(Position input)
        {
            return new PositionF(input.X, input.Y);
        }
    }

    class PositionF
    {
        public PositionF(double x, double y)
        {
            X = x;
            Y = y;
        }

        public PositionF(PositionF position)
        {
            X = position.X;
            Y = position.Y;
        }

        public static PositionF Zero => new PositionF(0, 0);

        public double X { get; set; }
        public double Y { get; set; }

        public double this[Axis axis]
        {
            get => axis == Axis.Horizontal ? X : Y;
            set
            {
                if (axis == Axis.Horizontal)
                {
                    X = value;
                }
                else
                {
                    Y = value;
                }
            }
        }

        public static PositionF operator +(PositionF left, Size right)
        {
            return new PositionF(left.X + right.Width, left.Y + right.Height);
        }

        public static PositionF operator +(PositionF left, PositionF right)
        {
            return new PositionF(left.X + right.X, left.Y + right.Y);
        }

        public static PositionF operator -(PositionF left, PositionF right)
        {
            return new PositionF(left.X - right.X, left.Y - right.Y);
        }

        public static bool operator >(PositionF left, PositionF right)
        {
            return left.X > right.X && left.Y > right.Y;
        }

        public static bool operator <(PositionF left, PositionF right)
        {
            return left.X < right.X && left.Y < right.Y;
        }

        public static explicit operator Position(PositionF input)
        {
            return new Position((int)Math.Round(input.X), (int)Math.Round(input.Y));
        }
    }
}
