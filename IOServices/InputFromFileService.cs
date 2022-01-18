using System.IO.Abstractions;
using IOServices.Base;
using IOServices.Interfaces;
using Microsoft.Extensions.Options;

namespace IOServices
{
    public class InputFromFileService<TA> : InputFromFileBaseService<TA> where TA: class 
    {
        private readonly OutputToConsoleService _outputService;
        private readonly IOutputService _outputFileService;

        public InputFromFileService(OutputToConsoleService outputService, IOptions<TA> options, IOutputService outputFileService) : base(options, new FileSystem())
        {
            _outputService = outputService;
            _outputFileService = outputFileService;
        }

        public override string? Input()
        {
            var str = base.Input();
            _outputService.Output($"{str}\n");
            _outputFileService.Output($"{str}\n");
            return str;
        }

    }
}