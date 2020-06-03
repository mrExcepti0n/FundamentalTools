using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider.WcfServiceHost.Logging
{
    public interface IRequestLoggerTarget
    {
        void SaveRequest(RequestMessage request);

        void SaveResponse(Guid sessionId);
    }
}
