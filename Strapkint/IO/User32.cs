using System;
using System.Runtime.InteropServices;

namespace Strapkint.IO
{
    static class User32
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(this IntPtr windowHandle, bool revert = false);

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(this IntPtr systemMenu, WindowMenu menu, int flags = 0x0);
    }
}
