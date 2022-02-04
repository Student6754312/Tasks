using System.Collections.Generic;
using IOServices.Interfaces;
using IOServices.ServiceFactory.Base;

namespace IOServices.ServiceFactory
{
    public class OutputServiceFactory : ServiceBaseFactory<IOutputService>, IOutputServiceFactory 
    {
        public OutputServiceFactory(IEnumerable<IOutputService> services, IInputOutputSettings inputOutputSettings)
            : base(services, inputOutputSettings)
        {
        }
    }
}