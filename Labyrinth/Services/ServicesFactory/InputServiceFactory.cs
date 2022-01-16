using System.Collections.Generic;
using IOServices.ServicesFactory.Base;
using Labyrinth;
using Microsoft.Extensions.Options;

namespace IOServices.ServicesFactory
{
    public class InputServiceFactory : ServiceBaseFactory<IInputService, ApplicationSettings>, IInputServiceFactory
    {
        public InputServiceFactory(IEnumerable<IInputService> services, IOptions<ApplicationSettings> options)
            : base(services, options)
        {
        }
    }
}