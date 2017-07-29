namespace Winamp.SDK
{
    #region Using Directives

    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Winamp.SDK.Utilities;

    #endregion //Using Directives

    public class WinampProcessManager
    {
        #region Singleton Setup

        private static WinampProcessManager _instance;

        public static WinampProcessManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WinampProcessManager();
                }
                return _instance;
            }
        }

        #endregion //Singleton Setup

        #region Constructors

        private WinampProcessManager()
        {
            StartWinAmp();
        }

        public WinampProcessManager(bool autoStart)
        {
            if (autoStart)
            {
                try
                {
                    StartWinAmp();
                }
                catch
                {
                    // 'HCR\Applications\Winamp.exe\shell\command' - Path to WinAmp Command
                    _winampFilePath = ((string)Microsoft.Win32.Registry.GetValue("HKEY_CLASSES_ROOT\\Applications\\winamp.exe\\shell\\open\\command", null, null)).Split(new char[] { '"' })[1];
                    if (!string.IsNullOrEmpty(_winampFilePath))
                    {
                        _process = Process.Start(_winampFilePath);
                    }
                    else
                    {
                        throw new Exception("No installation of winamp detected.");
                    }
                    StartWinAmp();
                    //MessageBox.Show(WinAmpProcess.Handle.ToString());
                    //while (hWnd.ToInt32() == 0)
                    //    hWnd = Win32.FindWindow("Winamp v1.x", null);
                    //MessageBox.Show(hWnd.ToString());
                }
            }
        }

        #endregion //Constructors

        #region Fields

        private Process _process;
        private IntPtr _handle;
        private string _winampFilePath;
        public bool _exited;

        #endregion //Fields

        #region Methods

        public void StartWinAmp()
        {
            //IntPtr hWnd = Win32.FindWindow("BaseWindow_RootWnd", null);
            IntPtr hWnd = Windows32.FindWindow(WmControllerBase.WINAMP_WINDOW_NAME, null);
            if (hWnd.ToInt32() == 0)
            {
                throw new Exception("Winamp instance not found.");
            }
            else
            {
                _process = Process.GetProcessesByName("Winamp")[0];
            }
            _process.EnableRaisingEvents = true;
            _process.Exited += new EventHandler(WinAmpProcess_Exited);

            _handle = hWnd;
            _exited = false;
            _winampFilePath = ((string)Registry.GetValue("HKEY_CLASSES_ROOT\\Applications\\winamp.exe\\shell\\open\\command", null, null)).Split(new char[] { '"' })[1];
            //Win32.SendMessage(Handle, Win32.WM_COMMAND, (int)WA_Commands.WINAMP_BUTTON2, WA_NOTHING);
            //I commented out this line because it was causing Winamp
            //to restart the song that it's on when a new winamp object was created.

            //MessageBox.Show(WinAmpProcess.MainWindowTitle);
        }

        void WinAmpProcess_Exited(object sender, EventArgs e)
        {
            _handle = (IntPtr)0;
            _process = null;
            _exited = true;
        }

        #endregion //Methods
    }
}