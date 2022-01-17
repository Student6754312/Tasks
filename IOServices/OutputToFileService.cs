using System;
using System.Threading;
using IOServices.Base;

namespace IOServices
{
    public class OutputToFileService : OutputToFileBaseService
    {
        private OutputToConsoleService _outputService;

        public OutputToFileService(OutputToConsoleService outputService)
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