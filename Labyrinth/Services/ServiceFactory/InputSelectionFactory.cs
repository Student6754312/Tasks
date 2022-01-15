using System;
using System.Collections.Generic;
using System.Linq;
using IOServices;
using Microsoft.Extensions.Options;

namespace Fibonacci.Factory
{
    public class InputSelectionFactory : IInputSelectionFactory
    {
        private readonly IEnumerable<IInputService> _inputServices;
        private readonly ApplicationSettings _applicationSettings;

        public InputSelectionFactory(IEnumerable<IInputService> inputServices, IOptions<ApplicationSettings> options)
        {
            _inputServices = inputServices;
            _applicationSettings = options.Value;
        }

        public IInputService GetInputService()
        {
            var value = _applicationSettings.DefaultInputService;
            return value switch
            {
                "Console" => _inputServices.First(x => x.GetType() == typeof(InputFromConsoleService)),
                "File" => _inputServices.First(x => x.GetType() == typeof(InputFromFileService)),
                _ => throw new ArgumentNullException()
            };
        }
    }
}