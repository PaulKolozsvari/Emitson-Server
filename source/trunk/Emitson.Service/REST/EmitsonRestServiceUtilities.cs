namespace Emitson.Service.REST
{
    #region Using Directives

    using Figlut.Server.Toolkit.Utilities;
    using Figlut.Server.Toolkit.Utilities.Logging;
    using Figlut.Server.Toolkit.Web.Client;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.ServiceModel.Web;
    using System.Text;
    using System.Threading.Tasks;

    #endregion //Using Directives

    public class EmitsonRestServiceUtilities
    {
        #region Utility Methods

        protected void LogInfoMessage(string message)
        {
            message += string.Format(" {0}", DateTime.Now);
            GOC.Instance.Logger.LogMessage(new LogMessage(
                message,
                LogMessageType.Information,
                LoggingLevel.Maximum));
        }

        protected virtual void UpdateHttpStatusOnException(Exception ex)
        {
            WebOperationContext context = WebOperationContext.Current;
            if (context.OutgoingResponse.StatusCode == HttpStatusCode.OK ||
                context.OutgoingResponse.StatusCode == HttpStatusCode.Created)
            {
                context.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            }
            /*A status description with escape sequences causes the server to not respond to the request 
            causing the client to not get a response therefore not know what the exception was.*/
            string errorMessage = ex.Message.Replace("\r", string.Empty);
            errorMessage = errorMessage.Replace("\n", string.Empty);
            errorMessage = errorMessage.Replace("\t", string.Empty);
            context.OutgoingResponse.StatusDescription = errorMessage;
        }

        protected virtual void ValidateRequestMethod(HttpVerb verb)
        {
            ValidateRequestMethod(verb.ToString());
        }

        protected virtual void ValidateRequestMethod(string verb)
        {
            if (WebOperationContext.Current.IncomingRequest.Method != verb)
            {
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.MethodNotAllowed;
                throw new UserThrownException(
                    string.Format(
                    "Unexpected Method of {0} on incoming POST Request {1}.",
                    WebOperationContext.Current.IncomingRequest.Method,
                    WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.ToString()),
                    LoggingLevel.Normal);
            }
        }

        protected virtual Stream GetStreamFromObject(object obj)
        {
            return StreamHelper.GetStreamFromString(GOC.Instance.JsonSerializer.SerializeToText(obj), GOC.Instance.Encoding);
        }

        protected virtual E GetObjectFromStream<E>(Stream stream) where E : class
        {
            return (E)GetObjectFromStream(typeof(E), stream);
        }

        protected virtual object GetObjectFromStream(Type entityType, Stream stream)
        {
            string inputText = StreamHelper.GetStringFromStream(stream, GOC.Instance.Encoding);
            object result = GOC.Instance.JsonSerializer.DeserializeFromText(entityType, inputText);
            if (result == null)
            {
                throw new Exception(string.Format("The following text could not be deserialized to a {0} : {1}", entityType.FullName, inputText));
            }
            return result;
        }

        #endregion //Utility Methods
    }
}
