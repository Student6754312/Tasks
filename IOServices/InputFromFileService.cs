namespace IOServices
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
            _outputService.Output($"{str}\n");
            return str;
        }

    }
}