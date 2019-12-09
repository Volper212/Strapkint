using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Strapkint.IO
{
    static class Output
    {
        static ConsoleScreenBufferInfoEx bufferInfo = new ConsoleScreenBufferInfoEx();

        static Output()
        {
            bufferInfo.ByteSize = (uint)Marshal.SizeOf(bufferInfo);
            Handle.GetConsoleScreenBufferInfoEx(ref bufferInfo);
        }

        public static IntPtr Handle { get; } = Kernel32.GetStdHandle(StdHandle.Output);

        public static void CorrectScrollbars()
        {
            ++bufferInfo.WindowRect.Right;
            ++bufferInfo.WindowRect.Bottom;
            Handle.SetConsoleScreenBufferInfoEx(ref bufferInfo);
        }

        public static void SetBetterColors()
        {
            bufferInfo.Black = new Colorref(12, 12, 12);
            bufferInfo.DarkBlue = new Colorref(0, 55, 218);
            bufferInfo.DarkGreen = new Colorref(19, 161, 14);
            bufferInfo.DarkCyan = new Colorref(58, 150, 221);
            bufferInfo.DarkRed = new Colorref(197, 15, 31);
            bufferInfo.DarkMagenta = new Colorref(136, 23, 152);
            bufferInfo.DarkYellow = new Colorref(193, 156, 0);
            bufferInfo.Gray = new Colorref(204, 204, 204);
            bufferInfo.DarkGray = new Colorref(118, 118, 118);
            bufferInfo.Blue = new Colorref(59, 120, 255);
            bufferInfo.Green = new Colorref(22, 198, 12);
            bufferInfo.Cyan = new Colorref(97, 214, 214);
            bufferInfo.Red = new Colorref(231, 72, 86);
            bufferInfo.Magenta = new Colorref(180, 0, 158);
            bufferInfo.Yellow = new Colorref(249, 241, 165);
            bufferInfo.White = new Colorref(242, 242, 242);
            Handle.SetConsoleScreenBufferInfoEx(ref bufferInfo);
        }
    }
}
