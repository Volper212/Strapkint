using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Strapkint.IO
{
    class Buffer
    {
        static readonly IntPtr handle = Output.Handle;
        static readonly Coord bufferStart = new Coord(0, 0);
        readonly Coord bufferSize;
        SmallRect writeRegion;

        public Buffer(ConsoleColor background, ConsoleColor foreground, int width, int height)
        {
            bufferSize = new Coord(width, height);
            writeRegion = new SmallRect(width, height);
            Array = new CharInfo[height, width];
            Array.Fill(new CharInfo(' ', background, foreground));
        }

        public CharInfo[,] Array { get; }
        public ref CharInfo this[int x, int y] => ref Array[y, x];

        public static void Resize(int width, int height)
        {
            Console.SetWindowSize(width - 2, height);
            Console.SetBufferSize(width, height);
        }

        public void Redraw()
        {
            handle.WriteConsoleOutput(Array, bufferSize, bufferStart, ref writeRegion);
        }

        public void Color(ConsoleColor background, ConsoleColor foreground, int x, int y)
        {
            this[x, y].Attributes = (short)(((byte)background << 4) | (byte)foreground);
        }
    }
}
