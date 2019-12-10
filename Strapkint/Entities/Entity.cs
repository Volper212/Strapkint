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

        //public Entity(char[,] text, Position position, Level level)
        //    : this(text, position, Corner.BottomLeft)
        //{
        //    IsFixed = false;
        //    _level = level;
        //    Level.CurrentEntities.Add(this);
        //}

        //public Entity(char[,] text, Position position, Corner corner = Corner.TopLeft)
        //    : base(text.GetLength(0), text.GetLength(1))
        //{
        //    Text = text;
        //    Position = position;
        //    DarkColorMap = new ConsoleColor?[Width, Height];
        //    LightColorMap = new ConsoleColor?[Width, Height];
        //    Corner = corner;
        //}

        //public char[,] Text { get; }
        //public virtual Position Position { get; }
        //public ConsoleColor?[,] DarkColorMap { get; }
        //public ConsoleColor?[,] LightColorMap { get; }
        //public bool IsFixed { get; } = true;
        //public Corner Corner { get; }


        //public char this[int x, int y]
        //{
        //    get => Text[x, y];
        //    set => Text[x, y] = value;
        //}

        //public Position GetPositionInside(Viewport viewport)
        //{
        //    Rectangle referenceRectangle = IsFixed ? (Rectangle)viewport : Level;
        //    var output = new Position(Position);
        //    if (Corner.HasFlag(Corner.Bottom))
        //    {
        //        output.Y = referenceRectangle.Height - Position.Y - Height;
        //    }
        //    if (Corner.HasFlag(Corner.Right))
        //    {
        //        output.X = referenceRectangle.Width - Position.X - Width;
        //    }
        //    if (!IsFixed)
        //    {
        //        output -= viewport.Position;
        //    }
        //    return output;
        //}
    }
}
