using System;
using System.Threading;
using IOServices.Base;
using Microsoft.Extensions.Options;

namespace IOServices
{
    public class OutputToFileService<TA> : OutputToFileBaseService<TA> where TA : class
    {
        private OutputToConsoleService _outputService;

        public OutputToFileService(OutputToConsoleService outputService, IOptions<TA> options) : base(options)
        {
            _outputService = outputService;
        }

        public override void Output(string str)
        {
            base.Output(str);
            _outputService.Output(str);
        }
    }
}