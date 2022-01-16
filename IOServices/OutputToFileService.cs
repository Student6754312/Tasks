using System;
using System.Threading;

namespace IOServices
{
    public class OutputToFileService : OutputToFileBaseService
    {
        //private IOutputService _outputService;

        //public OutputToFileService(IOutputService outputService)
        //{
        //    _outputService = outputService;
        //}

        public override void Output(string str)
        {
            base.Output(str);
            //????????????????
            Console.WriteLine(str);
            //_outputService.Output(str);
        }
    }
}