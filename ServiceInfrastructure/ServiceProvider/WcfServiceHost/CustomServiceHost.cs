using System;
using System.ServiceModel;
using ServiceProvider.WcfServiceHost.Behaviors;
using ServiceProvider.WcfServiceHost.Logging;

namespace ServiceProvider.WcfServiceHost
{
    public class CustomServiceHost : ServiceHost
    {
        private readonly IRequestLogger _requestLogger;

        public CustomServiceHost(Type serviceType, IRequestLogger requestLogger, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            _requestLogger = requestLogger;
        }

        protected override void OnOpening()
        {
            Description.Behaviors.Add(new ErrorHandlerServiceBehavior());
            Description.Behaviors.Add(new MessageLoggingServiceBehavior(_requestLogger));
            

            base.OnOpening();
        }
    }
}
