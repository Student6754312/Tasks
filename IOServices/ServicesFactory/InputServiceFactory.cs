using System.Collections.Generic;
using IOServices.ServicesFactory.Base;
using Microsoft.Extensions.Options;

namespace IOServices.ServicesFactory
{
    public class InputServiceFactory <TA>: ServiceBaseFactory<IInputService, TA>  where TA: class
    {
        public InputServiceFactory(IEnumerable<IInputService> services, IOptions<TA> options)
            : base(services, options)
        {
        }
    }
}