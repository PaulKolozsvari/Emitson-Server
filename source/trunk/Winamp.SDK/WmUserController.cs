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

    public class WmUserController : WmControllerBase
    {
        #region WM_USER (WM_WA_IPC) Type Methods

        public static int GetPlaybackStatus()
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            return Windows32.SendMessageA(hwnd, WM_WA_IPC_MESSAGE, WM_COMMAND.WA_NOTHING, WM_USER.IPC_ISPLAYING);
        }

        public static int GetWinampVersion()
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            return Windows32.SendMessageA(hwnd, WM_WA_IPC_MESSAGE, WM_COMMAND.WA_NOTHING, WM_USER.IPC_GETVERSION);
        }

        public static void DeleteCurrentPlaylist()
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            Windows32.SendMessageA(hwnd, WM_WA_IPC_MESSAGE, WM_COMMAND.WA_NOTHING, WM_USER.IPC_DELETE);
        }

        public static void SavePlaylist()
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            Windows32.SendMessageA(hwnd, WM_WA_IPC_MESSAGE, WM_COMMAND.WA_NOTHING, WM_USER.IPC_WRITEPLAYLIST);
        }

        public static int GetPlaylistPosition()
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            return Windows32.SendMessageA(hwnd, WM_WA_IPC_MESSAGE, WM_COMMAND.WA_NOTHING, WM_USER.IPC_GETLISTPOS);
        }

        public static void SetPlaylistPosition(int position)
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            Windows32.SendMessageA(hwnd, WM_WA_IPC_MESSAGE, position, WM_USER.IPC_SETPLAYLISTPOS);
        }

        public static int GetTrackPosition()
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            return Windows32.SendMessageA(hwnd, WM_WA_IPC_MESSAGE, WM_COMMAND.WA_NOTHING, WM_USER.IPC_GETOUTPUTTIME);
        }

        public static int GetTrackLength()
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            return Windows32.SendMessageA(hwnd, WM_WA_IPC_MESSAGE, WM_COMMAND.WA_MODE_1, WM_USER.IPC_GETOUTPUTTIME);
        }

        public static int GetTrackCount()
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            return Windows32.SendMessageA(hwnd, WM_WA_IPC_MESSAGE, WM_COMMAND.WA_NOTHING, WM_USER.IPC_GETLISTLENGTH);
        }

        public static void JumpToTrackPosition(int position)
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            Windows32.SendMessageA(hwnd, WM_WA_IPC_MESSAGE, position, WM_USER.IPC_JUMPTOTIME);
        }

        /// <summary>
        /// Scale of 0 (mute) - 255 (max)
        /// </summary>
        /// <param name="position"></param>
        public static void SetVolume(int position)
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            Windows32.SendMessageA(hwnd, WM_WA_IPC_MESSAGE, position, WM_USER.IPC_SETVOLUME);
        }

        public static void SetPanning(int position)
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            Windows32.SendMessageA(hwnd, WM_WA_IPC_MESSAGE, position, WM_USER.IPC_SETPANNING);
        }

        public static void GetTrackInfo(int mode)
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            Windows32.SendMessageA(hwnd, WM_WA_IPC_MESSAGE, mode, WM_USER.IPC_GETINFO);
        }

        public static void GetEqData(int position)
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            EqualizerPosition = Windows32.SendMessageA(hwnd, WM_WA_IPC_MESSAGE, position, WM_USER.IPC_GETEQDATA);
        }

        public static int SetEqData()
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            Windows32.SendMessageA(hwnd, WM_WA_IPC_MESSAGE, EqualizerPosition, WM_USER.IPC_SETEQDATA);
            return EqualizerPosition;
        }

        #endregion //WM_USER (WM_WA_IPC) Type Methods
    }
}
