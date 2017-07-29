namespace Emitson.Service.Configuration
{
    #region Using Directives

    using Figlut.Server.Toolkit.Utilities;
    using Figlut.Server.Toolkit.Utilities.Logging;
    using Figlut.Server.Toolkit.Utilities.SettingsFile;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion //Using Directives

    public class EmitsonServiceSettings : Settings
    {
        #region Constructors

        public EmitsonServiceSettings()
        {
        }

        #endregion //Constructors

        #region Properties

        #region Logging

        [SettingInfo("Logging", DisplayName = "To File", Description = "Whether or not to log to a text log file in the executing directory.", CategorySequenceId = 0)]
        public bool LogToFile { get; set; }

        [SettingInfo("Logging", DisplayName = "To Windows Event Log", Description = "Whether or not to log to the Windows Event Log.", CategorySequenceId = 1)]
        public bool LogToWindowsEventLog { get; set; }

        [SettingInfo("Logging", DisplayName = "To Console", Description = "Whether or not to write all log messages to the console. Useful when running the service as a console application i.e. running the executable with the /test_mode switch.", CategorySequenceId = 2)]
        public bool LogToConsole { get; set; }

        [SettingInfo("Logging", AutoFormatDisplayName = true, Description = "The name of the text log file to log to. The log file is written in the executing directory.", CategorySequenceId = 3)]
        public string LogFileName { get; set; }

        [SettingInfo("Logging", AutoFormatDisplayName = true, Description = "The name of the event source to use when logging to the Windows Event Log.", CategorySequenceId = 4)]
        public string EventSourceName { get; set; }

        [SettingInfo("Logging", AutoFormatDisplayName = true, Description = "The name of the Windows Event Log to log to.", CategorySequenceId = 5)]
        public string EventLogName { get; set; }

        [SettingInfo("Logging", AutoFormatDisplayName = true, Description = "The extent of messages being logged: None = logging is disabled, Minimum = logs server start/stop and exceptions, Normal = logs additional information messages, Maximum = logs all requests and responses to the server.", CategorySequenceId = 6)]
        public LoggingLevel LoggingLevel { get; set; }

        #endregion //Logging

        #region Service

        [SettingInfo("Service", DisplayName = "Root URI Content", Description = "The text content returned when on the root URI e.g. this could be the application's name.", CategorySequenceId = 0)]
        public string RootUriContent { get; set; }

        [SettingInfo("Service", AutoFormatDisplayName = true, Description = "The suffix to append to the URI on which the Figlut Server will be accessed i.e. http://localhost:{port_number}/{suffix} e.g. http://localhost:8889/Digistics.", CategorySequenceId = 1)]
        public string HostAddressSuffix { get; set; }

        [SettingInfo("Service", AutoFormatDisplayName = true, Description = "The port number on which the Figlut Web Service should listen for requests from clients i.e. http://localhost:{port_number}/{suffix} e.g. http://localhost:2984/Digistics.", CategorySequenceId = 2)]
        public int PortNumber { get; set; }

        [SettingInfo("Service", AutoFormatDisplayName = true, Description = "Whether or not the service should authenticate clients attempting to consume the service.", CategorySequenceId = 3)]
        public bool UseAuthentication { get; set; }

        [SettingInfo("Service", AutoFormatDisplayName = true, Description = "Whether or not to include the exception details including the stack trace in the web response when an unhandled exception occurs.", CategorySequenceId = 4)]
        public bool IncludeExceptionDetailInResponse { get; set; }

        [SettingInfo("Service", AutoFormatDisplayName = true, Description = "Encoding to used on the text response from the service.", CategorySequenceId = 5)]
        public TextEncodingType TextResponseEncoding { get; set; }

        [SettingInfo("Service", DisplayName = "Include ORM Type Names in JSON Response", Description = "Whether or not to include in the JSON response the names of the .NET generated ORM types representing each table in the database.", CategorySequenceId = 6)]
        public bool IncludeOrmTypeNamesInJsonResponse { get; set; }

        [SettingInfo("Service", AutoFormatDisplayName = true, Description = "The maximum amount of memory allocated, in bytes, for the buffer manager that manages the buffers required by endpoints that use this binding.", CategorySequenceId = 7)]
        public long MaxBufferPoolSize { get; set; }

        [SettingInfo("Service", AutoFormatDisplayName = true, Description = "The maximum amount of memory allocated, in bytes, for use by the manager of the message buffers that receive messages from the channel.", CategorySequenceId = 8)]
        public long MaxBufferSize { get; set; }

        [SettingInfo("Service", AutoFormatDisplayName = true, Description = "The maximum size, in bytes, for a message that can be processed by the binding.", CategorySequenceId = 9)]
        public long MaxReceivedMessageSize { get; set; }

        #endregion //Service

        #endregion //Properties
    }
}