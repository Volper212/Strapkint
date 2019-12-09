using System;

namespace Strapkint.Entities
{
    class BackgroundEntity : EntityBase
    {
        readonly Level level;

        public BackgroundEntity(char[,] text, Position position, Level level)
            : base(text, position, 0, ConsoleColor.Gray, ConsoleColor.DarkGray)
        {
            this.level = level;
            level.BackgroundEntities.Add(this);
        }

        public override Position ViewportPosition => new Position(Position.X, level.Height - Position.Y - Height) - Viewport.Position;
    }
}