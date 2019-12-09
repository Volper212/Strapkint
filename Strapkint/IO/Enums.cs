using System;

namespace Strapkint.IO
{
    enum StdHandle
    {
        Input = -10,
        Output = -11,
        Error = -12
    }

    enum WindowMenu
    {
        Resize = 0xF000,
        Minimize = 0xF020,
        Maximize = 0xF030,
        Close = 0xF060
    }

    enum InputEvent
    {
        Key = 0x01,
        Mouse = 0x02,
        WindowBufferSize = 0x04,
        Menu = 0x08,
        Focus = 0x10
    }

    [Flags]
    enum InputConsoleModes : uint
    {
        ProcessedInput = 0x0001,
        LineInput = 0x0002,
        EchoInput = 0x0004,
        WindowInput = 0x0008,
        MouseInput = 0x0010,
        InsertMode = 0x0020,
        QuickEditMode = 0x0040,
        ExtendedFlags = 0x0080,
        VirtualTerminalInput = 0x0200
    }
}