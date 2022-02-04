using System.Collections.Generic;
using IOServices.Interfaces;
using IOServices.ServiceFactory.Base;

namespace IOServices.ServiceFactory
{
    public class InputServiceFactory : ServiceBaseFactory<IInputService>, IInputServiceFactory  
    {
        public InputServiceFactory(IEnumerable<IInputService> services, IInputOutputSettings inputOutputSettings)
            : base(services, inputOutputSettings)
        {
        }
    }
}