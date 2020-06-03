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
            if (!(ex is FaultException<CustomException>))
            {
                _logger.Fatal(ex);
            }

            return false;
        }

        public void ProvideFault(Exception ex, MessageVersion version, ref Message msg) { }

        #endregion
    }
}