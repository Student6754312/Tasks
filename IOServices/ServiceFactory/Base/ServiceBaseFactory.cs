using System;
using System.Collections.Generic;
using System.Linq;
using IOServices.Interfaces;

namespace IOServices.ServiceFactory.Base
{
    public abstract class ServiceBaseFactory<TS> : IServiceBaseFactory<TS> where TS : class

    {
        private readonly IEnumerable<TS> _services;
        private readonly IInputOutputSettings _inputOutputSettings;

        protected ServiceBaseFactory(IEnumerable<TS> services, IInputOutputSettings inputOutputSettings)
        {
            _services = services;
            _inputOutputSettings = inputOutputSettings;
        }

        public TS GetService()
        {

            var value = _inputOutputSettings.DefaultInputService;

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
                _ => throw new FormatException("Wrong DefaultInputService in appsettings.json")
            };
        }
    }
}