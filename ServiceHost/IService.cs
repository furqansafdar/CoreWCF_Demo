using BusinessDomain;
using Common;

namespace ServiceHost
{
    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        [ReferencePreservingDataContractFormat]
        Manager SaveApplication(Manager manager);
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MyService : IMyService
    {
        public Manager SaveApplication(Manager manager)
        {
            if (manager == null) throw new ArgumentNullException(nameof(manager));
            return manager;
        }
    }
}
