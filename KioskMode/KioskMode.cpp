#include "stdafx.h"
#include <thread>
#include <atomic>
#include <vector>
#include <windows.h>
#include <algorithm>

#define EXPORT_FUNCTION extern "C" __declspec(dllexport)

typedef unsigned char UNUM8;	// Unsigned numeric 8 bits
typedef signed	 char SNUM8;	// Signed   numeric 8 bits

typedef void(__stdcall* OnKioskExitEvent)();
EXPORT_FUNCTION void SetOnKioskExitEventHandler(OnKioskExitEvent e);
EXPORT_FUNCTION BOOL StartKioskMode(UNUM8 *EnabledChars, int sizeOfEnabledChars, UNUM8 *EscapeSequence, int sizeOfEscapeSequence, HWND WindowHandle);
EXPORT_FUNCTION BOOL EndKioskMode(HWND WindowHandle);

#pragma warning(suppress : 5208)
typedef struct {
	STICKYKEYS StickyKeys = { sizeof(STICKYKEYS), 0 };
	TOGGLEKEYS ToggleKeys = { sizeof(TOGGLEKEYS), 0 };
	FILTERKEYS FilterKeys = { sizeof(FILTERKEYS), 0 };
} AccessKeySettings;

typedef struct {
	int x;
	int y;
	int w;
	int h;
	long gwl_exstyle;
	long gwl_hinstance;
	long gwl_hwndparent;
	long gwl_id;
	long gwl_style;
	long gwl_userdata;
	long gwl_wndproc;
	long dwl_dlgproc;
	long dwl_msgresult;
	long dwl_user;
} WindowSettings;

HHOOK gKeyboardHook;
std::atomic_bool antiscreensaver_active = false;
std::vector<UNUM8> EnabledKeys;
std::vector<UNUM8> PressedKeys;
std::vector<UNUM8> EscapeSequenceKeys;
AccessKeySettings AccessKeysOriginalSettings;
WindowSettings OriginalWindowSettings;
HWND OrigWindowHandle;
OnKioskExitEvent OnKioskExitEventHandler = nullptr;

template <typename T>
bool IsSubset(std::vector<T> a, std::vector<T> b) {
	if (b.size() - 1 <= 0) return false;
	int i = 0;
	std::reverse(a.begin(), a.end());
	std::reverse(b.begin(), b.end());
	for (T valOfA : a) {
		if (i == b.size() - 1) return true;
		if (valOfA == b[i]) {
			i++;
		}
		else {
			i = 0;
		}
	}
	return false;
}

LRESULT __stdcall onKeyboardKeyPress(int nCode, WPARAM wParam, LPARAM lParam)
{
	if (nCode >= 0)
	{
		KBDLLHOOKSTRUCT kbdStruct = *((KBDLLHOOKSTRUCT*)lParam);
		UNUM8 code = kbdStruct.vkCode;
		if (wParam == WM_KEYDOWN) {
			if (PressedKeys.size() == EscapeSequenceKeys.size()) PressedKeys.erase(PressedKeys.begin());
			PressedKeys.push_back(code);
		}
		if (IsSubset(PressedKeys, EscapeSequenceKeys)) {
			EndKioskMode(nullptr);
		}
		if (!(find(EnabledKeys.begin(), EnabledKeys.end(), code) != EnabledKeys.end())) {
			return -1;
		}
	}
	return CallNextHookEx(gKeyboardHook, nCode, wParam, lParam);
}

