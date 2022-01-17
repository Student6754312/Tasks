using System.Collections.Generic;
using IOServices.Base;
using IOServices.ServiceFactory.Base;
using Microsoft.Extensions.Options;

namespace IOServices.ServiceFactory
{
    public class InputServiceFactory <TA>: ServiceBaseFactory<IInputService, TA>, IInputServiceFactory where TA: class 
    {
        public InputServiceFactory(IEnumerable<IInputService> services, IOptions<TA> options)
            : base(services, options)
        {
        }
    }
}