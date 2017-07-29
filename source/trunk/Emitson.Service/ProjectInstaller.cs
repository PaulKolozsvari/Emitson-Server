namespace Emitson.Service
{
    #region Using Directives

    using Figlut.Server.Toolkit.Utilities;
    using Figlut.Server.Toolkit.Utilities.Logging;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.Linq;
    using System.ServiceProcess;
    using System.Threading.Tasks;

    #endregion //Using Directives

    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        #region Constructors

        public ProjectInstaller()
        {
            InitializeComponent();
        }

        #endregion //Constructors

        #region Methods

        protected override void OnBeforeInstall(IDictionary savedState)
        {
            base.OnBeforeInstall(savedState);
            if (!System.Diagnostics.EventLog.SourceExists("EmitsonService.Source"))
            {
                System.Diagnostics.EventLog.CreateEventSource("EmitsonService.Source", "Emitson.Service.Log");
            }
        }

        protected override void OnAfterUninstall(IDictionary savedState)
        {
            base.OnAfterUninstall(savedState);
            if (System.Diagnostics.EventLog.SourceExists("EmitsonService.Source"))
            {
                System.Diagnostics.EventLog.DeleteEventSource("EmitsonService.Source");
            }
            if (System.Diagnostics.EventLog.Exists("Emitson.Service.Log"))
            {
                System.Diagnostics.EventLog.Delete("Emitson.Service.Log");
            }
        }

        protected override void OnCommitted(IDictionary savedState)
        {
            base.OnCommitted(savedState);
            try
            {
                ServiceController serviceController = new ServiceController("EmitsonService");
                serviceController.Start();
            }
            catch (Exception ex)
            {
                GOC.Instance.Logger = new Logger(
                    false,
                    true,
                    true,
                    LoggingLevel.Normal,
                    null,
                    "EmitsonService.Source",
                    "Emitson.Service.Log");
                GOC.Instance.Logger.LogMessage(new LogMessage(ex.Message, LogMessageType.Error, LoggingLevel.Normal));
            }
        }

        #endregion //Methods
    }
}