BOOL SetAccessKeysPreferences() {
	BOOL ret = true;
	AccessKeySettings originalSettings = AccessKeySettings();
	STICKYKEYS skOff = originalSettings.StickyKeys;
	if ((skOff.dwFlags & SKF_STICKYKEYSON) == 0)
	{
		// Disable the hotkey and the confirmation
		skOff.dwFlags &= ~SKF_HOTKEYACTIVE;
		skOff.dwFlags &= ~SKF_CONFIRMHOTKEY;

		ret &= SystemParametersInfo(SPI_SETSTICKYKEYS, sizeof(STICKYKEYS), &skOff, 0);
	}

	TOGGLEKEYS tkOff = originalSettings.ToggleKeys;
	if ((tkOff.dwFlags & TKF_TOGGLEKEYSON) == 0)
	{
		// Disable the hotkey and the confirmation
		tkOff.dwFlags &= ~TKF_HOTKEYACTIVE;
		tkOff.dwFlags &= ~TKF_CONFIRMHOTKEY;

		ret &= SystemParametersInfo(SPI_SETTOGGLEKEYS, sizeof(TOGGLEKEYS), &tkOff, 0);
	}

	FILTERKEYS fkOff = originalSettings.FilterKeys;
	if ((fkOff.dwFlags & FKF_FILTERKEYSON) == 0)
	{
		// Disable the hotkey and the confirmation
		fkOff.dwFlags &= ~FKF_HOTKEYACTIVE;
		fkOff.dwFlags &= ~FKF_CONFIRMHOTKEY;

		ret &= SystemParametersInfo(SPI_SETFILTERKEYS, sizeof(FILTERKEYS), &fkOff, 0);
	}
	AccessKeysOriginalSettings = originalSettings;
	return ret;
}

BOOL ResetActionKeys() {
	BOOL ret = true;
	AccessKeySettings* settings = &AccessKeysOriginalSettings;

	STICKYKEYS sk = settings->StickyKeys;
	TOGGLEKEYS tk = settings->ToggleKeys;
	FILTERKEYS fk = settings->FilterKeys;

	ret &= SystemParametersInfo(SPI_SETSTICKYKEYS, sizeof(STICKYKEYS), &settings->StickyKeys, 0);
	ret &= SystemParametersInfo(SPI_SETTOGGLEKEYS, sizeof(TOGGLEKEYS), &settings->ToggleKeys, 0);
	ret &= SystemParametersInfo(SPI_SETFILTERKEYS, sizeof(FILTERKEYS), &settings->FilterKeys, 0);
	return ret;
}

BOOL StartMouseJiggler() {
	antiscreensaver_active = true;
	std::thread([] {
		while (antiscreensaver_active) {
			std::this_thread::sleep_for(std::chrono::milliseconds(1000 * 20));
			INPUT input;
			input.type = INPUT_MOUSE;
			input.mi.mouseData = 0;
			input.mi.time = 0;
			input.mi.dx = 0;
			input.mi.dy = 0;
			input.mi.dwFlags = MOUSEEVENTF_MOVE;
			SendInput(1, &input, sizeof(input));
		}
	}).detach();
	return antiscreensaver_active;
}

BOOL EndMouseJiggler() {
	antiscreensaver_active = false;
	return antiscreensaver_active;
}

WindowSettings GetCurrentWindowSettings(HWND window) {
	WindowSettings settings;
	RECT rect;
	if (GetWindowRect(window, &rect))
	{
		settings.w = rect.right - rect.left;
		settings.h = rect.bottom - rect.top;
		settings.x = rect.left;
		settings.y = rect.top;
	}

	settings.gwl_exstyle = GetWindowLong(window, GWL_EXSTYLE);
	settings.gwl_hinstance = GetWindowLong(window, GWL_HINSTANCE);
	settings.gwl_hwndparent = GetWindowLong(window, GWL_HWNDPARENT);
	settings.gwl_id = GetWindowLong(window, GWL_ID);
	settings.gwl_style = GetWindowLong(window, GWL_STYLE);
	settings.gwl_userdata = GetWindowLong(window, GWL_USERDATA);
	settings.gwl_wndproc = GetWindowLong(window, GWL_WNDPROC);

	//Optional Values: Only if its a Dialog Box
	settings.dwl_dlgproc = GetWindowLong(window, DWL_DLGPROC);
	settings.dwl_msgresult = GetWindowLong(window, DWL_MSGRESULT);
	settings.dwl_user = GetWindowLong(window, DWL_USER);
	return settings;
}

