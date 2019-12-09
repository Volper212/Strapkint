using System;

namespace Strapkint.IO
{
    static class Window
    {
        static readonly IntPtr handle = Kernel32.GetConsoleWindow();
        static readonly IntPtr systemMenu = handle.GetSystemMenu();

        public static void DeleteMenus(params WindowMenu[] menus)
        {
            foreach (WindowMenu menu in menus)
            {
                systemMenu.DeleteMenu(menu);
            }
        }
    }
}
