using System;
using Strapkint.IO;
using Buffer = Strapkint.IO.Buffer;
using Strapkint.Entities;

namespace Strapkint
{
    sealed partial class Viewport
    {
        public const ConsoleColor Light = ConsoleColor.White;
        public const ConsoleColor Dark = ConsoleColor.Black;

        readonly Buffer buffer;
        readonly CharInfo[,] lightEmptyBuffer, darkEmptyBuffer;

        CharInfo[,] emptyBuffer;

        ConsoleColor background, foreground;
        
        private bool _isDarkTheme;

        public Viewport(int width, int height, bool isDarkTheme = false)
        {
            Width = Math.Min(width, Console.LargestWindowWidth);
            Height = Math.Min(height, Console.LargestWindowHeight);

            Buffer.Resize(Width, Height);

            darkEmptyBuffer = new Buffer(Dark, Light, Width, Height).Array;
            lightEmptyBuffer = new Buffer(Light, Dark, Width, Height).Array;

            IsDarkTheme = isDarkTheme;

            buffer = new Buffer(background, foreground, Width, Height);
        }

        public int Width { get; }
        public int Height { get; }
        public Position Position { get; } = Position.Zero;
        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                _isDarkTheme = value;
                emptyBuffer = IsDarkTheme ? darkEmptyBuffer : lightEmptyBuffer;
                background = IsDarkTheme ? Dark : Light;
                foreground = IsDarkTheme ? Light : Dark;
            }
        }

        public void Write(EntityBase input)
        {
            Position position = input.ViewportPosition;

            for (int textY = 0; textY < input.Text.GetLength(1); ++textY)
            {
                int currentY = position.Y + textY;
                if (currentY >= 0 && currentY < Height)
                {
                    for (int textX = 0; textX < input.Text.GetLength(0); ++textX)
                    {
                        int currentX = position.X + textX;
                        char character = input[textX, textY];
                        if (currentX >= 0 && currentX < Width && character != '\0')
                        {
                            buffer[currentX, currentY].UnicodeChar = input[textX, textY];
                            ConsoleColor color = (IsDarkTheme ? input.DarkColorMap : input.LightColorMap)[textX, textY];
                            buffer.Color(background, color, currentX, currentY);
                        }
                    }
                }
            }
        }

        public void Clear()
        {
            Array.Copy(emptyBuffer, buffer.Array, buffer.Array.Length);
        }

        public void Redraw()
        {
            buffer.Redraw();
        }

        public void Move(Direction direction, Level level)
        {
            if (direction.HasFlag(Direction.Vertical))
            {
                if (direction.HasFlag(Direction.Plus))
                {
                    if (Position.Y > 0)
                    {
                        --Position.Y;
                    }
                }
                else if (Position.Y + Height < level.Height)
                {
                    ++Position.Y;
                }
            }
            else
            {
                if (direction.HasFlag(Direction.Plus))
                {
                    if (Position.X + Width < level.Width)
                    {
                        ++Position.X;
                    }
                }
                else if (Position.X > 0)
                {
                    --Position.X;
                }
            }
        }
    }
}
