using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Options;

namespace IOServices.ServiceFactory.Base
{
    public abstract class ServiceBaseFactory<TS, TA> : IServiceBaseFactory<TS> where TS : class where TA : class

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
            PropertyInfo? propertyInfo = type.GetProperty($"DefaultInputService");
            var value = propertyInfo?.GetValue(_applicationSettings)?.ToString();

            if (value == null)
            {
                throw new FormatException("DefaultInputService in appsettings.json not defined");
            }
            
            string prefix = "";
            if (typeof(TS).Name.StartsWith("IInput"))
            {
                prefix = "InputFrom";
            }
            else if (typeof(TS).Name.StartsWith("IOutput"))
            {
                prefix = "OutputTo";
            }
            

            return value.ToLower() switch
            {
                "console" => _services.First(x => x.GetType().ToString().Contains($"{prefix}ConsoleService")),
                "file" => _services.First(x => x.GetType().ToString().Contains($"{prefix}FileService")),
                _ => throw new ArgumentNullException()
            };
        }
    }
}