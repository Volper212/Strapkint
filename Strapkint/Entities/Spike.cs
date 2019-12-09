using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strapkint.Entities
{
    class Spike : Entity
    {
        //const string pattern = "˂˅˃˄";
        const string pattern = "◄▼►▲";

        public Spike(Direction direction, int length, Position position, Level level)
            : base(MakeSpikeText(direction, length), position, level, false, ConsoleColor.Gray, ConsoleColor.DarkGray)
        {
        }

        static char[,] MakeSpikeText(Direction direction, int length)
        {
            Axis axis = direction.GetAxis();
            //var output = new char[axis == Axis.Vertical ? 1 : length + 2, axis == Axis.Horizontal ? 1 : length + 2];
            string output = new string(pattern[(int)direction], length);
            return output.To2DCharArray(axis == Axis.Vertical ? length : 1);
        }
    }
}
