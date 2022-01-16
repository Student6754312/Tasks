using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Options;

namespace IOServices.ServicesFactory.Base
{
    public class ServiceBaseFactory<TS, TA> : IServiceBaseFactory<TS> where TS : class where TA : class

    {
        private readonly IEnumerable<TS> _services;
        private readonly TA _applicationSettings;

        protected ServiceBaseFactory(IEnumerable<TS> services, IOptions<TA> options)
        {
            _services = services;
            _applicationSettings = options.Value;
        }

        public TS GetService()
        {

           Type type = _applicationSettings.GetType();
            PropertyInfo propertyInfo = type.GetProperty($"DefaultService");
            var value = propertyInfo.GetValue(_applicationSettings).ToString();

            string service = typeof(TS).Name;

            if (service.StartsWith("IInput"))
            {
                service = "InputFrom";
            }
            else if (service.StartsWith("IOutput"))
            {
                service = "OutputTo";
            }

            return value.ToLower() switch
            {
                "console" => _services.First(x => x.GetType().ToString().Contains($"{service}ConsoleService")),
                "file" => _services.First(x => x.GetType().ToString().Contains($"{service}FileService")),
                _ => throw new ArgumentNullException()
            };
        }
    }
}