BOOL SetWindowPreferences(HWND window) {
	BOOL ret = true;
	OriginalWindowSettings = GetCurrentWindowSettings(window);
	HMONITOR hmon = MonitorFromWindow(window, MONITOR_DEFAULTTONEAREST);
	MONITORINFO mi = { sizeof(mi) };
	if (!GetMonitorInfo(hmon, &mi)) return false;
	ret &= SetWindowPos(window, HWND_TOPMOST, mi.rcMonitor.left, mi.rcMonitor.top, mi.rcMonitor.right - mi.rcMonitor.left, mi.rcMonitor.bottom - mi.rcMonitor.top, SWP_FRAMECHANGED);
	ret &= SetWindowLong(window, GWL_STYLE, GetWindowLong(window, GWL_STYLE) & ~(WS_BORDER | WS_DLGFRAME | WS_THICKFRAME));//Set undecorated style to hide the (title.exe    _ [] X) header of the application
	ret &= SetWindowLong(window, GWL_EXSTYLE, GetWindowLong(window, GWL_EXSTYLE) & ~WS_EX_DLGMODALFRAME);
	return ret;
}

BOOL ResetWindowPreferences(HWND window) {
	BOOL ret = true;
	ret &= SetWindowPos(window, HWND_TOP, OriginalWindowSettings.x, OriginalWindowSettings.y, OriginalWindowSettings.w, OriginalWindowSettings.h, SWP_FRAMECHANGED);
	ret &= SetWindowLong(window, GWL_STYLE, OriginalWindowSettings.gwl_style);
	ret &= SetWindowLong(window, GWL_EXSTYLE, OriginalWindowSettings.gwl_exstyle);
	ret &= SetWindowLong(window, GWL_HINSTANCE, OriginalWindowSettings.gwl_hinstance);
	ret &= SetWindowLong(window, GWL_HWNDPARENT, OriginalWindowSettings.gwl_hwndparent);
	ret &= SetWindowLong(window, GWL_ID, OriginalWindowSettings.gwl_id);
	ret &= SetWindowLong(window, GWL_USERDATA, OriginalWindowSettings.gwl_userdata);
	ret &= SetWindowLong(window, GWL_WNDPROC, OriginalWindowSettings.gwl_wndproc);
	ret &= SetWindowLong(window, DWL_DLGPROC, OriginalWindowSettings.dwl_dlgproc);
	ret &= SetWindowLong(window, DWL_MSGRESULT, OriginalWindowSettings.dwl_msgresult);
	ret &= SetWindowLong(window, DWL_USER, OriginalWindowSettings.dwl_user);
	return ret;
}

void SetOnKioskExitEventHandler(OnKioskExitEvent handler) {
	OnKioskExitEventHandler = handler;
}

BOOL StartKioskMode(UNUM8 *EnabledChars, int sizeOfEnabledChars, UNUM8 *EscapeSequence, int sizeOfEscapeSequence, HWND WindowHandle)
{
	OrigWindowHandle = WindowHandle;
	for (int i = 0; i < sizeOfEnabledChars; i++) EnabledKeys.push_back(EnabledChars[i]);
	for (int i = 0; i < sizeOfEscapeSequence; i++) EscapeSequenceKeys.push_back(EscapeSequence[i]);
	
	PressedKeys.clear();
	BOOL ret = true;
	ret &= StartMouseJiggler();
	ret &= (BOOL)(gKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, onKeyboardKeyPress, NULL, 0));
	ret &= SetAccessKeysPreferences();
	ret &= SetWindowPreferences(WindowHandle);
	return ret;
}

BOOL EndKioskMode(HWND WindowHandle)
{
	BOOL ret = true;
	ret &= !(antiscreensaver_active = false);
	ret &= UnhookWindowsHookEx(gKeyboardHook);
	ret &= ResetActionKeys();
	if (WindowHandle != nullptr) {
		ret &= ResetWindowPreferences(WindowHandle);
	}
	else if (OrigWindowHandle != nullptr)
	{
		ret &= ResetWindowPreferences(OrigWindowHandle);
	}
	if (OnKioskExitEventHandler != nullptr) OnKioskExitEventHandler();
	return ret;
}