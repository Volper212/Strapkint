using System;

namespace Strapkint.Entities
{
    class Coin : Entity, IDisposable
    {
        const string text =
            "\0\0\0" +
            "\0©\0" +
            "\0\0\0";

        public Coin(Position position, Level level)
            : base(text.To2DCharArray(3), position - new Position(1, 1), level, false, ConsoleColor.Yellow, ConsoleColor.DarkYellow)
        {
        }

        public void Dispose()
        {
            Level.Entities.Remove(this);
        }
    }
}
