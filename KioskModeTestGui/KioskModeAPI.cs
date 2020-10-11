using System;
using System.Runtime.InteropServices;

namespace KioskModeTestGui {
    class KioskModeAPI {

        const string DllPath = @"KioskMode.dll";
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        private static extern bool StartKioskMode(VKey[] EnabledChars, int sizeOfEnabledChars, VKey[] EscapeSequence, int sizeOfEscapeSequence, IntPtr WindowHandle);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool EndKioskMode(IntPtr WindowHandle);

        private static OnKioskExitEvent EventHandler;//This Variable is required, otherwise the GC would delete the Callback before it gets fired.
        public delegate void OnKioskExitEvent();//Declare the Callback Method of the EventHandler
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        private static extern void SetOnKioskExitEventHandler(OnKioskExitEvent e);

        public static void AddOnKioskExitEventHandler(OnKioskExitEvent e)
        {
            EventHandler = e;
            SetOnKioskExitEventHandler(EventHandler);
        }

        public static bool StartKioskMode(VKey[] EnabledChars, VKey[] EscapeSequence, IntPtr WindowHandle)
        {
            return StartKioskMode(EnabledChars, EnabledChars.Length, EscapeSequence, EscapeSequence.Length, WindowHandle);
        }

    }
    enum VKey: byte {//https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
        A = 65, //The letter "A" key.
        Accept = 30, //The accept button or key.
        Add = 107, //The add (+) operation key as located on a numeric pad.
        Application = 93, //The application key or button.
        B = 66, //The letter "B" key.
        Back = 8, //The virtual back key or button.
        C = 67, //The letter "C" key.
        Cancel = 3, //The cancel key or button
        CapitalLock = 20, //The Caps Lock key or button.
        Clear = 12, //The Clear key or button.
        Control = 17, //The Ctrl key. This is the general Ctrl case, applicable to key layouts with only one Ctrl key or that do not need to differentiate between left Ctrl and right Ctrl keystrokes.
        Convert = 28, //The convert button or key.
        D = 68, //The letter "D" key.
        Decimal = 110, //The decimal (.) key as located on a numeric pad.
        Delete = 46, //The Delete key.
        Divide = 111, //The divide (/) operation key as located on a numeric pad.
        Down = 40, //The Down Arrow key.
        E = 69, //The letter "E" key.
        End = 35, //The End key.
        Enter = 13, //The Enter key.
        Escape = 27, //The Esc key.
        Execute = 43, //The execute key or button.
        F = 70, //The letter "F" key.
        F1 = 112, //The F1 function key.
        F10 = 121, //The F10 function key.
        F11 = 122, //The F11 function key.
        F12 = 123, //The F12 function key.
        F13 = 124, //The F13 function key.
        F14 = 125, //The F14 function key.
        F15 = 126, //The F15 function key.
        F16 = 127, //The F16 function key.
        F17 = 128, //The F17 function key.
        F18 = 129, //The F18 function key.
        F19 = 130, //The F19 function key.
        F2 = 113, //The F2 function key.
        F20 = 131, //The F20 function key.
        F21 = 132, //The F21 function key.
        F22 = 133, //The F22 function key.
        F23 = 134, //The F23 function key.
        F24 = 135, //The F24 function key.
        F3 = 114, //The F3 function key.
        F4 = 115, //The F4 function key.
        F5 = 116, //The F5 function key.
        F6 = 117, //The F6 function key.
        F7 = 118, //The F7 function key.
        F8 = 119, //The F8 function key.
        F9 = 120, //The F9 function key.
        Favorites = 171, //The favorites key.
        Final = 24, //The Final symbol key-shift button.
        G = 71, //The letter "G" key.
        GamepadA = 195, //The gamepad A button.
        GamepadB = 196, //The gamepad B button.
        GamepadDPadDown = 204, //The gamepad d-pad down.
        GamepadDPadLeft = 205, //The gamepad d-pad left.
        GamepadDPadRight = 206, //The gamepad d-pad right.
        GamepadDPadUp = 203, //The gamepad d-pad up.
        GamepadLeftShoulder = 200, //The gamepad left shoulder.
        GamepadLeftThumbstickButton = 209, //The gamepad left thumbstick button.
        GamepadLeftThumbstickDown = 212, //The gamepad left thumbstick down.
        GamepadLeftThumbstickLeft = 214, //The gamepad left thumbstick left.
        GamepadLeftThumbstickRight = 213, //The gamepad left thumbstick right.
        GamepadLeftThumbstickUp = 211, //The gamepad left thumbstick up.
        GamepadLeftTrigger = 201, //The gamepad left trigger.
        GamepadMenu = 207, //The gamepad menu button.
        GamepadRightShoulder = 199, //The gamepad right shoulder.
        GamepadRightThumbstickButton = 210, //The gamepad right thumbstick button.
        GamepadRightThumbstickDown = 216, //The gamepad right thumbstick down.
        GamepadRightThumbstickLeft = 218, //The gamepad right thumbstick left.
        GamepadRightThumbstickRight = 217, //The gamepad right thumbstick right.
        GamepadRightThumbstickUp = 215, //The gamepad right thumbstick up.
        GamepadRightTrigger = 202, //The gamepad right trigger.
        GamepadView = 208, //The gamepad view button.
        GamepadX = 197, //The gamepad X button.
        GamepadY = 198, //The gamepad Y button.
        GoBack = 166, //The go back key.
        GoForward = 167, //The go forward key.
        GoHome = 172, //The go home key.
        H = 72, //The letter "H" key.
        Hangul = 21, //The Hangul symbol key-shift button.
        Hanja = 25, //The Hanja symbol key shift button.
        Help = 47, //The Help key or button.
        Home = 36, //The Home key.
        I = 73, //The letter "I" key.
        Insert = 45, //The Insert key.
        J = 74, //The letter "J" key.
        Junja = 23, //The Junja symbol key-shift button.
        K = 75, //The letter "K" key.
        Kana = 21, //The Kana symbol key-shift button
        Kanji = 25, //The Kanji symbol key-shift button.
        L = 76, //The letter "L" key.
        Left = 37, //The Left Arrow key.
        LeftButton = 1, //The left mouse button.
        LeftControl = 162, //The left Ctrl key.
        LeftMenu = 164, //The left menu key.
        LeftShift = 160, //The left Shift key.
        LeftWindows = 91, //The left Windows key.
        M = 77, //The letter "M" key.
        Menu = 18, //The menu key or button.
        MiddleButton = 4, //The middle mouse button.
        ModeChange = 31, //The mode change key.
        Multiply = 106, //The multiply (*) operation key as located on a numeric pad.
        N = 78, //The letter "N" key.
        NavigationAccept = 142, //The navigation accept button.
        NavigationCancel = 143, //The navigation cancel button.
        NavigationDown = 139, //The navigation down button.
        NavigationLeft = 140, //The navigation left button.
        NavigationMenu = 137, //The navigation menu button.
        NavigationRight = 141, //The navigation right button.
        NavigationUp = 138, //The navigation up button.
        NavigationView = 136, //The navigation up button.
        NonConvert = 29, //The nonconvert button or key.
        None = 0, //No virtual key value.
        Number0 = 48, //The number "0" key.
        Number1 = 49, //The number "1" key.
        Number2 = 50, //The number "2" key.
        Number3 = 51, //The number "3" key.
        Number4 = 52, //The number "4" key.
        Number5 = 53, //The number "5" key.
        Number6 = 54, //The number "6" key.
        Number7 = 55, //The number "7" key.
        Number8 = 56, //The number "8" key.
        Number9 = 57, //The number "9" key.
        NumberKeyLock = 144, //The Num Lock key.
        NumberPad0 = 96, //The number "0" key as located on a numeric pad.
        NumberPad1 = 97, //The number "1" key as located on a numeric pad.
        NumberPad2 = 98, //The number "2" key as located on a numeric pad.
        NumberPad3 = 99, //The number "3" key as located on a numeric pad.
        NumberPad4 = 100, //The number "4" key as located on a numeric pad.
        NumberPad5 = 101, //The number "5" key as located on a numeric pad.
        NumberPad6 = 102, //The number "6" key as located on a numeric pad.
        NumberPad7 = 103, //The number "7" key as located on a numeric pad.
        NumberPad8 = 104, //The number "8" key as located on a numeric pad.
        NumberPad9 = 105, //The number "9" key as located on a numeric pad.
        O = 79, //The letter "O" key.
        P = 80, //The letter "P" key.
        PageDown = 34, //The Page Down key.
        PageUp = 33, //The Page Up key.
        Pause = 19, //The Pause key or button.
        Print = 42, //The Print key or button.
        Q = 81, //The letter "Q" key.
        R = 82, //The letter "R" key.
        Refresh = 168, //The refresh key.
        Right = 39, //The Right Arrow key.
        RightButton = 2, //The right mouse button.
        RightControl = 163, //The right Ctrl key.
        RightMenu = 165, //The right menu key.
        RightShift = 161, //The right Shift key.
        RightWindows = 92, //The right Windows key.
        S = 83, //The letter "S" key.
        Scroll = 145, //The Scroll Lock (ScrLk) key.
        Search = 170, //The search key.
        Select = 41, //The Select key or button.
        Separator = 108, //The separator key as located on a numeric pad.
        Shift = 16, //The Shift key. This is the general Shift case, applicable to key layouts with only one Shift key or that do not need to differentiate between left Shift and right Shift keystrokes.
        Sleep = 95, //The sleep key or button.
        Snapshot = 44, //The snapshot key or button.
        Space = 32, //The Spacebar key or button.
        Stop = 169, //The stop key.
        Subtract = 109, //The subtract (-) operation key as located on a numeric pad.
        T = 84, //The letter "T" key.
        Tab = 9, //The Tab key.
        U = 85, //The letter "U" key.
        Up = 38, //The Up Arrow key.
        V = 86, //The letter "V" key.
        W = 87, //The letter "W" key.
        X = 88, //The letter "X" key.
        XButton1 = 5, //An additional "extended" device key or button (for example, an additional mouse button).
        XButton2 = 6, //An additional "extended" device key or button (for example, an additional mouse button).
        Y = 89, //The letter "Y" key.
        Z = 90 //The letter "Z" key.
    }
}
