using NLog;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using NLog.Targets;
using ServiceProvider.Exceptions;

namespace ServiceProvider.WcfServiceHost
{
    public class GlobalExceptionHandler : IErrorHandler
    {
        #region IErrorHandler Members

        private readonly ILogger _logger;

        public GlobalExceptionHandler()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }


        public bool HandleError(Exception ex)
        {
            return true;
        }

        public void ProvideFault(Exception ex, MessageVersion version, ref Message msg)
        {
            var errorId = Guid.NewGuid();
            _logger.Fatal(ex,"errorId={0}", errorId);

            FaultException<CustomException> fException = new FaultException<CustomException>(new CustomException(ex.Message, errorId), 
                "Error occurred during the request");
            MessageFault fault = fException.CreateMessageFault();
            msg = Message.CreateMessage(
                version,
                fault,
                "http://wwwroot.ru"
            );
        }

        #endregion
    }
}