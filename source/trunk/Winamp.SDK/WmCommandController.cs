namespace Winamp.SDK
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Winamp.SDK.Utilities;

    #endregion //Using Directives

    public class WmCommandController : WmControllerBase
    {
        #region WM_COMMAND Type Methods

        public static void Stop()
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            Windows32.SendMessageA(hwnd, WM_COMMAND_MESSAGE, WM_COMMAND.WA_STOP, WM_COMMAND.WA_NOTHING);
        }

        public static void Play()
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            Windows32.SendMessageA(hwnd, WM_COMMAND_MESSAGE, WM_COMMAND.WA_PLAY, WM_COMMAND.WA_NOTHING);
        }

        public static void Pause()
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            Windows32.SendMessageA(hwnd, WM_COMMAND_MESSAGE, WM_COMMAND.WA_PAUSE, WM_COMMAND.WA_NOTHING);
        }

        public static void PreviousTrack()
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            Windows32.SendMessageA(hwnd, WM_COMMAND_MESSAGE, WM_COMMAND.WA_PREVTRACK, WM_COMMAND.WA_NOTHING);
        }

        public static void NextTrack()
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            Windows32.SendMessageA(hwnd, WM_COMMAND_MESSAGE, WM_COMMAND.WA_NEXTTRACK, WM_COMMAND.WA_NOTHING);
        }

        public static void VolumeUp()
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            Windows32.SendMessageA(hwnd, WM_COMMAND_MESSAGE, WM_COMMAND.WA_VOLUMEUP, WM_COMMAND.WA_NOTHING);
        }

        public static void VolumeDown()
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            Windows32.SendMessageA(hwnd, WM_COMMAND_MESSAGE, WM_COMMAND.WA_VOLUMEDOWN, WM_COMMAND.WA_NOTHING);
        }

        public static void Forward5Sec()
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            Windows32.SendMessageA(hwnd, WM_COMMAND_MESSAGE, WM_COMMAND.WINAMP_FFWD5S, WM_COMMAND.WA_NOTHING);
        }

        public static void Rewind5Sec()
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            Windows32.SendMessageA(hwnd, WM_COMMAND_MESSAGE, WM_COMMAND.WINAMP_REW5S, WM_COMMAND.WA_NOTHING);
        }
        #endregion //WM_COMMAND Type Methods
    }
}
