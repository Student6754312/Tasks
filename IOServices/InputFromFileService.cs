using IOServices.Base;

namespace IOServices
{
    public class InputFromFileService : InputFromFileBaseService
    {
        private readonly OutputToConsoleService _outputService;

        public InputFromFileService(OutputToConsoleService outputService)
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