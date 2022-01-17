using IOServices.Base;
using Microsoft.Extensions.Options;

namespace IOServices
{
    public class InputFromFileService<TA> : InputFromFileBaseService<TA> where TA: class 
    {
        private readonly OutputToConsoleService _outputService;
        
        public InputFromFileService(OutputToConsoleService outputService, IOptions<TA> options) : base(options)
        {
            _outputService = outputService;
        }

        public override string? Input()
        {
            var str = base.Input();
            _outputService.Output($"{str}\n");
            return str;
        }

    }
}