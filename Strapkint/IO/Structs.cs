using System;
using System.Runtime.InteropServices;

namespace Strapkint.IO
{
    [StructLayout(LayoutKind.Sequential)]
    struct ConsoleScreenBufferInfoEx
    {
        public uint ByteSize;
        public Coord BufferSize;
        public Coord CursorPosition;
        public short CharAttributes;
        public SmallRect WindowRect;
        public Coord MaxWindowSize;
        public ushort PopupAttributes;
        public bool FullscreenSupported;

        public Colorref Black, DarkBlue, DarkGreen, DarkCyan, DarkRed, DarkMagenta, DarkYellow, Gray, DarkGray, Blue, Green, Cyan, Red, Magenta, Yellow, White;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct Colorref
    {
        public uint ColorDWORD;

        public Colorref(int r, int g, int b)
        {
            ColorDWORD = (uint)r + ((uint)g << 8) + ((uint)b << 16);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    struct Coord
    {
        public short X, Y;

        public Coord(int x, int y)
        {
            X = (short)x;
            Y = (short)y;
        }
    }

    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Auto)]
    struct CharInfo
    {
        [FieldOffset(0)] public char UnicodeChar;
        [FieldOffset(2)] public short Attributes;

        public CharInfo(char unicodeChar, ConsoleColor background, ConsoleColor foreground)
        {
            UnicodeChar = unicodeChar;
            Attributes = (short)(((byte)background << 4) | (byte)foreground);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    struct SmallRect
    {
        public short Left, Top, Right, Bottom;

        public SmallRect(int width, int height)
        {
            Left = 0;
            Top = 0;
            Right = (short)width;
            Bottom = (short)height;
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    struct InputRecord
    {
        [FieldOffset(0)] public readonly short EventType;
        [FieldOffset(4)] public KeyEventRecord KeyEvent;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct KeyEventRecord
    {
        public readonly bool KeyDown;
        public readonly short RepeatCount;
        public readonly short KeyCode;
        public readonly short ScanCode;
        public readonly char UnicodeChar;
        public readonly int ControlKeyState;
    }
}