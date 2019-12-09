using System;

namespace Strapkint.Entities
{
    abstract class EntityBase
    {
        public EntityBase(char[,] text, Position position, int zIndex = 0, ConsoleColor darkColor = Viewport.Light, ConsoleColor lightColor = Viewport.Dark)
        {
            ZIndex = zIndex;
            Text = text;
            Position = position;
            Width = Text.GetLength(0);
            Height = Text.GetLength(1);
            Size = new Size(Width, Height);
            DarkColorMap = new ConsoleColor[Width, Height];
            DarkColorMap.Fill(darkColor);
            LightColorMap = new ConsoleColor[Width, Height];
            LightColorMap.Fill(lightColor);
        }

        public static Viewport Viewport { get; set; }

        public int ZIndex { get; }
        public int Width { get; }
        public int Height { get; }
        public Size Size { get; }
        public char[,] Text { get; }
        public virtual Position Position { get; }
        public ConsoleColor[,] DarkColorMap { get; }
        public ConsoleColor[,] LightColorMap { get; }

        public char this[int x, int y]
        {
            get => Text[x, y];
            set => Text[x, y] = value;
        }

        public abstract Position ViewportPosition { get; }
        //public abstract Position GetViewportPosition(Viewport viewport);
    }
}
