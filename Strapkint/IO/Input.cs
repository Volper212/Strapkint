using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strapkint.IO
{
    class Input
    {
        static readonly IntPtr handle = Kernel32.GetStdHandle(StdHandle.Input);

        public void Run(ConsoleKey exitKey)
        {
            var records = new InputRecord[10];
            bool[] keyStates = new bool[byte.MaxValue];
            int recordsRead = 0;

            while (true)
            {
                handle.ReadConsoleInput(records, records.Length, ref recordsRead);

                foreach (KeyEventRecord record in records
                    .Where(record => record.EventType == (short)InputEvent.Key)
                    .Select(record => record.KeyEvent))
                {
                    var key = (ConsoleKey)record.KeyCode;
                    if (key == exitKey)
                    {
                        return;
                    }

                    if (keyStates[record.KeyCode] != record.KeyDown)
                    {
                        if (record.KeyDown)
                        {
                            OnKeyDown(key);
                        }
                        else
                        {
                            OnKeyUp(key);
                        }

                        keyStates[record.KeyCode] ^= true;
                    }
                }
            }
        }

        public delegate void KeyEventHandler(ConsoleKey key);

        public event KeyEventHandler OnKeyDown = delegate { };
        public event KeyEventHandler OnKeyUp = delegate { };

        public static void DisableModes(params InputConsoleModes[] modes)
        {
            handle.GetConsoleMode(out InputConsoleModes consoleMode);
            foreach (InputConsoleModes mode in modes)
            {
                consoleMode &= ~mode;
            }
            handle.SetConsoleMode(consoleMode | InputConsoleModes.ExtendedFlags);
        }
    }
}
