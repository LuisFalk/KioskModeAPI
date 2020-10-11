# KioskModeAPI
The Kiosk Mode API is used to force applications into full-screen and always-on-top mode. 
A Global Lowlevel Keyboard Hook is also provided that allows you to block keystrokes such as [ALT]+[F4]. Known (Tested out) Protected (ant therefore not blockable) Shortcuts are [WIN]+[L], [CTRL]+[ALT]+[ENTF] and [WIN]+[G].
This Repo may be useful if you want to create your own test program to ensure that candidates do not simply jump out of the test. 
It also blocks the screen saver by moving the mouse 0 pixels every 20 seconds. 
(Windows will recognize this as input and therefore the screen saver won't get emitted.)

The Config- file has the following setting options

```
[AppSettings]
UnlockPassword     = <"Emergency password" to exit the kiosk mode>
WindowName         = <Part of the name of the window to be set to Kiosk mode (substring since the name can be generic)>
ProcessName        = <Name of the .exe- File, which should get forced to Kiosk mode>
AllowedChars       = <Allowed keys as VK_Code (https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes) (All others are blocked by default)>
[WindowSettings]
PreventScreenSaver = <Bypass screen saver by simulated moving of the mouse>
SetAlwaysOnTop     = <Sets the respective window permanently to the foreground>
ResizeToFullScreen = <Makes the respective window into a full screen window and hides the title bar>
```
