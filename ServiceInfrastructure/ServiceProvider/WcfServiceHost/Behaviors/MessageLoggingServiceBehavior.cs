using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using ServiceProvider.WcfServiceHost.Logging;

namespace ServiceProvider.WcfServiceHost.Behaviors
{
    public class MessageLoggingServiceBehavior : IServiceBehavior
    {
        private readonly IRequestLogger _requestLogger;

        public MessageLoggingServiceBehavior(IRequestLogger requestLogger)
        {
            _requestLogger = requestLogger;
        }


        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }


        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

            foreach (var channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                var channel = (ChannelDispatcher) channelDispatcherBase;
                foreach (EndpointDispatcher endpoint in channel.Endpoints)
                {
                    endpoint.DispatchRuntime.MessageInspectors.Add(new MessageLoggingInterceptor(_requestLogger));
                }
            }
        }
    }
}