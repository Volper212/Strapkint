namespace Strapkint.Entities
{
    class FixedEntity : EntityBase
    {
        readonly Corner corner;

        public FixedEntity(char[,] text, Position position, Corner corner = Corner.TopLeft)
            : base(text, position, 2)
        {
            this.corner = corner;
        }

        public override Position ViewportPosition
        {
            get
            {
                var output = new Position(Position);
                if (corner.HasFlag(Corner.Bottom))
                {
                    output.Y = Viewport.Height - Position.Y - Height;
                }
                if (corner.HasFlag(Corner.Right))
                {
                    output.X = Viewport.Width - Position.X - Width;
                }
                return output;
            }
        }
    }
}
