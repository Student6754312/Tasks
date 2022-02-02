using System.IO.Abstractions;
using IOServices.Base;
using Microsoft.Extensions.Options;

namespace IOServices
{
    public class OutputToFileService<TA> : OutputToFileBaseService<TA> where TA : class
    {
        private readonly OutputToConsoleService _outputToConsoleService;
        public OutputToFileService(OutputToConsoleService outputService, IOptions<TA> options) : base(options, new FileSystem())
        {
            _outputToConsoleService = outputService;
        }

        public override void Output(string str)
        {
            base.Output(str);
            _outputToConsoleService.Output(str);
        }
    }
}