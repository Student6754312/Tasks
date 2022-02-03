using System.IO.Abstractions;
using IOServices.Base;
using Microsoft.Extensions.Options;

namespace IOServices
{
    public class OutputToFileService<TA> : OutputToFileBaseService<TA> where TA : class
    {
        private readonly OutputToConsoleService _outputToConsoleService;
        public OutputToFileService(IOptions<TA> options, OutputToConsoleService outputToConsoleService) : base(options, new FileSystem())
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