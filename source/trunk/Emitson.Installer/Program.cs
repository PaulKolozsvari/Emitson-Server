namespace Emitson.Installer
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Text;

    #endregion //Using Directives

    class Program
    {
        #region Constants

        private const string INSTALLER_APPLICATION_NAME = "emitson.installer.exe";

        #endregion //Constants

        #region Methods

        static void Main(string[] args)
        {
            try
            {
                string directory = GetExecutingDirectory();
                string[] fileNames = Directory.GetFiles(GetExecutingDirectory());
                if (fileNames == null || fileNames.Length < 1)
                {
                    throw new Exception("No files found.");
                }
                foreach (string f in fileNames)
                {
                    string fileName = Path.GetFileName(f);
                    string extension = Path.GetExtension(fileName).ToLower();
                    if ((fileName.ToLower() == INSTALLER_APPLICATION_NAME) ||
                        (extension != ".exe" && extension != ".msi"))
                    {
                        continue;
                    }
                    string filePath = Path.Combine(directory, fileName);
                    if (!File.Exists(filePath))
                    {
                        throw new FileNotFoundException(string.Format("Could not find {0}.", filePath));
                    }
                    Process p = new Process();
                    p.StartInfo = new ProcessStartInfo()
                    {
                        UseShellExecute = true,
                        FileName = filePath
                    };
                    Console.Write(string.Format("Starting {0} ... ", fileName));
                    p.Start();
                    p.WaitForExit();
                    Console.WriteLine("done!");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                Console.WriteLine("Press any key to continue.");
                Console.Read();
            }
        }

        private static string GetExecutingDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Remove(0, 6);
        }

        #endregion //Methods
    }
}
