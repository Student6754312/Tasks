using System.Collections.Generic;
using IOServices.ServicesFactory.Base;
using Microsoft.Extensions.Options;

namespace IOServices.ServicesFactory
{
    public class OutputServiceFactory<TA> : ServiceBaseFactory<IOutputService, TA> where TA : class
    {
        public OutputServiceFactory(IEnumerable<IOutputService> services, IOptions<TA> options)
            : base(services, options)
        {
        }
    }
}