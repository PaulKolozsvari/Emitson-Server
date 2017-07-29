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

    public class WmControllerBase
    {
        #region Fields

        public static int EqualizerPosition = 0;

        #endregion //Fields

        #region Winamp-specific Constants

        // We have to define the Winamp class name
        public const string WINAMP_WINDOW_NAME = "Winamp v1.x";

        // Useful for GetSongTitle() Method
        public const string STRING_TITLE_END = " - Winamp";

        #endregion //Winamp-specific Constants

        #region Command Type Constants

        public const int WM_COMMAND_MESSAGE = 0x111; // To tell Winamp that we are sending it a WM_COMMAND it needs the hex code 0x111
        public const int WM_WA_IPC_MESSAGE = 0x0400; // To tell Winamp that we are sending it a WM_USER (WM_WA_IPC) it needs the hex code 0x0400
        public const int WM_COPYDATA_MESSAGE = 0x4a;

        #endregion //Command Type Constants

        #region Methods

        #region Utiltiy Methods

        protected static bool IsNumeric(string Value)
        {
            try
            {
                double.Parse(Value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected static int ToInt32(bool b)
        {
            return (b) == true ? 1 : 0; //Shorthand for if statement
        }

        protected static bool ToBool(int i)
        {
            return (i) == 1 ? true : false;
        }

        public static int SendToWinamp(int nData, WM_USER_MSGS msg)
        {
            return SendToWinamp(WA_MsgTypes.WM_USER, nData, (int)msg);
        }

        public static int SendToWinamp(int nData, WM_COMMAND_MSGS msg)
        {
            return SendToWinamp(WA_MsgTypes.WM_COMMAND, (int)msg, 0);
        }

        public static int SendToWinamp(ref Windows32.COPYDATASTRUCT data) //not sure about this one, i think the only message you can send for copydata is 0
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            return Windows32.SendMessageA(hwnd, WM_COPYDATA_MESSAGE, 0, ref data);
        }

        public static int SendToWinamp(WA_MsgTypes msgType, int nData, int nMsg)
        {
            IntPtr hwnd = Windows32.FindWindow(WINAMP_WINDOW_NAME, null);
            return Windows32.SendMessageA(hwnd, (int)msgType, nData, (uint)nMsg);
        }

        #endregion //Utiltiy Methods

        #endregion //Methods
    }
}