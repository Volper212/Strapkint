using System;

namespace Strapkint.Entities
{
    class Spike : Entity
    {
        const string pattern = "◄▼►▲";

        public Spike(Direction direction, int length, Position position, Level level)
            : base(MakeSpikeText(direction, length), position, level, false, ConsoleColor.Gray, ConsoleColor.DarkGray)
        {
        }

        static char[,] MakeSpikeText(Direction direction, int length)
        {
            Axis axis = direction.GetAxis();
            string output = new string(pattern[(int)direction], length);
            return output.To2DCharArray(axis == Axis.Vertical ? length : 1);
        }
    }
}
