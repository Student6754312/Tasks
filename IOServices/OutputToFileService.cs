using System.IO.Abstractions;
using IOServices.Base;
using IOServices.Interfaces;

namespace IOServices
{
    public class OutputToFileService : OutputToFileBaseService
    {
        private readonly OutputToConsoleService _outputToConsoleService;
        public OutputToFileService(IInputOutputSettings inputOutputSettings, OutputToConsoleService outputToConsoleService) : base(inputOutputSettings, new FileSystem())
        {
            _outputToConsoleService = outputToConsoleService;
        }

        public override void Output(string str)
        {
            base.Output(str);
            _outputToConsoleService.Output(str);
        }
    }
}