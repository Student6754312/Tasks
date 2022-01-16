using System.Collections.Generic;
using IOServices;
using IOServices.ServicesFactory;
using IOServices.ServicesFactory.Base;
using Microsoft.Extensions.Options;

namespace Labyrinth.Services
{
    public class OutputServiceFactory : ServiceBaseFactory<IOutputService, ApplicationSettings>, IOutputServiceFactory
    {
        public OutputServiceFactory(IEnumerable<IOutputService> services, IOptions<ApplicationSettings> options)
            : base(services, options)
        {
        }
    }
}