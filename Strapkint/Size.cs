namespace Strapkint
{
    struct Size
    {
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }

        public int this[Axis axis] => axis == Axis.Horizontal ? Width : Height;
    }
}
