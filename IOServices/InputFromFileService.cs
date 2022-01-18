using System.IO.Abstractions;
using IOServices.Base;
using IOServices.Interfaces;
using Microsoft.Extensions.Options;

namespace IOServices
{
    public class InputFromFileService<TA> : InputFromFileBaseService<TA> where TA: class 
    {
        private readonly IOutputService _outputFileService;
        public InputFromFileService(IOptions<TA> options, IOutputService outputFileService) : base(options, new FileSystem())
        {
            _outputFileService = outputFileService;
        }

        public override string? Input()
        {
            var str = base.Input();
            _outputFileService.Output($"{str}\n");
            return str;
        }

    }
}