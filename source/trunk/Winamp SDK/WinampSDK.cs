using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text;
using Microsoft.Win32;
using System.IO;

namespace WinAmpSDK
{
    //For convenience i moved the Win32 class outside of this file.

    /// <summary>
    /// Class for controlling Winamp
    /// </summary>
    //TO DO : still need to figure out how this is going to work (instance NOT static is better)
    public class Winamp
    {
        public IntPtr Handle;
        public string WA_Path;
        public Process WinAmpProcess;
        public bool Exited;

        public static int SendCommandToWinamp(IntPtr hWnd, WM_COMMAND_MSGS Command, uint lParam)
        {
            return Win32.SendMessage(hWnd, Win32.WM_COMMAND, (int)Command, lParam);
        }

        private static int ToInt32(bool b)
        {
            return (b) == true ? 1 : 0; //Shorthand for if statement
        }

        private static bool ToBool(int i)
        {
            return (i) == 1 ? true : false;
        }

        /// <summary>
        /// Creates a Winamp object and binds it to a running instance of winamp. 
        /// Throws a "Winamp instance not found" Exception if no winamp is running.
        /// </summary>
        public Winamp()
        {
            StartWinAmp();
        }

        private void StartWinAmp()
        {
            //IntPtr hWnd = Win32.FindWindow("BaseWindow_RootWnd", null);
            IntPtr hWnd = Win32.FindWindow("Winamp v1.x", null);

            if (hWnd.ToInt32() == 0)
            {
                throw new Exception("Winamp instance not found.");
            }
            else
                WinAmpProcess = Process.GetProcessesByName("Winamp")[0];

            WinAmpProcess.EnableRaisingEvents = true;
            WinAmpProcess.Exited += new EventHandler(WinAmpProcess_Exited);

            Handle = hWnd;
            Exited = false;
            WA_Path = ((string)Registry.GetValue("HKEY_CLASSES_ROOT\\Applications\\winamp.exe\\shell\\open\\command", null, null)).Split(new char[] { '"' })[1];
            //Win32.SendMessage(Handle, Win32.WM_COMMAND, (int)WA_Commands.WINAMP_BUTTON2, WA_NOTHING);
            //I commented out this line because it was causing Winamp
            //to restart the song that it's on when a new winamp object was created.

            //MessageBox.Show(WinAmpProcess.MainWindowTitle);
        }

        /// <summary>
        /// Creates a Winamp object and binds it to a running instance of winamp.
        /// </summary>
        /// <param name="bAutoStart">If true, starts an instance of winamp if none is found.</param>
        public Winamp(bool bAutoStart)
        {
            if (bAutoStart)
            {
                try
                {
                    StartWinAmp();
                }
                catch
                {
                    // 'HCR\Applications\Winamp.exe\shell\command' - Path to WinAmp Command
                    WA_Path = ((string)Microsoft.Win32.Registry.GetValue("HKEY_CLASSES_ROOT\\Applications\\winamp.exe\\shell\\open\\command", null, null)).Split(new char[] { '"' })[1];
                    if (WA_Path != null)
                    {
                        WinAmpProcess = Process.Start(WA_Path);
                    }
                    else
                    {
                        throw new Exception("No installation of winamp detected.");
                    }
                    new Winamp(bAutoStart);
                    //MessageBox.Show(WinAmpProcess.Handle.ToString());
                    //while (hWnd.ToInt32() == 0)
                    //    hWnd = Win32.FindWindow("Winamp v1.x", null);
                    //MessageBox.Show(hWnd.ToString());
                }
            }
        }

        void WinAmpProcess_Exited(object sender, EventArgs e)
        {
            Handle = (IntPtr)0;
            WinAmpProcess = null;
            Exited = true;
        }

        public int SendToWinamp(int nData, WM_USER_MSGS msg)
        {
            return SendToWinamp(WA_MsgTypes.WM_USER, nData, (int)msg);
        }

        public int SendToWinamp(int nData, WM_COMMAND_MSGS msg)
        {
            return SendToWinamp(WA_MsgTypes.WM_COMMAND, (int)msg, 0);
        }

        public int SendToWinamp(ref Win32.COPYDATASTRUCT data) //not sure about this one, i think the only message you can send for copydata is 0
        {
            return Win32.SendMessageA(this.Handle, Win32.WM_COPYDATA, 0, ref data);
        }

        public int SendToWinamp(WA_MsgTypes msgType, int nData, int nMsg)
        {
            return Win32.SendMessage(this.Handle, (int)msgType, nData, (uint)nMsg);
        }

        #region Winamp Commands

        public void AppendToPlayList(string fileName)
        {

            Win32.COPYDATASTRUCT cds;
            //cds.dwData = (IntPtr)100;
            cds.dwData = (IntPtr)WA_IPC.IPC_ENQUEUEFILE;
            cds.lpData = fileName;
            cds.cbData = fileName.Length + 1;

            //Win32.SendMessageA(this.Handle, Win32.WM_COPYDATA, 0, ref cds);
            SendToWinamp(ref cds);
        }

        public void AppendToPlayList(string[] fileNames)
        {
            foreach (string sFile in fileNames)
            {
                AppendToPlayList(sFile);
            }
        }

        public void AppendToPlayList(FileInfo fileName)
        {
            AppendToPlayList(fileName.FullName);
        }

        public void AppendToPlayList(FileInfo[] fileNames)
        {
            foreach (FileInfo fileName in fileNames)
            {
                AppendToPlayList(fileName);
            }
        }

        public bool ShuffleStatus
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

        public bool ToggleShuffle()
        {
            this.ShuffleStatus = !this.ShuffleStatus;
            return this.ShuffleStatus;
            //it is possible to use a WM_COMMAND to do this, but we cannot return the new value then.
        }

        public bool RepeatStatus
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

        public bool ToggleRepeat()
        {
            this.RepeatStatus = !this.RepeatStatus;
            return this.RepeatStatus;
            //it is possible to use a WM_COMMAND to do this, but we cannot return the new value then.
        }

        public int Volume
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

        #endregion
    }
}
