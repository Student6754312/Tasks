using System.Collections.Generic;
using IOServices.Base;
using IOServices.ServiceFactory.Base;
using Microsoft.Extensions.Options;

namespace IOServices.ServiceFactory
{
    public class OutputServiceFactory<TA> : ServiceBaseFactory<IOutputService, TA>, IOutputServiceFactory where TA : class
    {
        public OutputServiceFactory(IEnumerable<IOutputService> services, IOptions<TA> options)
            : base(services, options)
        {
        }
    }
}