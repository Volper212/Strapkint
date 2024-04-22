using System;

namespace Strapkint.Entities
{
    class Entity : EntityBase
    {
        private Level _level;

        public Entity(char[,] text, Position position, Level level, bool isCollidable = true, ConsoleColor darkColor = Viewport.Light, ConsoleColor lightColor = Viewport.Dark)
            : base(text, position, darkColor, lightColor)
        {
            _level = level;
            Level.Entities.Add(this);
            IsCollidable = isCollidable;
        }

        public bool IsCollidable { get; }
        
        public Level Level
        {
            get => _level;
            set
            {
                Level.Entities.Remove(this);
                _level = value;
                Level.Entities.Add(this);
            }
        }

        public override Position ViewportPosition => new Position(Position.X, Level.Height - Position.Y - Height) - Viewport.Position;
    }
}
