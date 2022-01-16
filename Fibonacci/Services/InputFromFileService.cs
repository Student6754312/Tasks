using IOServices;

namespace Fibonacci.Services
{
    public class InputFromFileService : InputFromFileBaseService
    {
        private readonly IOutputService _outputService;

        public InputFromFileService(IOutputService outputService)
        {
            _outputService = outputService;
        }

        public override string? Input()
        {
            var str = base.Input();
            _outputService.ConsoleOutputLine($"{str}\n");
            return str;
        }

    }
}