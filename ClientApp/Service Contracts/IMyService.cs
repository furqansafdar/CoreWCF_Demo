using ClientDomain;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Common;

namespace ClientApp.Service_Contracts
{
    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        [ReferencePreservingDataContractFormat]
        Task<Manager> SaveApplication(Manager manager);
    }

    public enum EndpointConfiguration
    {
        BasicHttpBinding_IMyService,
    }

    public class MyService : ClientBase<IMyService>, IMyService
    {
        public MyService() : base(GetDefaultBinding(), GetDefaultEndpointAddress())
        {
        }

        private static Binding GetDefaultBinding()
        {
            return GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_IMyService);
        }

        private static EndpointAddress GetDefaultEndpointAddress()
        {
            return GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_IMyService);
        }

        private static Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IMyService))
            {
                var result = new BasicHttpBinding
                {
                    MaxBufferSize = int.MaxValue,
                    ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max,
                    MaxReceivedMessageSize = int.MaxValue,
                    AllowCookies = true,
                    Security =
                    {
                        Mode = BasicHttpSecurityMode.Transport
                    }
                };
                return result;
            }
            throw new InvalidOperationException($"Could not find endpoint with name \'{endpointConfiguration}\'.");
        }

        private static EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IMyService))
            {
                return new EndpointAddress("https://localhost:7086/Service.svc");
            }
            throw new InvalidOperationException($"Could not find endpoint with name \'{endpointConfiguration}\'.");
        }

        public Task<Manager> SaveApplication(Manager manager) => Channel.SaveApplication(manager);
    }
}