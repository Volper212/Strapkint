using System;
using System.Runtime.InteropServices;

namespace Strapkint.IO
{
    static class Kernel32
    {
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool WriteConsoleOutput(
            this IntPtr outputHandle,
            [MarshalAs(UnmanagedType.LPArray), In] CharInfo[,] buffer,
            Coord bufferSize,
            Coord bufferStart,
            ref SmallRect writeRegion);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(StdHandle handle);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleScreenBufferInfoEx(
            this IntPtr outputHandle,
            ref ConsoleScreenBufferInfoEx bufferInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleScreenBufferInfoEx(
            this IntPtr outputHandle,
            ref ConsoleScreenBufferInfoEx bufferInfo);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool ReadConsoleInput(
            this IntPtr inputHandle,
            [Out] InputRecord[] records,
            int recordsLength,
            ref int eventsRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleMode(this IntPtr inputHandle, InputConsoleModes mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleMode(this IntPtr inputHandle, out InputConsoleModes mode);

        //[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        //public static extern bool GetCurrentConsoleFontEx(
        //    this IntPtr consoleOutput,
        //    bool maximumWindow,
        //    ref ConsoleFontInfoEx fontInfo);

        //[DllImport("kernel32.dll", SetLastError = true)]
        //public static extern bool SetCurrentConsoleFontEx(
        //    this IntPtr outputHandle,
        //    bool maximumWindow,
        //    ref ConsoleFontInfoEx fontInfo);
    }
}
