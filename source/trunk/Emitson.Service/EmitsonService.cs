namespace Emitson.Service
{
    #region Using Directives

    using Emitson.Service.Configuration;
    using Figlut.Server.Toolkit.Utilities;
    using Figlut.Server.Toolkit.Utilities.Logging;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceProcess;
    using System.Text;
    using System.Threading.Tasks;

    #endregion //Using Directives

    public partial class EmitsonService : ServiceBase
    {
        public EmitsonService()
        {
            InitializeComponent();
        }

        #region Methods

        protected override void OnStart(string[] args)
        {
            try
            {
                Start(false);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw ex;
            }
        }

        protected override void OnStop()
        {
            try
            {
                Stop();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw ex;
            }
        }

        public static void Start(bool logMax)
        {
            EmitsonServiceSettings settings = GOC.Instance.GetSettings<EmitsonServiceSettings>(true, true);
            if (logMax)
            {
                settings.LoggingLevel = LoggingLevel.Maximum;
            }
            EmitsonServiceApplication.Instance.Initialize(settings);
        }

        public static new void Stop()
        {
            GOC.Instance.GetByTypeName<ServiceHost>().Close();
            GOC.Instance.Logger.LogMessage(new LogMessage(
                "Emitson Service stopped.",
                LogMessageType.Information,
                LoggingLevel.Minimum));
        }

        #endregion //Methods
    }
}
