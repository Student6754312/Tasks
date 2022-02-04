using System.IO.Abstractions;
using IOServices.Base;
using IOServices.Interfaces;
using IOServices.ServiceFactory;

namespace IOServices
{
    public class InputFromFileService : InputFromFileBaseService
    {
        private readonly IOutputService _outputToFileService;
        public InputFromFileService(IInputOutputSettings inputOutputSettings, IOutputServiceFactory outputServiceFactory) : base(inputOutputSettings, new FileSystem())
        {
            _outputToFileService = outputServiceFactory.GetService();
        }

        public override string? Input()
        {
            var str = base.Input();
            _outputToFileService.Output($"{str}\n");
            return str;
        }

    }
}