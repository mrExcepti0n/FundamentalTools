using System;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;
using NLog;
using NLog.Fluent;

namespace ServiceProvider.WcfServiceHost.Logging
{
    public class WcfRequestLogger : IRequestLogger
    {
        private readonly IRequestLoggerTarget _requestLoggerTarget;
        private ILogger _logger;

        public WcfRequestLogger(IRequestLoggerTarget requestLoggerTarget)
        {
            _requestLoggerTarget = requestLoggerTarget;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public Guid LogRequest(ref Message request)
        {
            //Добавляем идентификатор для текущей операции
            Guid sessionId = Guid.NewGuid();

            try
            {
                MessageBuffer buffer = request.CreateBufferedCopy(int.MaxValue);
                Message msgCopy = buffer.CreateMessage();
                request = buffer.CreateMessage();

                string methodName = request.Headers.Action.Replace("http://tempuri.org/", "");
                string parameters = "";

                if (request.Headers.FindHeader(HeaderMessageConstatns.SiteName, HeaderMessageConstatns.HeaderNamespace) >= 0)
                {
                    parameters = "{S/n=" + request.Headers.GetHeader<string>(HeaderMessageConstatns.SiteName, HeaderMessageConstatns.HeaderNamespace) + "}";
                }

                using (XmlDictionaryReader bodyReader = msgCopy.GetReaderAtBodyContents())
                {
                    parameters += GetMethodParameters(bodyReader);
                }

                //OperationContext.Current.IncomingMessageProperties.Add(HeaderMessageConstatns.SessionId, sessionId);
                //OperationContext.Current.IncomingMessageProperties.Add(HeaderMessageConstatns.CallingMethodName, methodName);

                _requestLoggerTarget.SaveRequest(new RequestMessage(sessionId, "testUser", methodName, parameters));
            }
            catch (MethodAccessException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error on request logging");
            }
            return sessionId;
        }

        private string GetMethodParameters(XmlDictionaryReader reader)
        {
            string name = string.Empty;
            string val = string.Empty;

            StringBuilder result = new StringBuilder();

            int maxParameterValueLength = 250;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    name = $"[{reader.LocalName} ";
                }
                else if (reader.NodeType == XmlNodeType.Text)
                {
                    val = (!string.IsNullOrWhiteSpace(reader.Value) && reader.Value.Length >= maxParameterValueLength)
                        ? reader.Value.Substring(0, maxParameterValueLength)
                        : reader.Value;
                    val = $"= {val}];";
                }
                if (name != string.Empty && val != string.Empty)
                {
                    result.Append(name);
                    result.Append(val);
                    result.Append(Environment.NewLine);
                    name = string.Empty;
                    val = string.Empty;
                }
            }

            return result.ToString();
        }


        public void LogResponse(ref Message response, Guid sessionId)
        {
            _requestLoggerTarget.SaveResponse(sessionId);
        }
    }
}
