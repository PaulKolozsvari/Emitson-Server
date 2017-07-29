namespace Emitson.Service
{
    #region Using Directives

    using Emitson.Service.Configuration;
    using Figlut.Server.Toolkit.Utilities;
    using Figlut.Server.Toolkit.Utilities.Logging;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.ServiceProcess;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Winamp.SDK;

    #endregion //Using Directives

    static class Program
    {
        #region Constants

        private const string HELP_ARGUMENT = "/help";
        private const string HELP_QUESTION_MARK_ARGUMENT = "/?";
        private const string RESET_SETTINGS_ARGUMENT = "/reset_settings";
        //private const string TEST_MODE_ARGUMENT = "/test_mode";
        private const string HIDE = "/hide";
        private const string LOG_MAX = "/logmax";

        #endregion //Constants

        #region Constants

        private static bool _hide;
        private static bool _logMax;

        #endregion //Constants

        #region Methods

        private static bool ParseArguments(string[] args)
        {
            foreach (string a in args)
            {
                string aLower = a.ToLower();
                switch (aLower)
                {
                    case HELP_ARGUMENT:
                        DisplayHelp();
                        return false;
                    case HELP_QUESTION_MARK_ARGUMENT:
                        DisplayHelp();
                        return false;
                    case RESET_SETTINGS_ARGUMENT:
                        ResetSettings();
                        return false;
                    case HIDE:
                        _hide = true;
                        break;
                    case LOG_MAX:
                        _logMax = true;
                        break;
                    default:
                        throw new ArgumentException(string.Format("Invalid argument '{0}'.", a));
                }
            }
            return true;
        }

        private static void DisplayHelp()
        {
            Console.WriteLine("*** Emitson Service Usage ***");
            Console.WriteLine();
            Console.WriteLine("{0} or {1} : Display usage (service is not started).", HELP_ARGUMENT, HELP_QUESTION_MARK_ARGUMENT);
            Console.WriteLine("{0} : Resets the service's settings file with the default settings (server is not started).", RESET_SETTINGS_ARGUMENT);
            Console.WriteLine("{0} : Hides the console screen (command prompt). Application can be closed from Task Manager.", HIDE);
            Console.WriteLine("{0} : Overrides the Logging Level to Maximum and hence logs all activity.", LOG_MAX);
            Console.WriteLine();
            Console.WriteLine("N.B. Executing without any parameters runs the server as a windows service.");
        }

        private static void ResetSettings()
        {
            EmitsonServiceSettings s = new EmitsonServiceSettings()
            {
                LogToFile = true,
                LogToWindowsEventLog = true,
                LogToConsole = true,
                LogFileName = "Emitson.Service.Log.txt",
                EventSourceName = "EmitsonService.Source",
                EventLogName = "Emitson.Service.Log",
                LoggingLevel = LoggingLevel.Normal,

                RootUriContent = "Emitson Service",
                HostAddressSuffix = "Emitson",
                PortNumber = 2985,
                UseAuthentication = false,
                IncludeExceptionDetailInResponse = false,
                TextResponseEncoding = TextEncodingType.UTF8,
                IncludeOrmTypeNamesInJsonResponse = false,
                MaxBufferPoolSize = 2147483647,
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647
            };
            Console.Write("Reset settings file {0}, are you sure (Y/N)?", s.FilePath);
            string response = Console.ReadLine();
            if (response.Trim().ToLower() != "y")
            {
                return;
            }
            s.SaveToFile();
            Console.WriteLine("{0} reset successfully.", Path.GetFileName(s.FilePath));
            Console.Read();
        }

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("Kernel32")]
        private static extern IntPtr GetConsoleWindow();

        const int SW_HIDE=0;
        const int SW_SHOW=5;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            try
            {
                /*If Running as Windows Application:
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new EmitsonServiceUI()); */

                /*If running as Windows Service:
                 * ServiceBase.Run(new ServiceBase[] { new EmitsonService() });*/

                if (!ParseArguments(args))
                {
                    return; //User only wanted to display the help or to reset the settings.
                }
                Color c = Color.LightGoldenrodYellow;
                ConsoleColor cc = ConsoleColorConverter.ClosestConsoleColor(c.R, c.G, c.B);
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();
                Console.WriteLine("Starting Emitson Service ... ");
                EmitsonService.Start(_logMax);
                Console.WriteLine("Press any key to stop the service ...");
                if (_hide)
                {
                    IntPtr hwnd;
                    hwnd = GetConsoleWindow();
                    ShowWindow(hwnd, SW_HIDE);
                }
                Console.Read();
                EmitsonService.Stop();
                return;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw ex;
            }
        }

        #endregion //Methods
    }
}
