namespace Winamp.SDK
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Winamp.SDK.Utilities;

    #endregion //Using Directives

    public class WmExtraController : WmControllerBase
    {
        #region Playlist Methods

        public static string GetCurrentSongTitle()
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);

            if (hwnd.Equals(IntPtr.Zero))
                return "N/A";

            string lpText = new string((char)0, 100);
            int intLength = Windows32.GetWindowText(hwnd, lpText, lpText.Length);

            if ((intLength <= 0) || (intLength > lpText.Length))
                return "N/A";

            string strTitle = lpText.Substring(0, intLength);
            int intName = strTitle.IndexOf(STRING_TITLE_END);
            int intLeft = strTitle.IndexOf("[");
            int intRight = strTitle.IndexOf("]");

            if ((intName >= 0) && (intLeft >= 0) && (intName < intLeft) && (intRight >= 0) && (intLeft + 1 < intRight))
                return strTitle.Substring(intLeft + 1, intRight - intLeft - 1);

            if ((strTitle.EndsWith(STRING_TITLE_END)) && (strTitle.Length > STRING_TITLE_END.Length))
                strTitle = strTitle.Substring(0, strTitle.Length - STRING_TITLE_END.Length);

            int intDot = strTitle.IndexOf(".");
            if ((intDot > 0) && IsNumeric(strTitle.Substring(0, intDot)))
                strTitle = strTitle.Remove(0, intDot + 1);

            return strTitle.Trim();
        }

        public static void AppendToPlayList(string fileName)
        {
            Windows32.COPYDATASTRUCT cds;
            //cds.dwData = (IntPtr)100;
            cds.dwData = (IntPtr)WA_IPC.IPC_ENQUEUEFILE;
            cds.lpData = fileName;
            cds.cbData = fileName.Length + 1;

            //Win32.SendMessageA(this.Handle, Win32.WM_COPYDATA, 0, ref cds);
            SendToWinamp(ref cds);
        }

        public static void AppendToPlayList(string[] fileNames)
        {
            foreach (string sFile in fileNames)
            {
                AppendToPlayList(sFile);
            }
        }

        public static void AppendToPlayList(FileInfo fileName)
        {
            AppendToPlayList(fileName.FullName);
        }

        public static void AppendToPlayList(FileInfo[] fileNames)
        {
            foreach (FileInfo fileName in fileNames)
            {
                AppendToPlayList(fileName);
            }
        }

        public static bool ShuffleStatus
        {
            get
            {
                int length = SendToWinamp(0, WM_USER_MSGS.WA_GET_SHUFFLE);
                //Win32.SendMessage(this.Handle, Win32.WM_USER, 0, (uint)WM_USER_MSGS.WA_GET_SHUFFLE);
                bool status = ToBool(length);
                return status;
            }
            set
            {
                int status = ToInt32(value);
                SendToWinamp(status, WM_USER_MSGS.WA_SET_SHUFFLE);
                //Win32.SendMessage(this.Handle, Win32.WM_USER, status, (uint)WM_USER_MSGS.WA_SET_SHUFFLE);
            }
        }

        public static bool ToggleShuffle()
        {
            ShuffleStatus = !ShuffleStatus;
            return ShuffleStatus;
            //it is possible to use a WM_COMMAND to do this, but we cannot return the new value then.
        }

        public static bool RepeatStatus
        {
            get
            {
                int length = SendToWinamp(0, WM_USER_MSGS.WA_GET_REPEAT);
                //Win32.SendMessage(this.Handle, Win32.WM_USER, 0, (uint)WM_USER_MSGS.WA_GET_REPEAT);
                return ToBool(length);
            }
            set
            {
                int status = ToInt32(value);
                SendToWinamp(status, WM_USER_MSGS.WA_SET_REPEAT);
            }
        }

        public static bool ToggleRepeat()
        {
            RepeatStatus = !RepeatStatus;
            return RepeatStatus;
            //it is possible to use a WM_COMMAND to do this, but we cannot return the new value then.
        }

        public static int Volume
        {
            //volume is 0 - 255
            get
            {
                return 0;//SendToWinamp(0, WM_USER_MSGS.wa_get;
            }
            set
            {
                int vol = value;
                if (vol <= 0)
                {
                    vol = 0;
                }
                else if (vol >= 100)
                {
                    vol = 100;
                }
                int nWaVol = (int)(vol / 100d * 255);
                SendToWinamp(nWaVol, WM_USER_MSGS.WA_SET_VOL);
            }
        }

        #endregion //Playlist Methods
    }
}
