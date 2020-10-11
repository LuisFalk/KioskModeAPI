# KioskModeAPI
The Kiosk Mode API is used to force applications into full-screen and always-on-top mode. 
A Global Lowlevel Keyboard Hook is also provided that allows you to block keystrokes such as [ALT]+[F4]. Known (Tested out) Protected (ant therefore not blockable) Shortcuts are [WIN]+[L], [CTRL]+[ALT]+[ENTF] and [WIN]+[G].
This Repo may be useful if you want to create your own test program to ensure that candidates do not simply jump out of the test. 
It also blocks the screen saver by moving the mouse 0 pixels every 20 seconds. 
(Windows will recognize this as input and therefore the screen saver won't get emitted.)
