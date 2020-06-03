using System;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;
using NLog;
using NLog.Internal;
using ServiceProvider.WcfServiceHost.Logging;
using Thinktecture.IdentityModel.Constants;

namespace ServiceProvider.WcfServiceHost
{
    public class MessageLoggingInterceptor : IClientMessageInspector, IDispatchMessageInspector
    {
        public MessageLoggingInterceptor(IRequestLogger requestLogger)
        {
            _logger = LogManager.GetCurrentClassLogger();
            _requestLogger = requestLogger;
        }

        private readonly Logger _logger;
        private readonly IRequestLogger _requestLogger;

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            return null;
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            if (request.IsEmpty)
                return null;
            // identificator for correlation state
            Guid sessionId = _requestLogger.LogRequest(ref request);
            return sessionId;
        }




        //private int GetWorkerId(Message request)
        //{
        //    if (request.Properties.Security == null)
        //    {
        //        //TODO need implement more logic
        //        return -1;
        //    }

        //    var claimsPrinc = request.Properties.Security.ServiceSecurityContext.AuthorizationContext.Properties["ClaimsPrincipal"] as ClaimsPrincipal;
        //    //Claim adminWorkerId = claimsPrinc.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.) .FirstOrDefault(x => x.Type == "http://ibzkh.ru/claims/adminWorkerId");

        //    if (!String.IsNullOrEmpty(adminWorkerId?.Value))
        //    {
        //        Int32.TryParse(adminWorkerId.Value, out var workerChangeBy);
        //        return workerChangeBy;
        //    }


        //    Claim workerId = claimsPrinc.Claims.FirstOrDefault(x => x.Type == "http://ibzkh.ru/claims/WorkerId");
        //    if (!String.IsNullOrEmpty(workerId?.Value))
        //    {
        //        Int32.TryParse(workerId.Value, out var workerChangeBy);

        //        return workerChangeBy;
        //    }

        //    return -1;
        //}


        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            Console.WriteLine(reply);
        }



     

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            try
            {
                if (correlationState is Guid sessionGuid)
                {
                    _requestLogger.LogResponse(ref reply, sessionGuid);
                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception,"Error on save repsonse result");
            }
        }

    }
}