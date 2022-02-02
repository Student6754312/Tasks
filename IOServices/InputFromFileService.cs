using System.IO.Abstractions;
using IOServices.Base;
using IOServices.Interfaces;
using Microsoft.Extensions.Options;

namespace IOServices
{
    public class InputFromFileService<TA> : InputFromFileBaseService<TA> where TA: class 
    {
        private readonly IOutputService _outputToFileService;
        public InputFromFileService(IOptions<TA> options, IOutputService outputToFileService) : base(options, new FileSystem())
        {
            _outputToFileService = outputToFileService;
        }

        public override string? Input()
        {
            var str = base.Input();
            _outputToFileService.Output($"{str}\n");
            return str;
        }

    }
}