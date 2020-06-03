using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider.WcfServiceHost
{
    public class RequestMessage
    {
        public RequestMessage(Guid sessionId, string userId, string methodName, string parameters)
        {
            SessionId = sessionId;
            UserId = userId;
            MethodName = methodName;
            Parameters = parameters;
        }
        public Guid SessionId { get; } 
        public string UserId { get; }
        public string MethodName { get; }
        public string Parameters { get; }
    }
}
