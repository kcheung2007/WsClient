using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Text;

namespace WsClient
{
    public class Win32Api
    {
        // The WM_COMMAND message is sent when the user selects a command item from a menu, 
		// when a control sends a notification message to its parent window, or when an 
		// accelerator keystroke is translated.
		public const int WM_COMMAND = 0x111;

		// The FindWindow function retrieves a handle to the top-level window whose class name
		// and window name match the specified strings. This function does not search child windows.
		// This function does not perform a case-sensitive search.
		[DllImport("User32.dll")]
		public static extern int FindWindow(string strClassName, string strWindowName);

		// The FindWindowEx function retrieves a handle to a window whose class name 
		// and window name match the specified strings. The function searches child windows, beginning
		// with the one following the specified child window. This function does not perform a case-sensitive search.
		[DllImport("User32.dll")]
		public static extern int FindWindowEx(int hwndParent, int hwndChildAfter, string strClassName, string strWindowName);

        [DllImport("user32")] 
        public static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        [DllImport("user32")]
        public static extern int SetForegroundWindow(int hwnd);

        // ShowWindow(): use the integer number in C#
        // hwnd - The handle of the window to change the show status of. 
        // nCmdShow - Exactly one of the following flags specifying how to show the window: 
        //
        // SW_HIDE              = 0 - Hide the window. 
        // SW_MAXIMIZE          = 3 - Maximize the window. 
        // SW_MINIMIZE          = 6 - Minimize the window. 
        // SW_RESTORE           = 9 - Restore the window (not maximized nor minimized). 
        // SW_SHOW              = 5 - Show the window. 
        // SW_SHOWMAXIMIZED     = 3 - Show the window maximized. 
        // SW_SHOWMINIMIZED     = 2 - Show the window minimized. 
        // SW_SHOWMINNOACTIVE   = 7 - Show the window minimized but do not activate it. 
        // SW_SHOWNA            = 8 - Show the window in its current state but do not activate it. 
        // SW_SHOWNOACTIVATE    = 4 - Show the window in its most recent size and position but do not activate it. 
        // SW_SHOWNORMAL        = 1 - Show the window and activate it (as usual). 
        [DllImport("user32")]
        public static extern int ShowWindow(int hwnd, int nCmdShow);

        // The SendMessage function sends the specified message to a 
		// window or windows. It calls the window procedure for the specified 
		// window and does not return until the window procedure has processed the message. 
		[DllImport("User32.dll")]
		public static extern Int32 SendMessage(
			                    int hWnd,               // handle to destination window
			                    int Msg,                // message
			                    int wParam,             // first message parameter
			                    [MarshalAs(UnmanagedType.LPStr)] string lParam); // second message parameter

		[DllImport("User32.dll")]
		public static extern Int32 SendMessage(
			                    int hWnd,               // handle to destination window
			                    int Msg,                // message
			                    int wParam,             // first message parameter
			                    int lParam);			// second message parameter


        [DllImport("kernel32.dll", SetLastError=true)] 
        public static extern int GetModuleHandleA( string lpModuleName );

        [DllImport("kernel32")]
        public extern static int LoadLibrary(string lpLibFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int FreeLibrary( int hLibModule );

        [DllImport("kernel32", CharSet = CharSet.Ansi)]
        public extern static IntPtr GetProcAddress(int hModule, string lpProcName);

        [DllImport("user32", EntryPoint = "CallWindowProc")]
        public static extern int CallWindowProcA(int lpPrevWndFunc, int hwnd, int MSG, int wParam, int lParam);

		public Win32Api()
		{
			
		}

		~Win32Api()
		{
		}

    }
}
