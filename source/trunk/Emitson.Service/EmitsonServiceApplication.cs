namespace Emitson.Service
{
    #region Using Directives

    using Emitson.Service.Configuration;
    using Emitson.Service.REST;
    using Figlut.Server.Toolkit.Data.DB.LINQ;
    using Figlut.Server.Toolkit.Utilities;
    using Figlut.Server.Toolkit.Utilities.Logging;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using System.Text;
    using System.Threading.Tasks;

    #endregion //Using Directives

    public class EmitsonServiceApplication
    {
        #region Singleton Setup

        private static EmitsonServiceApplication _instance;

        public static EmitsonServiceApplication Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EmitsonServiceApplication();
                }
                return _instance;
            }
        }

        #endregion //Singleton Setup

        #region Constructors

        private EmitsonServiceApplication()
        {
        }

        #endregion //Constructors

        #region Methods

        internal void Initialize(EmitsonServiceSettings settings)
        {
            GOC.Instance.Logger = new Logger(
                settings.LogToFile,
                settings.LogToWindowsEventLog,
                settings.LogToConsole,
                settings.LoggingLevel,
                settings.LogFileName,
                settings.EventSourceName,
                settings.EventLogName);
            GOC.Instance.ApplicationName = settings.RootUriContent;
            GOC.Instance.JsonSerializer.IncludeOrmTypeNamesInJsonResponse = settings.IncludeOrmTypeNamesInJsonResponse;
            GOC.Instance.SetEncoding(settings.TextResponseEncoding);

            //LinqFunnelSettings linqFunnelSettings = new LinqFunnelSettings(settings.DatabaseConnectionString, settings.DatabaseCommandTimeout);
            //GOC.Instance.AddByTypeName(linqFunnelSettings);
            //string linqToSqlAssemblyFilePath = Path.Combine(Information.GetExecutingDirectory(), settings.LinqToSQLClassesAssemblyFileName);
            //GOC.Instance.Logger.LogMessage(new LogMessage(string.Format("Attemping to load {0}", Path.GetFileName(linqToSqlAssemblyFilePath)), LogMessageType.Information, LoggingLevel.Minimum));

            //GOC.Instance.LinqToClassesAssembly = Assembly.LoadFrom(linqToSqlAssemblyFilePath);
            //GOC.Instance.LinqToSQLClassesNamespace = settings.LinqToSQLClassesNamespace;
            //GOC.Instance.SetLinqToSqlDataContextType<DatasmithSDDataContext>();
            //GOC.Instance.UserLinqToSqlType = typeof(User);
            //GOC.Instance.ServerActionLinqToSqlType = typeof(ServerAction);
            //GOC.Instance.ServerErrorLinqToSqlType = typeof(ServerError);

            InitializeSericeHost(settings);
        }

        private void InitializeSericeHost(EmitsonServiceSettings settings)
        {
            WebHttpBinding binding = new WebHttpBinding()
            {
                MaxBufferPoolSize = settings.MaxBufferPoolSize,
                MaxBufferSize = Convert.ToInt32(settings.MaxBufferSize),
                MaxReceivedMessageSize = settings.MaxReceivedMessageSize
            };
            if (settings.UseAuthentication)
            {
                binding.Security.Mode = WebHttpSecurityMode.TransportCredentialOnly;
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            }
            ServiceHost serviceHost = new ServiceHost(typeof(EmitsonRestService));
            string address = string.Format("http://127.0.0.1:{0}/{1}", settings.PortNumber, settings.HostAddressSuffix);
            ServiceDebugBehavior debugBehaviour = serviceHost.Description.Behaviors.Find<ServiceDebugBehavior>();
            if (debugBehaviour == null) //This should never be, but just in case.
            {
                debugBehaviour = new ServiceDebugBehavior();
                serviceHost.Description.Behaviors.Add(debugBehaviour);
            }
            debugBehaviour.IncludeExceptionDetailInFaults = settings.IncludeExceptionDetailInResponse;

            serviceHost.AddServiceEndpoint(typeof(IEmitsonRestService), binding, address).Behaviors.Add(new WebHttpBehavior());
            if (GOC.Instance.GetByTypeName<ServiceHost>() != null)
            {
                GOC.Instance.DeleteByTypeName<ServiceHost>();
            }
            GOC.Instance.AddByTypeName(serviceHost);
            serviceHost.Open();

            GOC.Instance.Logger.LogMessage(new LogMessage(
                string.Format("Emitson Service started: {0}", address),
                LogMessageType.Information,
                LoggingLevel.Minimum));
        }

        #endregion //Methods
    }
}