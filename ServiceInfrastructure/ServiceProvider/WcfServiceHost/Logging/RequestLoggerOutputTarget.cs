using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ServiceProvider.WcfServiceHost.Logging
{
    public class RequestLoggerOutputTarget : IRequestLoggerTarget
    {
        public void SaveRequest(RequestMessage request)
        {
            Debug.WriteLine(JsonConvert.SerializeObject(request));
        }

        public void SaveResponse(Guid sessionId)
        {
            Debug.WriteLine($"Method (sessionId=${sessionId}) finish at ${DateTime.Now}");
        }
    }
}
