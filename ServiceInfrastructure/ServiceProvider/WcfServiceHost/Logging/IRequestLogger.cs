using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider.WcfServiceHost.Logging
{
    public interface IRequestLogger
    {
        Guid LogRequest(ref Message request);


        void LogResponse(ref Message response, Guid sessionId);
    }
}
